using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.DTOs.Request
{
    public record  AuthRequestDTO(string Username, string Password)
    {

    }
}
