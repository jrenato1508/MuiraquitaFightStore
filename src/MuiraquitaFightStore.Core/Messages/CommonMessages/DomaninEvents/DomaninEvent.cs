using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Core.Messages.CommonMessages.DomaninEvents
{
    public class DomaninEvent: Event
    {
        public DomaninEvent( Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
// Parei no 7.7