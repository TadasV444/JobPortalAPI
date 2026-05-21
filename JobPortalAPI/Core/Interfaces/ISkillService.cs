using JobPortalAPI.Api.Models.Requests;
using JobPortalAPI.Api.Models.Responses;

namespace JobPortalAPI.Core.Interfaces;

public interface ISkillService
{
    Task<SkillResponse?> CreateSkillAsync(CreateSkillsRequest request);
    Task<List<SkillResponse>> GetSkillsAsync();
    Task<bool> DeleteSkillByIdAsync(int id);   
}