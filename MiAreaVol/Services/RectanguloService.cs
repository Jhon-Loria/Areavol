using System.Collections.Generic;
using System.Linq;
using MiAreaVol.Models;

namespace MiAreaVol.Services
{
    public class RectanguloService
    {
        private static List<Rectangulo> _rectangulos = new();
        private static int _nextId = 1;

        public IEnumerable<Rectangulo> GetAll() => _rectangulos;
        public Rectangulo GetById(int id) => _rectangulos.FirstOrDefault(r => r.Id == id);
        public Rectangulo Create(Rectangulo r)
        {
            r.Id = _nextId++;
            _rectangulos.Add(r);
            return r;
        }
        public bool Update(int id, Rectangulo r)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            existing.Largo = r.Largo;
            existing.Ancho = r.Ancho;
            return true;
        }
        public bool Delete(int id)
        {
            var r = GetById(id);
            if (r == null) return false;
            _rectangulos.Remove(r);
            return true;
        }
        public IEnumerable<Rectangulo> GetPaged(int pageNumber, int pageSize)
        {
            return _rectangulos.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
} 