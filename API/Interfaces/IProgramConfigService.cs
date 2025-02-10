using Shared.Models.Model;

namespace Microondas.API.Interfaces
{
    public interface IProgramConfigService
    {
        Task<IEnumerable<ProgramConfig>> GetAllAsync();
        Task AddAsync(ProgramConfig programConfig);
        Task UpdateAsync(ProgramConfig programConfig);
        Task DeleteAsync(int id);
    }
}
