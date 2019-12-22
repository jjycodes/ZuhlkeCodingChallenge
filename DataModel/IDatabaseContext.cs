using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IDatabaseContext : IDisposable
    {
        DbSet<Sales> Sales { get; set; }
        int SaveChanges();
    }
}
