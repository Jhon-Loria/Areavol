using System.Collections.Generic;
using System.Linq;
using MiAreaVol.Models;

namespace MiAreaVol.Services
{
    public class CilindroService
    {
        private static List<Cilindro> _cilindros = new();
        private static int _nextId = 1;

        public IEnumerable<Cilindro> GetAll() => _cilindros;
        public Cilindro GetById(int id) => _cilindros.FirstOrDefault(c => c.Id == id);
        public Cilindro Create(Cilindro c)
        {
            c.Id = _nextId++;
            _cilindros.Add(c);
            return c;
        }
        public bool Update(int id, Cilindro c)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            existing.Radio = c.Radio;
            existing.Altura = c.Altura;
            return true;
        }
        public bool Delete(int id)
        {
            var c = GetById(id);
            if (c == null) return false;
            _cilindros.Remove(c);
            return true;
        }
        public IEnumerable<Cilindro> GetPaged(int pageNumber, int pageSize)
        {
            return _cilindros.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
} 