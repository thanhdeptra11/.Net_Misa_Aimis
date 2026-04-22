using BL.Base;
using BL.Interface;
using Common.Model;
using DL.Interface;

namespace BL.Service
{
    public class CandidatesBL : BaseBL<Candidates>, ICandidatesBL
    {
        private readonly ICandidatesRepository _repository;

        public CandidatesBL(ICandidatesRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<PagingResponse<Common.DTO.CandidateDto>> GetPagingDtoAsync(PagingRequest request)
        {
            return await _repository.GetPagingDtoAsync(request);
        }

        public async Task<Common.DTO.CandidateDto?> GetDtoByIdAsync(Guid id)
        {
            return await _repository.GetDtoByIdAsync(id);
        }
    }
}
