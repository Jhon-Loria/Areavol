using System.Collections.Generic;
using System.Threading.Tasks;
using MiAreaVol.Models;
using MiAreaVol.Data;
using Microsoft.EntityFrameworkCore;

namespace MiAreaVol.Services
{
    public class CuadradoService
    {
        private readonly MiAreaVolContext _context;
        public CuadradoService(MiAreaVolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cuadrado>> GetAllAsync() => await _context.Cuadrados.ToListAsync();
        public async Task<Cuadrado?> GetByIdAsync(int id) => await _context.Cuadrados.FindAsync(id);
        public async Task<Cuadrado> CreateAsync(Cuadrado c)
        {
            _context.Cuadrados.Add(c);
            await _context.SaveChangesAsync();
            return c;
        }
        public async Task<bool> UpdateAsync(int id, Cuadrado c)
        {
            var existing = await _context.Cuadrados.FindAsync(id);
            if (existing == null) return false;
            existing.Lado = c.Lado;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var c = await _context.Cuadrados.FindAsync(id);
            if (c == null) return false;
            _context.Cuadrados.Remove(c);
            await _context.SaveChangesAsync();
            return true;
        }
        public IEnumerable<Cuadrado> GetPaged(int pageNumber, int pageSize)
        {
            return _context.Cuadrados.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
} 