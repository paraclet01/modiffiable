using System;
using System.Collections.Generic;
using System.Text;

namespace OPTICIP.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
