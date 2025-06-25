using System.Collections.Generic;
using System.Linq;
using MiAreaVol.Models;

namespace MiAreaVol.Services
{
    public class TrianguloService
    {
        private static List<Triangulo> _triangulos = new();
        private static int _nextId = 1;

        public IEnumerable<Triangulo> GetAll() => _triangulos;
        public Triangulo GetById(int id) => _triangulos.FirstOrDefault(t => t.Id == id);
        public Triangulo Create(Triangulo t)
        {
            t.Id = _nextId++;
            _triangulos.Add(t);
            return t;
        }
        public bool Update(int id, Triangulo t)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            existing.Base = t.Base;
            existing.Altura = t.Altura;
            return true;
        }
        public bool Delete(int id)
        {
            var t = GetById(id);
            if (t == null) return false;
            _triangulos.Remove(t);
            return true;
        }
        public IEnumerable<Triangulo> GetPaged(int pageNumber, int pageSize)
        {
            return _triangulos.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
} 