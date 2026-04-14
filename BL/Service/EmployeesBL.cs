using BL.Base;
using BL.Interface;
using Common.Model;
using DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Service
{
    public class EmployeesBL : BaseBL<Employees>, IEmployeesBL
    {
        public EmployeesBL(IBaseDL<Employees> baseDL) : base(baseDL)
        {
        }
    }
}
