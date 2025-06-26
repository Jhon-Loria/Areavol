using System.Collections.Generic;
using System.Threading.Tasks;
using MiAreaVol.Models;
using MiAreaVol.Data;
using Microsoft.EntityFrameworkCore;

namespace MiAreaVol.Services
{
    public class CirculoService
    {
        private readonly MiAreaVolContext _context;
        public CirculoService(MiAreaVolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Circulo>> GetAllAsync() => await _context.Circulos.ToListAsync();
        public async Task<Circulo?> GetByIdAsync(int id) => await _context.Circulos.FindAsync(id);
        public async Task<Circulo> CreateAsync(Circulo c)
        {
            _context.Circulos.Add(c);
            await _context.SaveChangesAsync();
            return c;
        }
        public async Task<bool> UpdateAsync(int id, Circulo c)
        {
            var existing = await _context.Circulos.FindAsync(id);
            if (existing == null) return false;
            existing.Radio = c.Radio;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var c = await _context.Circulos.FindAsync(id);
            if (c == null) return false;
            _context.Circulos.Remove(c);
            await _context.SaveChangesAsync();
            return true;
        }
        public IEnumerable<Circulo> GetPaged(int pageNumber, int pageSize)
        {
            return _context.Circulos.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
} 