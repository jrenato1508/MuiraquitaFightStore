using MuiraquitaFightStore.Core.Messages.CommonMessages.DomaninEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Catalogo.Domain.Events
{
    public class ProdutoAbaixoEstoqueEvent : DomaninEvent
    {
        public int QuantidadeRestante { get; set; }
        public ProdutoAbaixoEstoqueEvent(Guid aggregateId, int quantidadeRestante) : base(aggregateId)
        {
            QuantidadeRestante = quantidadeRestante;
        }
    }
}
