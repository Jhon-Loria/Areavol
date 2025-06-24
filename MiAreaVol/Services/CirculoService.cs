using System.Collections.Generic;
using System.Linq;
using MiAreaVol.Models;

namespace MiAreaVol.Services
{
    public class CirculoService
    {
        private static List<Circulo> _circulos = new();
        private static int _nextId = 1;

        public IEnumerable<Circulo> GetAll() => _circulos;
        public Circulo GetById(int id) => _circulos.FirstOrDefault(c => c.Id == id);
        public Circulo Create(Circulo c)
        {
            c.Id = _nextId++;
            _circulos.Add(c);
            return c;
        }
        public bool Update(int id, Circulo c)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            existing.Radio = c.Radio;
            return true;
        }
        public bool Delete(int id)
        {
            var c = GetById(id);
            if (c == null) return false;
            _circulos.Remove(c);
            return true;
        }
    }
} 