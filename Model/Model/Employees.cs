using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Employees : BaseModel
    {
        /// <summary>
        /// Id nhân viên ghi đè base
        /// </summary>
        [Column("employee_id")]
        [JsonPropertyName("employeeId")]
        public override Guid Id { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [Column("employee_name")]
        [JsonPropertyName("employeeName")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// Tuổi nhân viên
        /// </summary>
        [Column("employee_age")]
        [JsonPropertyName("employeeAge")]
        public int EmployeeAge { get; set; }
    }
}
