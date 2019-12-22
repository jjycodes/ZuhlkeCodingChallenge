using Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Contracts
{
    public interface IDatasourceReader<T> where T : BaseStoreEntity
    {
        IEnumerable<T> Read(IDataSource dataSource);
    }
}
