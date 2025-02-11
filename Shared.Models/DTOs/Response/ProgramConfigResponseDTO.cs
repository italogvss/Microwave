
namespace Shared.Models.DTO.Response
{
    public record ProgramConfigResponseDTO(
        int Id,
        string Name,
        string Food,
        int Time,
        int Power,
        string Str,
        string? Instructions,
        bool isDefault
    )
    { }
}
