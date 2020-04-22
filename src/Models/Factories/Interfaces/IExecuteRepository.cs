using Microsoft.EntityFrameworkCore.Storage;
using Models;

namespace Factories.Services
{
    public interface IExecuteRepository
    {
        IDbContextTransaction BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
        int SaveAll();
        bool DiscardAllChange();
    }
}
