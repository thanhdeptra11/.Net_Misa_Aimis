using Common.Model;

namespace DL.Interface
{
    public interface ICandidatesRepository : IBaseDL<Candidates>
    {
        Task<PagingResponse<Common.DTO.CandidateDto>> GetPagingDtoAsync(PagingRequest request);
        Task<Common.DTO.CandidateDto?> GetDtoByIdAsync(Guid id);
    }
}
