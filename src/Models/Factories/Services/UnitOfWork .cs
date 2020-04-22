using Models;
using Models.Entities;

namespace Factories.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyDbContext _dbcontext;

        public UnitOfWork(MyDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public MyDbContext Context
        {
            get => _dbcontext;
        }

    }
}
