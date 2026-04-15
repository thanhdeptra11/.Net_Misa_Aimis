using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class FilterCondition
    {

        public string DataType { get; set; } = string.Empty;
        public object? Value { get; set; }
        public string Operator { get; set; } = string.Empty;
        public string Property { get; set; } = string.Empty;
    }
}
