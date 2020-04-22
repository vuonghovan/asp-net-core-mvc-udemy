using Microsoft.EntityFrameworkCore.Storage;
using Models;
using Models.Entities;

namespace Factories.Services
{
    public interface IUnitOfWork
    {
        MyDbContext Context { get; }
    }
}
