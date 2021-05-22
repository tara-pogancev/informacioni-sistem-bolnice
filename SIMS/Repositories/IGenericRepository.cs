using System;
using System.Collections.Generic;
using System.Text;

namespace SIMS.Repositories
{
    interface IGenericRepository<T,ID>
    {
        List<T> GetAll();
        void Update(T entity);
        void Delete(ID key);
        void Save(T entity);
        T FindById(ID key);
    }
}
