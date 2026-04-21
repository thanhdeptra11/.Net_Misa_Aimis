using BL.Base;
using BL.Interface;
using DL.Interface;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Service
{
    public class RegionBL : BaseBL<Region, int>, IRegionBL
    {
        private readonly IRegionRepository _regionRepository;

        public RegionBL(IRegionRepository regionRepository) : base(regionRepository)
        {
            _regionRepository = regionRepository;
        }
    }
}
