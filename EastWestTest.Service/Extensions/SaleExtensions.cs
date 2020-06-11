using EastWestTest.Repository.Entities;
using EastWestTest.Service.Models;
using System.Collections.Generic;
using System.Linq;

namespace EastWestTest.Service.Extensions
{
    public static class SaleExtensions
    {
        public static SaleInfo SaleEntityToSaleInfo(this SaleEntity entity) =>
            new SaleInfo()
            {
                Id = entity.Id,
                CreateDate = entity.CreateDate,
                Client = entity.Client.ClientEntityToClient()
            };

        public static List<Sale> SaleEntityToSale(this ICollection<SaleEntity> entityCollection) =>
            entityCollection.Select(y => new Sale()
            {
                Id = y.Id,
                CreateDate = y.CreateDate,
            }).ToList();
    }
}
