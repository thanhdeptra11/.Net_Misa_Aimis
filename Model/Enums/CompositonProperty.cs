using System.ComponentModel;

namespace Common.Enums
{
    public enum CompositionProperty
    {
        [Description("Thu nhập")]
        Income = 1,

        [Description("Khấu trừ")]
        Deduction = 2,

        [Description("Khác")]
        Other = 3
    }
}
