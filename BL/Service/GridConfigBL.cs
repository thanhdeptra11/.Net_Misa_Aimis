using BL.Base;
using BL.Interface;
using Common.Model;
using DL.Interface;

namespace BL.Service
{
    public class GridConfigBL : BaseBL<GridConfig>, IGridConfigBL
    {
        public GridConfigBL(IBaseDL<GridConfig> baseDL) : base(baseDL)
        {
        }
    }
}
