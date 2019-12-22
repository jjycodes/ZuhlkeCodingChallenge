using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public abstract class IDataSource
    {
        public string Name { get; set; }
        public bool HasHeaderRecord { get; set; }
        
        //Other essential fields that abstracts any datasource and its data - Type, Delimeter, trim options, etc
        //public string Path { get; set; } = string.Empty;
    }
    
}
