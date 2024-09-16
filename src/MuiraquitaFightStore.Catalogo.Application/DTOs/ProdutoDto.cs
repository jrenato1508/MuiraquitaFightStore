
using System.ComponentModel.DataAnnotations;


namespace MuiraquitaFightStore.Catalogo.Application.DTOs
{
    public class ProdutoDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid CategoriaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataCadastro { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Imagem { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int QuantidadeEstoque { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string TamanhoNumeracao { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Cor {  get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string TamanhoCamisa { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string TamanhoShort { get; set; }

        [Range(40, int.MaxValue, ErrorMessage = "O campo {0} precisa ter o valor mínimo de {1}")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Peso { get; private set; }

        // Como produto possui uma categoria vamos precisar listar essas categorias então vamos carregar essa lista aqui dentro
        public IEnumerable<CategoriaDto>? Categorias { get; set; }
    }
}
