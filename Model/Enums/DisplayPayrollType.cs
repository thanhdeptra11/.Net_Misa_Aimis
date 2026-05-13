using System.ComponentModel;

namespace Common.Enums
{
    public enum DisplayPayrollType
    {
        [Description("Có")]
        Yes = 1,

        [Description("Không")]
        No = 2,

        [Description("Chỉ hiển thị nếu giá trị khác 0")]
        DifferenceZero = 3
    }
}
