using Data.Repository;
using Microondas.API.Exeptions;
using Microondas.API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models.DTO.Request;
using Shared.Models.DTO.Response;
using Shared.Models.Mappers;
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

        public async Task<IEnumerable<ProgramConfigResponseDTO>> GetAllAsync()
        {
            var entities = await _programConfigRepository.GetAllAsync();
            return ProgramConfigMapper.ToDtoList(entities);
        } 

        public async Task AddAsync(ProgramConfigRequestDTO programConfigDto)
        {
            var programsList = await _programConfigRepository.GetAllAsync();

            if (programsList.Any(p => p.Str == programConfigDto.Str))
            {
                throw new DuplicateEntryException("String já existe");
            }
            var entity = ProgramConfigMapper.ToModel(programConfigDto);
            await _programConfigRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(ProgramConfigRequestDTO programConfigDto)
        {
            var existingProgram = await _programConfigRepository.GetByIdAsync((int)programConfigDto.Id);

            if (existingProgram == null)
            {
                throw new NotFoundException("Programa não encontrado");
            }

            bool hasDuplicate = await _programConfigRepository.AnyAsync(p => p.Str == programConfigDto.Str && p.Id != programConfigDto.Id);

            if (hasDuplicate)
            {
                throw new DuplicateEntryException("String já existe");
            }

            // Atualiza os valores do objeto existente
            existingProgram.Update(
                programConfigDto.Name,
                programConfigDto.Food,
                programConfigDto.Time,
                programConfigDto.Power,
                programConfigDto.Str,
                programConfigDto.Instructions
                );

            await _programConfigRepository.UpdateAsync(existingProgram);
        }

        public async Task DeleteAsync(int id)
        {
            await _programConfigRepository.DeleteAsync(id);
        }
    }
}
