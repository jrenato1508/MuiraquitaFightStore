using MuiraquitaFightStore.Core.DomainObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuiraquitaFightStore.Core.Data
{
    public interface IRepository <T> : IDisposable where T: IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
