using FreeSql;
using System.Collections.Generic;

namespace CCDto.application.Crud
{
    public class RepositoryBase<T, TKey> : BaseRepository<T, TKey> where T : class
    {
        protected static IBaseRepository<T> _dbRepository;

        public RepositoryBase(IFreeSql fsql) : base(fsql, null, null)
        {
            _dbRepository = fsql.GetRepository<T, TKey>();
        }


        public List<T> GetAll()
        {
            return _dbRepository.Where(o => 1 == 1).ToList();
        }
    }
}
