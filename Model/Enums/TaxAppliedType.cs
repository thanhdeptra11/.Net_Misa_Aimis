using System.ComponentModel;

namespace Common.Enums
{
    public enum TaxAppliedType
    {
       [Description("Chịu thuế")]
        Taxable = 1,

        [Description("Miễn thuế toàn phần")]
        Dispense = 2,

        [Description("Miễn thuế một phần")]
        DispensePartition = 3
    }
}
