using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class DataContextFactory : IDataContextFactory
    {
        private readonly ZuhlkeDBContextOptions _options;

        public DataContextFactory(ZuhlkeDBContextOptions options)
        {
            _options = options;
        }

        public IDatabaseContext Create()
        {
            return new ZuhlkeSQLDBContext(_options);
        }
    }
}
