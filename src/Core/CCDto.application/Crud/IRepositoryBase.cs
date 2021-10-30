using FreeSql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CCDto.application.Crud
{
    public interface IRepositoryBase<T, TKey> : IBaseRepository<T, TKey> where T : class
    {
        IList<T> GetAll();
    }
}
