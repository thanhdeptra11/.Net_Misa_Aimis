using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Common.Extension
{
    /// <summary>
    /// Helper dùng reflection để tự động lấy mô tả (Description) của các trường enum
    /// và gán vào property tương ứng có tên là {PropertyName}Description trên object.
    ///
    /// Điều kiện để hoạt động:
    ///   1. Property kiểu int/int? phải được đánh [EnumType(typeof(TênEnum))]
    ///   2. Model phải có property [NotMapped] tên là {PropertyName}Description kiểu string?
    /// </summary>
    public static class EnumDescriptionHelper
    {
        // Cache mapping: Type -> danh sách (sourceProperty, descriptionProperty, enumType)
        private static readonly Dictionary<Type, List<EnumMapping>> _mappingCache = new();
        private static readonly object _lock = new();

        private record EnumMapping(
            PropertyInfo SourceProperty,
            PropertyInfo DescriptionProperty,
            Type EnumType);

        /// <summary>
        /// Enrich description cho một đối tượng đơn lẻ.
        /// </summary>
        public static void Enrich<T>(T obj) where T : class
        {
            if (obj == null) return;
            var mappings = GetMappings(typeof(T));
            ApplyMappings(obj, mappings);
        }

        /// <summary>
        /// Enrich description cho danh sách đối tượng.
        /// </summary>
        public static void EnrichAll<T>(IEnumerable<T> items) where T : class
        {
            if (items == null) return;
            var mappings = GetMappings(typeof(T));
            if (mappings.Count == 0) return;

            foreach (var item in items)
            {
                if (item != null)
                    ApplyMappings(item, mappings);
            }
        }

        // ------------------------------------------------------------------ //

        private static List<EnumMapping> GetMappings(Type type)
        {
            if (_mappingCache.TryGetValue(type, out var cached))
                return cached;

            lock (_lock)
            {
                if (_mappingCache.TryGetValue(type, out cached))
                    return cached;

                var result = new List<EnumMapping>();
                var allProps = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in allProps)
                {
                    var enumAttr = prop.GetCustomAttribute<EnumTypeAttribute>();
                    if (enumAttr == null) continue;

                    // Tìm property Description tương ứng: {PropertyName}Description
                    var descPropName = $"{prop.Name}Description";
                    var descProp = type.GetProperty(descPropName,
                        BindingFlags.Public | BindingFlags.Instance);

                    // Phải có [NotMapped] để không bị Dapper/EF map sai
                    if (descProp == null || !descProp.CanWrite) continue;
                    if (descProp.GetCustomAttribute<NotMappedAttribute>() == null) continue;

                    result.Add(new EnumMapping(prop, descProp, enumAttr.EnumType));
                }

                _mappingCache[type] = result;
                return result;
            }
        }

        private static void ApplyMappings(object obj, List<EnumMapping> mappings)
        {
            foreach (var mapping in mappings)
            {
                var rawValue = mapping.SourceProperty.GetValue(obj);
                if (rawValue == null) continue;

                // rawValue có thể là int hoặc int?
                var intValue = Convert.ToInt32(rawValue);
                var description = GetEnumDescription(mapping.EnumType, intValue);
                mapping.DescriptionProperty.SetValue(obj, description);
            }
        }

        /// <summary>
        /// Lấy [Description] của một giá trị enum theo số nguyên.
        /// Nếu không có [Description], trả về tên của member enum.
        /// Nếu giá trị không tồn tại trong enum, trả về null.
        /// </summary>
        private static string? GetEnumDescription(Type enumType, int value)
        {
            if (!Enum.IsDefined(enumType, value)) return null;

            var enumValue = Enum.ToObject(enumType, value);
            var memberInfo = enumType.GetMember(enumValue.ToString()!);
            if (memberInfo.Length == 0) return enumValue.ToString();

            var descAttr = memberInfo[0].GetCustomAttribute<DescriptionAttribute>();
            return descAttr?.Description ?? enumValue.ToString();
        }
    }
}
