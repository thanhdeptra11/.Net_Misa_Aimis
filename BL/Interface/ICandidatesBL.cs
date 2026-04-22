using Common.Model;

namespace BL.Interface
{
    public interface ICandidatesBL : IBaseBL<Candidates>
    {
        Task<PagingResponse<Common.DTO.CandidateDto>> GetPagingDtoAsync(PagingRequest request);
        Task<Common.DTO.CandidateDto?> GetDtoByIdAsync(Guid id);
    }
}
