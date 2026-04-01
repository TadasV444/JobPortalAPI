using JobPortalAPI.Api.Models.Dtos;

namespace JobPortalAPI.Core.Interfaces;

public interface IEmployersService
{
    Task<List<EmployerListDto>> GetEmployersAsync();
    Task<EmployerDetailsDto?> GetEmployerDetailsAsync(int id);
}