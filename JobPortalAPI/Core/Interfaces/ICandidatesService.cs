using JobPortalAPI.Api.Models.Dtos;

namespace JobPortalAPI.Core.Interfaces;

public interface ICandidatesService
{
    Task<List<CandidateListDto>> GetCandidatesAsync(); 
    Task<CandidateDetailsDto?> GetCandidateDetailsAsync(int id);
}