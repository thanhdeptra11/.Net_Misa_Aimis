using System.ComponentModel;

namespace Common.Enums
{
    public enum MiValueType
    {
        [Description("Số")]
        Number = 1,

        [Description("Văn bản")]
        Text = 2,

        [Description("Ngày tháng")]
        Date = 3,

        [Description("Tiền tệ")]
        Currency = 4
    }
}
