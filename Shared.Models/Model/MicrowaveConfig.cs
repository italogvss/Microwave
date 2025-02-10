using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Model
{
    public class MicrowaveConfig
    {
        public int Id { get; set; }
        public required string State { get; set; }
        public int? ProgramId { get; set; }
        public virtual ProgramConfig? Program { get; set; }
    }

}
