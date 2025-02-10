using Data.Context;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ProgramConfigRepository : RepositoryInterface<ProgramConfig>
    {
        private readonly MicrowaveContext _context;

        public ProgramConfigRepository(MicrowaveContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProgramConfig>> GetAllAsync()
        {
            return await _context.ProgramConfigs.ToListAsync();
        }

        public async Task<ProgramConfig?> GetByIdAsync(int id)
        {
            return await _context.ProgramConfigs.FindAsync(id);
        }

        public async Task AddAsync(ProgramConfig entity)
        {
            await _context.ProgramConfigs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProgramConfig entity)
        {
            _context.ProgramConfigs.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.ProgramConfigs.FindAsync(id);
            if (entity != null)
            {
                _context.ProgramConfigs.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<ProgramConfig, bool>> predicate)
        {
            return await _context.ProgramConfigs.AnyAsync(predicate);
        }
    }

}
