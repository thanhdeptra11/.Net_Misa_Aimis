using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface ICustomerBL : IBaseBL<Customer>
    {
        // Kế thừa tất cả CRUD operations từ IBaseBL<Customer>
        // GetAllAsync(), GetByIdAsync(), AddAsync(), UpdateAsync(), DeleteAsync(), DeleteMultipleAsync()
        // Thêm các method đặc thù cho Customer nếu cần
    }
}
