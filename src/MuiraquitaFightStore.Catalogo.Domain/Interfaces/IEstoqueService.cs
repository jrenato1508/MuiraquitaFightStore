using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Catalogo.Domain.Interfaces
{
    public interface IEstoqueService : IDisposable
    {
        Task<bool> DebitarEstoque(Guid produtoId, int quantidade);

        Task<bool> ReporEstoque(Guid produtoId, int quantidade);
    }
}
