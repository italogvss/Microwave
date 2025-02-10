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
    public class MicrowaveConfigRepository : RepositoryInterface<MicrowaveConfig>
    {
        private readonly MicrowaveContext _context;

        public MicrowaveConfigRepository(MicrowaveContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MicrowaveConfig>> GetAllAsync()
        {
            return await _context.MicrowaveConfigs.ToListAsync();
        }

        public async Task<MicrowaveConfig?> GetByIdAsync(int id)
        {
            return await _context.MicrowaveConfigs.FindAsync(id);
        }

        public async Task AddAsync(MicrowaveConfig entity)
        {
            await _context.MicrowaveConfigs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MicrowaveConfig entity)
        {
            _context.MicrowaveConfigs.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.MicrowaveConfigs.FindAsync(id);
            if (entity != null)
            {
                _context.MicrowaveConfigs.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}
