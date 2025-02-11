
namespace Shared.Models.DTO.Request
{
    public record ProgramConfigRequestDTO(
        int Id,
        string Name,
        string Food,
        int Time,
        int Power,
        string Str,
        string? Instructions
    )
    { }
}
