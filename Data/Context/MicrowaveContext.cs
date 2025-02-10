using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Models.Model;

namespace Data.Context
{
    public class MicrowaveContext : DbContext
    {
       

        public DbSet<ProgramConfig> ProgramConfigs { get; set; }
        public DbSet<MicrowaveConfig> MicrowaveConfigs { get; set; }

        public MicrowaveContext(DbContextOptions<MicrowaveContext> options) : base(options)
        {
           
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configura a relação entre MicrowaveConfig e ProgramConfig (1:1)
            modelBuilder.Entity<MicrowaveConfig>()
        .HasOne(m => m.Program)  // Relaciona MicrowaveConfig com ProgramConfig
        .WithMany()  // ProgramConfig não precisa ter uma referência de volta
        .HasForeignKey(m => m.ProgramId) // Chave estrangeira de MicrowaveConfig
        .OnDelete(DeleteBehavior.SetNull); // Quando o ProgramConfig for removido, não exclui o MicrowaveConfig, mas apenas define como nulo o Program.
        }

        public void InitializeDatabase()
        {
            this.Database.EnsureCreated();
            SeedDatabase(this);
        }

        private static void SeedDatabase(MicrowaveContext context)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Data/Resources", "defaultPrograms.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Arquivo de seed não encontrado: {filePath}");
            }

            var jsonData = File.ReadAllText(filePath);
            var programConfigsFromJson = JsonConvert.DeserializeObject<List<ProgramConfig>>(jsonData);

            if (programConfigsFromJson == null || !programConfigsFromJson.Any())
                return;

            // Obtém todos os "Str" existentes no banco de dados
            var existingStrs = new HashSet<string>(context.ProgramConfigs.Select(p => p.Str));

            // Filtra os registros para adicionar apenas os que têm "Str" único
            var newPrograms = programConfigsFromJson
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

