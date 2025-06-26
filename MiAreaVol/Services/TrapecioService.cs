using System.Collections.Generic;
using System.Threading.Tasks;
using MiAreaVol.Models;
using MiAreaVol.Data;
using Microsoft.EntityFrameworkCore;

namespace MiAreaVol.Services
{
    public class TrapecioService
    {
        private readonly MiAreaVolContext _context;
        public TrapecioService(MiAreaVolContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trapecio>> GetAllAsync() => await _context.Trapecios.ToListAsync();
        public async Task<Trapecio?> GetByIdAsync(int id) => await _context.Trapecios.FindAsync(id);
        public async Task<Trapecio> CreateAsync(Trapecio t)
        {
            _context.Trapecios.Add(t);
            await _context.SaveChangesAsync();
            return t;
        }
        public async Task<bool> UpdateAsync(int id, Trapecio t)
        {
            var existing = await _context.Trapecios.FindAsync(id);
            if (existing == null) return false;
            existing.BaseMayor = t.BaseMayor;
            existing.BaseMenor = t.BaseMenor;
            existing.Altura = t.Altura;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var t = await _context.Trapecios.FindAsync(id);
            if (t == null) return false;
            _context.Trapecios.Remove(t);
            await _context.SaveChangesAsync();
            return true;
        }
        public IEnumerable<Trapecio> GetPaged(int pageNumber, int pageSize)
        {
            return _context.Trapecios.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
} 