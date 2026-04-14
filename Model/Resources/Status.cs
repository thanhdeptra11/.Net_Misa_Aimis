using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Resources
{
    /// <summary>
    /// Quản lý trạng thái
    /// </summary>
    public enum Status: int
    {
        /// <summary>
        /// Ko làm gì
        /// </summary>
        None = 0,
        /// <summary>
        /// Specifies that the operation is an insert action.
        /// </summary>
        Insert = 1,
        /// <summary>
        /// Specifies an update operation.
        /// </summary>
        Update = 2,
        /// <summary>
        /// Represents the delete operation or action.
        /// </summary>
        Delete = 3,
    }
}
