using Data.Repository;
using Microondas.API.Exeptions;
using Microondas.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Model;

namespace Microondas.API.Service
{
    public class ProgramConfigService : IProgramConfigService
    {
        private readonly ProgramConfigRepository _programConfigRepository;

        public ProgramConfigService(ProgramConfigRepository programConfigRepository)
        {
            _programConfigRepository = programConfigRepository;
        }

        public async Task<IEnumerable<ProgramConfig>> GetAllAsync()
        {
            return await _programConfigRepository.GetAllAsync();
        } 

        public async Task AddAsync(ProgramConfig programConfig)
        {
            var programsList = await _programConfigRepository.GetAllAsync();
            if (programsList.Any(p => p.Str == programConfig.Str))
            {
                throw new DuplicateEntryException("String already exist");
            }
            await _programConfigRepository.AddAsync(programConfig);
        }

        public async Task UpdateAsync(ProgramConfig programConfig)
        {
            var existingProgram = await _programConfigRepository.GetByIdAsync((int)programConfig.Id);

            if (existingProgram == null)
            {
                throw new NotFoundException("Program not found");
            }

            bool hasDuplicate = await _programConfigRepository.AnyAsync(p => p.Str == programConfig.Str && p.Id != programConfig.Id);

            if (hasDuplicate)
            {
                throw new DuplicateEntryException("String already exists");
            }

            // Atualiza os valores do objeto existente
            existingProgram.Update(
                programConfig.Name,
                programConfig.Food,
                programConfig.Time,
                programConfig.Power,
                programConfig.Str,
                programConfig.Instructions
                );

            await _programConfigRepository.UpdateAsync(existingProgram);
        }

        public async Task DeleteAsync(int id)
        {
            await _programConfigRepository.DeleteAsync(id);
        }
    }
}
