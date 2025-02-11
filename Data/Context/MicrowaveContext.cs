using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Models.Model;
using Shared.Models.Models;

namespace Data.Context
{
    public class MicrowaveContext : DbContext
    {
       

        public DbSet<ProgramConfig> ProgramConfigs { get; set; }
        public DbSet<User> Users { get; set; }

        public MicrowaveContext(DbContextOptions<MicrowaveContext> options) : base(options)
        {
           
        }

        public void InitializeDatabase()
        {
            this.Database.EnsureCreated();
            SeedDatabase(this);
        }

        private static void SeedDatabase(MicrowaveContext context)
        {
            // Valores hardcoded
            var hardcodedPrograms = new List<ProgramConfig>
            {
                new ProgramConfig(null, "Pipoca", "Pipoca (de micro-ondas)", 180, 7, "#", "Observar o barulho de estouros do milho, caso houver um intervalo de mais de 10 segundos entre um estouro e outro, interrompa o aquecimento.", true),
                new ProgramConfig(null, "Leite", "Leite", 300, 5, "@", "Cuidado com aquecimento de líquidos, o choque térmico aliado ao movimento do recipiente pode causar fervura imediata causando risco de queimaduras.", true),
                new ProgramConfig(null, "Carnes de boi", "Carne em pedaço ou fatias", 840, 4, "+", "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme.", true),
                new ProgramConfig(null, "Frango", "Frango (qualquer corte)", 480, 7, "()", "Interrompa o processo na metade e vire o conteúdo com a parte de baixo para cima para o descongelamento uniforme.", true),
                new ProgramConfig(null, "Feijão", "Feijão congelado", 480, 9, "/", "Deixe o recipiente destampado e em casos de plástico, cuidado ao retirar o recipiente pois o mesmo pode perder resistência em altas temperaturas.", true)
            };

            // Obtém todos os "Str" existentes no banco de dados
            var existingStrs = new HashSet<string>(context.ProgramConfigs.Select(p => p.Str));

            // Filtra os registros para adicionar apenas os que têm "Str" único
            var newPrograms = hardcodedPrograms
                .Where(p => !existingStrs.Contains(p.Str)) // Verifica se o "Str" já existe no banco
                .ToList();

            if (newPrograms.Any()) // Se houver novos registros, adiciona ao banco
            {
                context.ProgramConfigs.AddRange(newPrograms);
                context.SaveChanges();
            }
        }


    }
}

