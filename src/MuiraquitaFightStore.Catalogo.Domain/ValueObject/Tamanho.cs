using MuiraquitaFightStore.Core.DomainObject.AssertionConcem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Catalogo.Domain.ValueObject
{
    public class Tamanho
    {
        public string TamanhoNumeracao { get; private set; }

        public string TamanhoCamisa { get; private set; }

        public string TamanhoShort { get; private set; }

        public string Peso { get; private set; }

        public Tamanho(string tamanhoNumeracao, string tamanhoCamisaEVagui, string tamanhoCalcaShort, string peso)
        {
            TamanhoNumeracao = tamanhoNumeracao;
            TamanhoCamisa = tamanhoCamisaEVagui;
            TamanhoShort = tamanhoCalcaShort;
            Peso = peso;

            Validar();
        }

        private void Validar()
        {
            Validacoes.ValidarSeVazio(TamanhoNumeracao, "O campo Numeração não pode estar vario");
            Validacoes.ValidarSeVazio(TamanhoCamisa, "O campo Tamanho da Camisa não pode estar vario");
            Validacoes.ValidarSeVazio(TamanhoShort, "O campo Tamanho da Calça  não pode estar vario");
            Validacoes.ValidarSeVazio(Peso, "O campo Peso não pode estar vazio");
        }
    }

}


