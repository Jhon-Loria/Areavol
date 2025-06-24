using System.Collections.Generic;
using System.Linq;
using MiAreaVol.Models;

namespace MiAreaVol.Services
{
    public class CuadradoService
    {
        private static List<Cuadrado> _cuadrados = new();
        private static int _nextId = 1;

        public IEnumerable<Cuadrado> GetAll() => _cuadrados;
        public Cuadrado GetById(int id) => _cuadrados.FirstOrDefault(c => c.Id == id);
        public Cuadrado Create(Cuadrado c)
        {
            c.Id = _nextId++;
            _cuadrados.Add(c);
            return c;
        }
        public bool Update(int id, Cuadrado c)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            existing.Lado = c.Lado;
            return true;
        }
        public bool Delete(int id)
        {
            var c = GetById(id);
            if (c == null) return false;
            _cuadrados.Remove(c);
            return true;
        }
    }
} 