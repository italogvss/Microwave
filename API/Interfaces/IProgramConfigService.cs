using Shared.Models.DTO.Request;
using Shared.Models.DTO.Response;
using Shared.Models.Model;

namespace Microondas.API.Interfaces
{
    public interface IProgramConfigService
    {
        Task<IEnumerable<ProgramConfigResponseDTO>> GetAllAsync();
        Task AddAsync(ProgramConfigRequestDTO programConfigDto);
        Task UpdateAsync(ProgramConfigRequestDTO programConfigDto);
        Task DeleteAsync(int id);
    }
}
