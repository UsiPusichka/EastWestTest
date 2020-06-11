using EastWestTest.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace EastWestTest.Repository.Abstract
{
    public interface IUnitOfWork
    {
        DbSet<ClientEntity> Clients { get; set; }
        DbSet<SaleEntity> Sales { get; set; }
    }
}
