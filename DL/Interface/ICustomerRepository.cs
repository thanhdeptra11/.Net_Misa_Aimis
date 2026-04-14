using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL.Interface
{
    public interface ICustomerRepository : IBaseDL<Customer>
    {
        // Kế thừa tất cả CRUD operations từ IBaseDL<Customer>
        // Thêm các method đặc thù cho Customer nếu cần
    }
}
