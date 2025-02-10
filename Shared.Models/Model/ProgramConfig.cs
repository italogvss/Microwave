using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models.Model
{
    public class ProgramConfig
    {
        public int? Id { get; private set; }
        public string Name { get; private set; }
        public string Food { get; private set; }
        public int Time { get; private set; }
        public int Power { get; private set; }
        public string Str { get; private set; }
        public string? Instructions { get; private set; }
        public bool IsDefault { get; private set; }

        public ProgramConfig(int? id, string name, string food, int time, int power, string str, string? instructions, bool isDefault)
        {
            Id = id;
            Name = name;
            Food = food;
            Time = time;
            Power = power;
            Str = str;
            Instructions = instructions;
            IsDefault = isDefault;
        }

        // Método para atualizar valores, caso necessário
        public void Update(string name, string food, int time, int power, string str, string? instructions)
        {
            Name = name;
            Food = food;
            Time = time;
            Power = power;
            Str = str;
            Instructions = instructions;
        }
    }
}

