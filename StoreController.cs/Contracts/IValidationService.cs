using Data;
using System.Collections;
using System.Collections.Generic;

namespace BusinessLogic.Contracts
{
    public interface IValidationService<T> where T : BaseStoreEntity
    {
        void Validate(IEnumerable<T> records);
    }
}