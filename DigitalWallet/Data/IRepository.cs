using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalWallet.Data
{
    internal interface IRepository<T, TId> where T : class
    {
        T Get(TId id);

        List<T> GetAll();

        void Add(T entity);
    }
}
