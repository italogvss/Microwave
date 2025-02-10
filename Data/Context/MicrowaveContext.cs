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
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Microwave;" +
            "Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application " +
            "Intent=ReadWrite;Multi Subnet Failover=False";

        public DbSet<ProgramConfig> ProgramConfigs { get; set; }

        public MicrowaveContext(DbContextOptions<MicrowaveContext> options) : base(options)
        {
           
        }

        public void InitializeDatabase()
        {
            this.Database.EnsureCreated();
            SeedDatabase(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(connectionString)
                .UseLazyLoadingProxies();
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

