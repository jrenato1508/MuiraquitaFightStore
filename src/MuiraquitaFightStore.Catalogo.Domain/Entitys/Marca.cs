using MuiraquitaFightStore.Core.DomainObject;
using MuiraquitaFightStore.Core.DomainObject.AssertionConcem;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Catalogo.Domain.Entitys
{
    public class Marca : Entity
    {
        public string Nome { get; private set; }

        public int Codigo { get; private set; }

        public IEnumerable<Produto> Produtos { get; set; }

        public Marca( string nome, int codigo)
        {
            Nome= nome;
            Codigo = codigo;
            Validar();
        }

        public override string ToString()
        {
            return $"{Nome} - {Codigo}";
        }

        public void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome da categoria não pode estar vazio");
            Validacoes.ValidarSeIgual(Codigo, 0, "O campo Codigo não pode ser 0");
        }
    }
}
