using System.Collections.Generic;
using System.Threading.Tasks;
using MiAreaVol.Models;
using MiAreaVol.Data;
using Microsoft.EntityFrameworkCore;

namespace MiAreaVol.Services
{
    public class CilindroService
    {
        private readonly MiAreaVolContext _context;
        public CilindroService(MiAreaVolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cilindro>> GetAllAsync() => await _context.Cilindros.ToListAsync();
        public async Task<Cilindro?> GetByIdAsync(int id) => await _context.Cilindros.FindAsync(id);
        public async Task<Cilindro> CreateAsync(Cilindro c)
        {
            _context.Cilindros.Add(c);
            await _context.SaveChangesAsync();
            return c;
        }
        public async Task<bool> UpdateAsync(int id, Cilindro c)
        {
            var existing = await _context.Cilindros.FindAsync(id);
            if (existing == null) return false;
            existing.Radio = c.Radio;
            existing.Altura = c.Altura;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var c = await _context.Cilindros.FindAsync(id);
            if (c == null) return false;
            _context.Cilindros.Remove(c);
            await _context.SaveChangesAsync();
            return true;
        }
        public IEnumerable<Cilindro> GetPaged(int pageNumber, int pageSize)
        {
            return _context.Cilindros.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
} 