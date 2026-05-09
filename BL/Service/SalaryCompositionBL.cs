using BL.Base;
using BL.Interface;
using Common.DTO;
using Common.Model;
using DL.Interface;
using System;
using System.Threading.Tasks;

namespace BL.Service
{
    public class SalaryCompositionBL : BaseBL<SalaryComposition>, ISalaryCompositionBL
    {
        private readonly ISalaryCompositionRepository _repository;

        public SalaryCompositionBL(ISalaryCompositionRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<PagingResponse<SalaryCompositionDto>> GetPagingDtoAsync(PagingRequest request)
        {
            return await _repository.GetPagingDtoAsync(request);
        }

        public async Task<SalaryCompositionDto?> GetDtoByIdAsync(Guid id)
        {
            return await _repository.GetDtoByIdAsync(id);
        }
    }
}
