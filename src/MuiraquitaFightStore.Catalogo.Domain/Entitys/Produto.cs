using MuiraquitaFightStore.Core.DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Catalogo.Domain.Entitys
{
    public class Produto : Entity, IAggregateRoot
    {
        public Guid CategoriaId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Imagem { get; private set; }
        public int QuantidadeEstoque { get; private set; }
        public string tamanho { get; private set; }
        //public Dimensoes Dimensoes { get; private set; }
        public Categoria Categoria { get; private set; }

        public Produto(string nome, string descricao, bool ativo, decimal valor, Guid categoriaId, DateTime datacadastro, string imagem)
        {
            CategoriaId = categoriaId;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            DataCadastro = datacadastro;
            Imagem = imagem;

            //Validar();
        }

        protected Produto(){}

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void AlterarCategoria(Categoria NovaCategoria)
        {
            Categoria = NovaCategoria;
            CategoriaId = NovaCategoria.Id;
        }


        public void AlterarDescricao(string NovaDescricao)
        {
            Descricao = NovaDescricao;
        }


        public void ReporEstoque(int quantidade)
        {
            QuantidadeEstoque += quantidade;
        }


        public bool PossuiEstoque(int quantidade)
        {
            return QuantidadeEstoque >= quantidade;
        }


    }
}
