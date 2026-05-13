using System.ComponentModel;

namespace Common.Enums
{
    public enum CompositionType
    {
        [Description("Thông tin nhân viên")]
        StaffInformation = 1,

        [Description("Chấm công")]
        Attendance = 2,

        [Description("Doanh số")]
        Revenue = 3,

        [Description("KPI")]
        KPI = 4,

        [Description("Sản phẩm")]
        Product = 5,

        [Description("Lương")]
        Wage = 6,
        
        [Description("Thuế TNCN")]
        IndividualTax = 7,
        
        [Description("Bảo hiểm - Công đoàn")]
        Assurance = 8,
        
        [Description("Khác")]
        Other = 9

    }
}
