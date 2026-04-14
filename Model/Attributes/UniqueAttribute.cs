using System;

namespace Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UniqueAttribute : Attribute
    {
        public string? Message { get; }

        public UniqueAttribute(string? message = null)
        {
            Message = message;
        }
    }
}
