using EastWestTest.Repository.Entities;
using EastWestTest.Service.Models;

namespace EastWestTest.Service.Extensions
{
    public static class ClientExtensions
    {
        public static ClientInfo ClientEntityToClientInfo(this ClientEntity entity) =>
            new ClientInfo()
            {
                Id = entity.Id,
                Surname = entity.Surname,
                CreateDate = entity.CreateDate,
                Sales = entity.Sales.SaleEntityToSale()
            };

        public static Client ClientEntityToClient(this ClientEntity entity) =>
            new Client()
            {
                Id = entity.Id,
                Surname = entity.Surname,
                CreateDate = entity.CreateDate,
            };
    }
}
