using System.Collections.Generic;
using System.Threading.Tasks;
using MiAreaVol.Models;
using MiAreaVol.Data;
using Microsoft.EntityFrameworkCore;

namespace MiAreaVol.Services
{
    public class TrianguloService
    {
        private readonly MiAreaVolContext _context;
        public TrianguloService(MiAreaVolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Triangulo>> GetAllAsync() => await _context.Triangulos.ToListAsync();
        public async Task<Triangulo?> GetByIdAsync(int id) => await _context.Triangulos.FindAsync(id);
        public async Task<Triangulo> CreateAsync(Triangulo t)
        {
            _context.Triangulos.Add(t);
            await _context.SaveChangesAsync();
            return t;
        }
        public async Task<bool> UpdateAsync(int id, Triangulo t)
        {
            var existing = await _context.Triangulos.FindAsync(id);
            if (existing == null) return false;
            existing.Base = t.Base;
            existing.Altura = t.Altura;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var t = await _context.Triangulos.FindAsync(id);
            if (t == null) return false;
            _context.Triangulos.Remove(t);
            await _context.SaveChangesAsync();
            return true;
        }
        public IEnumerable<Triangulo> GetPaged(int pageNumber, int pageSize)
        {
            return _context.Triangulos.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
} 