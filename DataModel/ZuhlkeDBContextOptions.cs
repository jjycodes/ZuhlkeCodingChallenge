using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ZuhlkeDBContextOptions
    {
        public readonly DbContextOptions<ZuhlkeSQLDBContext> Options;
        public ZuhlkeDBContextOptions(DbContextOptions<ZuhlkeSQLDBContext> options)
        {
            Options = options;
        }
    }
}
