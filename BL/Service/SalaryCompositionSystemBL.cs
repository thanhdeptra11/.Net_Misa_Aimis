using BL.Base;
using BL.Interface;
using Common.Model;
using DL.Interface;

namespace BL.Service
{
    public class SalaryCompositionSystemBL : BaseBL<SalaryCompositionSystem>, ISalaryCompositionSystemBL
    {
        public SalaryCompositionSystemBL(IBaseDL<SalaryCompositionSystem> baseDL) : base(baseDL)
        {
        }
    }
}
