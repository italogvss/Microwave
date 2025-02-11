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
   
    ///<summary>
    /// Classe estática responsável por mapear objetos do tipo ProgramConfig para ProgramConfigResponseDTO e vice-versa.
    /// </summary>
    public static class ProgramConfigMapper
    {
        /// <summary>
        /// Converte um objeto ProgramConfig em um objeto ProgramConfigResponseDTO.
        /// </summary>
        /// <param name="program">O objeto ProgramConfig a ser convertido.</param>
        /// <returns>Um objeto ProgramConfigResponseDTO.</returns>
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

        /// <summary>
        /// Converte uma lista de objetos ProgramConfig em uma lista de objetos ProgramConfigResponseDTO.
        /// </summary>
        /// <param name="programs">A lista de objetos ProgramConfig a ser convertida.</param>
        /// <returns>Uma lista de objetos ProgramConfigResponseDTO.</returns>
        public static List<ProgramConfigResponseDTO> ToDtoList(this IEnumerable<ProgramConfig> programs) =>
            programs.Select(program => program.ToDto()).ToList();

        /// <summary>
        /// Converte um objeto ProgramConfigRequestDTO em um objeto ProgramConfig.
        /// </summary>
        /// <param name="dto">O objeto ProgramConfigRequestDTO a ser convertido.</param>
        /// <returns>Um objeto ProgramConfig.</returns>
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
