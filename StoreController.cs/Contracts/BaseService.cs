using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic.Contracts
{
    public abstract class BaseService
    {
        protected readonly IDataContextFactory _dataContextFactory;
        protected IList<Log> _errors;

        protected BaseService(IDataContextFactory dataContextFactory)
        {
            _dataContextFactory = dataContextFactory;
        }

        public bool HasErrors => _errors.Any();
        public IEnumerable<Log> Errors => _errors;
    }
}