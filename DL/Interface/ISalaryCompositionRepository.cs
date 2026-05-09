using Common.DTO;
using Common.Model;
using System;
using System.Threading.Tasks;

namespace DL.Interface
{
    public interface ISalaryCompositionRepository : IBaseDL<SalaryComposition>
    {
        Task<PagingResponse<SalaryCompositionDto>> GetPagingDtoAsync(PagingRequest request);
        Task<SalaryCompositionDto?> GetDtoByIdAsync(Guid id);
    }
}
