using System.Collections.Generic;
using System.Linq;
using MiAreaVol.Models;

namespace MiAreaVol.Services
{
    public class TrapecioService
    {
        private static List<Trapecio> _trapecios = new();
        private static int _nextId = 1;

        public IEnumerable<Trapecio> GetAll() => _trapecios;
        public Trapecio GetById(int id) => _trapecios.FirstOrDefault(t => t.Id == id);
        public Trapecio Create(Trapecio t)
        {
            t.Id = _nextId++;
            _trapecios.Add(t);
            return t;
        }
        public bool Update(int id, Trapecio t)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            existing.BaseMayor = t.BaseMayor;
            existing.BaseMenor = t.BaseMenor;
            existing.Altura = t.Altura;
            return true;
        }
        public bool Delete(int id)
        {
            var t = GetById(id);
            if (t == null) return false;
            _trapecios.Remove(t);
            return true;
        }
        public IEnumerable<Trapecio> GetPaged(int pageNumber, int pageSize)
        {
            return _trapecios.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
} 