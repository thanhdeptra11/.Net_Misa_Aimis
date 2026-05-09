using Common.DTO;
using Common.Model;
using System;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface ISalaryCompositionBL : IBaseBL<SalaryComposition>
    {
        Task<PagingResponse<SalaryCompositionDto>> GetPagingDtoAsync(PagingRequest request);
        Task<SalaryCompositionDto?> GetDtoByIdAsync(Guid id);
    }
}
