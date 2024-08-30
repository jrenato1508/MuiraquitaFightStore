using MuiraquitaFightStore.Core.DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Catalogo.Domain.Entitys
{
    public class Categoria : Entity
    {
        public string Nome { get; private set; }

        public int Codigo { get; private set; }


        public Categoria(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;
        }


        protected Categoria() { }

        public override string ToString()
        {
            return $"{Nome} - {Codigo}";
        }
    }
}
