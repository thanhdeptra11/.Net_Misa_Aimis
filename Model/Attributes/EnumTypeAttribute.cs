using System;

namespace Common.Attributes
{
    /// <summary>
    /// Đánh dấu một property kiểu int/int? là một enum, cung cấp kiểu enum tương ứng.
    /// BaseDL sẽ tự động đọc attribute này và điền Description vào property
    /// có tên {PropertyName}Description (phải được khai báo [NotMapped] trong model).
    /// Ví dụ:
    ///   [EnumType(typeof(CompositionType))]
    ///   public int? CompositionType { get; set; }
    ///
    ///   [NotMapped]
    ///   public string? CompositionTypeDescription { get; set; }
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EnumTypeAttribute : Attribute
    {
        public Type EnumType { get; }

        public EnumTypeAttribute(Type enumType)
        {
            if (!enumType.IsEnum)
                throw new ArgumentException($"'{enumType.Name}' phải là kiểu enum.", nameof(enumType));

            EnumType = enumType;
        }
    }
}
