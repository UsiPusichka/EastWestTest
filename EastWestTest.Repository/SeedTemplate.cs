using EastWestTest.Repository.Context;
using EastWestTest.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EastWestTest.Repository
{
    public class SeedTemplate
    {
        public static void Fill(DataContext dataContext)
        {
            if(!dataContext.Clients.Any())
            {
                var clientList = new List<ClientEntity>();
                for(int i = 1; i < 11; i++)
                {
                    var client = new ClientEntity()
                    {
                        Surname = $"User {i}",
                        CreateDate = DateTime.UtcNow.AddDays(-6)
                    };

                    for(int j = 1; j < 4; j++)
                    {
                        client.Sales.Add(new SaleEntity()
                        {
                            CreateDate = DateTime.UtcNow.AddDays(-j),
                        });
                    }
                    clientList.Add(client);
                }
                dataContext.Clients.AddRange(clientList);
                dataContext.SaveChanges();
            }
        }

        public static void FillForTests(DataContext dataContext)
        {
            if (!dataContext.Clients.Any())
            {
                var clientList = new List<ClientEntity>();
                for (int i = 1; i < 11; i++)
                {
                    var client = new ClientEntity()
                    {
                        Surname = $"User {i}",
                        CreateDate = DateTime.UtcNow.AddDays(-6)
                    };

                    for (int j = 1; j < 4; j++)
                    {
                        client.Sales.Add(new SaleEntity()
                        {
                            CreateDate = DateTime.UtcNow.AddDays(-j),
                        });
                    }
                    clientList.Add(client);
                }
                dataContext.Clients.AddRange(clientList);
                dataContext.SaveChanges();
            }
        }
    }
}
