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

        public string TamanhoCalcaEVagui { get; private set; }

        public string Peso { get; private set; }

        public Tamanho(string numeracao, string tamanhoCalcaVagui, string peso)
        {
            TamanhoNumeracao = numeracao;
            TamanhoCalcaEVagui = tamanhoCalcaVagui;
            Peso = peso;

            Validar();
        }

        private void Validar()
        {
            Validacoes.ValidarSeVazio(TamanhoNumeracao, "O campo Numeração não pode estar vario");
            Validacoes.ValidarSeVazio(TamanhoCalcaEVagui, "O campo Tamanho da Calça e do Vagui não pode estar vario");
            Validacoes.ValidarSeVazio(Peso, "O campo Peso não pode estar vazio");
        }
    }

}

public enum TamanhoNumeracao
{
    Nenhum = 0,
    A0,
    A1,
    A2,
    A3,
    A4,
    A5,
    A6,

}
