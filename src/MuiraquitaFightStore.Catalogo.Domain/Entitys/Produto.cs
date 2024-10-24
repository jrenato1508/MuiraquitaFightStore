using MuiraquitaFightStore.Catalogo.Domain.Entitys.ValueObject;
using MuiraquitaFightStore.Core.DomainObject;
using MuiraquitaFightStore.Core.DomainObject.AssertionConcem;
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
        public Guid MarcaId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string Imagem { get; private set; }
        public int QuantidadeEstoque { get; private set; }
        public string Cor { get; set; }
        public Tamanho Tamanho { get; private set; }
        public Categoria Categoria { get; private set; }
        public Marca Marca { get; private set; }


        public Produto(string nome, string descricao, bool ativo, decimal valor, Guid categoriaId, Guid marcaId, DateTime datacadastro, string imagem, Tamanho tamanho )
        {
            CategoriaId = categoriaId;
            MarcaId = marcaId;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
            Valor = valor;
            DataCadastro = datacadastro;
            Imagem = imagem;
            Tamanho = tamanho;

            Validar();
        }

        protected Produto(){}

        public void Ativar() => Ativo = true;

        public void Desativar() => Ativo = false;

        public void AlterarCategoria(Categoria NovaCategoria)
        {
            Categoria = NovaCategoria;
            CategoriaId = NovaCategoria.Id;
        }


        public void DebitarEstoque(int quantidade)
        {
            if (quantidade < 0) quantidade *= -1;
            if (!PossuiEstoque(quantidade)) throw new DomainException("Estoque insuficiente");
            QuantidadeEstoque -= quantidade;
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


        private void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "O campo Nome do produto não pode estar vazio");
            Validacoes.ValidarSeVazio(Descricao, "O campo Descrição do produto não pode estar vazio");
            Validacoes.ValidarSeIgual(CategoriaId, Guid.Empty, "O campo Categoria do produto não pode estar vazio");
            Validacoes.ValidarSeIgual(MarcaId, Guid.Empty, "O campo Categoria do produto não pode estar vazio");
            Validacoes.ValidarSeMenorQue(Valor, 1, "O campo Valor do produto não pode se menor igual a 0");
            Validacoes.ValidarSeVazio(Imagem, "O campo de imagem do produto não pode estar vazio");
        }
    }
}
