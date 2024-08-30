using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Core.DomainObject.AssertionConcem
{
    public class Validacoes
    {
        public static void ValidarSeIgual(object object1, object object2, string mensagem)
        {
            if (object1.Equals(object2))
            {
                throw new DomainException(mensagem);
            }
        }


        public static void ValidarSeDiferente(object object1, object object2, string mensagem)
        {
            if (!object1.Equals(object2))
            {
                throw new DomainException(mensagem);
            }
        }


        public static void ValidarSeDiferente(string pattern, string valor, string mensagem)
        {
            var regex = new Regex(pattern);

            if (!regex.IsMatch(valor))
            {
                throw new DomainException(mensagem);
            }
        }


        public static void ValidarTamanho(string valor, int minimo, int maximo, string mensagem)
        {
            var tamanho = valor.Trim().Length;
            if(tamanho >maximo)
            {
                throw new DomainException(mensagem);
            }
        }


        public static void ValidarSeVazio(string valor, string mensagem)
        {
            if(valor == null || valor.Trim().Length == 0)
            {
                throw new DomainException(mensagem);
            }
        }


        public static void ValidarSeNulo(object object1, string mensagem)
        {
            if(object1 == null)
            {
                throw new DomainException(mensagem);
            }
        }


        public static void ValidarMinimoMaximo(double valor, double minimo, double maximo, string mensagem)
        {
            if(valor < minimo || valor > maximo)
            {
                throw new DomainException(mensagem);
            }
        }

                
        public static void ValidarMinimoMaximo(float valor, float minimo, float maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo)
            {
                throw new DomainException(mensagem);
            }
        }


        public static void ValidarMinimoMaximo(int valor, int minimo, int maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo)
            {
                throw new DomainException(mensagem);
            }
        }


        // Parei no ValidarMinimoMaximo
    }
}
