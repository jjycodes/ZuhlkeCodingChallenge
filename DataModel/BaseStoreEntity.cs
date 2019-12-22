using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public abstract class BaseStoreEntity
    {
        public int Id { get; set; }
        public bool IsValid { get; set; }
        public int LineNumber { get; set; }
    }
}
