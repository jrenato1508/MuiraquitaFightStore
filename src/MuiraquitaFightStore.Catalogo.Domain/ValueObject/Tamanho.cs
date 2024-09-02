﻿using MuiraquitaFightStore.Core.DomainObject.AssertionConcem;
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

        public string TamanhoCamisaEVagui { get; private set; }

        public string TamanhoCalcaShort { get; private set; }

        public string Peso { get; private set; }

        public Tamanho(string tamanhoNumeracao, string tamanhoCamisaEVagui, string tamanhoCalcaShort, string peso)
        {
            TamanhoNumeracao = tamanhoNumeracao;
            TamanhoCamisaEVagui = tamanhoCamisaEVagui;
            TamanhoCalcaShort = tamanhoCalcaShort;
            Peso = peso;

            Validar();
        }

        private void Validar()
        {
            Validacoes.ValidarSeVazio(TamanhoNumeracao, "O campo Numeração não pode estar vario");
            Validacoes.ValidarSeVazio(TamanhoCamisaEVagui, "O campo Tamanho da Camisa não pode estar vario");
            Validacoes.ValidarSeVazio(TamanhoCalcaShort, "O campo Tamanho da Calça  não pode estar vario");
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
