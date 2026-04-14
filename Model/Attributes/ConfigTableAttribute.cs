using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Attributes
{
    public class ConfigTableAttribute : Attribute
    {
        public string tableName { get; }
        public string PrimaryKey { get; set; } = "id"; // Mặc định là "id", có thể override khi sử dụng attribute

        public ConfigTableAttribute(string tableName)
        {
            this.tableName = tableName;
        }

    }
}
