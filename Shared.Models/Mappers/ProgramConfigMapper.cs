using Shared.Models.DTO.Request;
using Shared.Models.DTO.Response;
using Shared.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Mappers
{
    public static class ProgramConfigMapper
    {
        public static ProgramConfigResponseDTO ToDto(this ProgramConfig program) =>
            new ProgramConfigResponseDTO(
                program.Id ?? 0,  // Garante que o ID nunca seja nulo
                program.Name,
                program.Food,
                program.Time,
                program.Power,
                program.Str,
                program.Instructions,
                program.IsDefault
            );

        public static List<ProgramConfigResponseDTO> ToDtoList(this IEnumerable<ProgramConfig> programs) =>
            programs.Select(program => program.ToDto()).ToList();

        public static ProgramConfig ToModel(this ProgramConfigRequestDTO dto) =>
             new ProgramConfig(
                null,
                dto.Name,
                dto.Food,
                dto.Time,
                dto.Power,
                dto.Str,
                dto.Instructions,
                false
             );
    }
    }
