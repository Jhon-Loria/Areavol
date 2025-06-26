using System.Collections.Generic;
using System.Threading.Tasks;
using MiAreaVol.Models;
using MiAreaVol.Data;
using Microsoft.EntityFrameworkCore;

namespace MiAreaVol.Services
{
    public class RectanguloService
    {
        private readonly MiAreaVolContext _context;
        public RectanguloService(MiAreaVolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rectangulo>> GetAllAsync() => await _context.Rectangulos.ToListAsync();
        public async Task<Rectangulo?> GetByIdAsync(int id) => await _context.Rectangulos.FindAsync(id);
        public async Task<Rectangulo> CreateAsync(Rectangulo r)
        {
            _context.Rectangulos.Add(r);
            await _context.SaveChangesAsync();
            return r;
        }
        public async Task<bool> UpdateAsync(int id, Rectangulo r)
        {
            var existing = await _context.Rectangulos.FindAsync(id);
            if (existing == null) return false;
            existing.Largo = r.Largo;
            existing.Ancho = r.Ancho;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var r = await _context.Rectangulos.FindAsync(id);
            if (r == null) return false;
            _context.Rectangulos.Remove(r);
            await _context.SaveChangesAsync();
            return true;
        }
        public IEnumerable<Rectangulo> GetPaged(int pageNumber, int pageSize)
        {
            return _context.Rectangulos.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
} 