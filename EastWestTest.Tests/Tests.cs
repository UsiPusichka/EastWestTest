using EastWestTest.Repository.Context;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using EastWestTest.Repository;
using EastWestTest.Service.Abstract;
using System;
using System.Threading.Tasks;
using System.Linq;
using EastWestTest.Service.Concrete;
using EastWestTest.Repository.Abstract;

namespace EastWestTest.Tests
{
    public class Tests
    {
        private IServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDbContext<DataContext>(config =>
                config.UseInMemoryDatabase("DataContext").ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
            
            serviceCollection.AddTransient<IUnitOfWork, DataContext>();
            serviceCollection.AddTransient<IClientService, ClientService>();
            serviceCollection.AddTransient<ISaleService, SaleService>();

            serviceProvider = serviceCollection.BuildServiceProvider();

            SeedTemplate.FillForTests(serviceProvider.GetService<DataContext>());
        }

        [Test]
        public async Task GetAllClientTest()
        {
            var clientService = serviceProvider.GetService<IClientService>();

            var clients = await clientService.GetAllClientsInfoAsync();

            Assert.IsNotNull(clients);
            Assert.AreEqual(clients.Count, 10);
            Assert.AreEqual(clients.SelectMany(x => x.Sales).Count(), 30);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetClientInfoByIdTest(int clientId)
        {
            var clientService = serviceProvider.GetService<IClientService>();

            var client = await clientService.GetClientInfoByIdAsync(clientId);

            Assert.IsNotNull(client);
            Assert.AreEqual(client.Surname, $"User {clientId}");

            Assert.IsNotNull(client.Sales);
            Assert.IsTrue(client.Sales.Any());
            Assert.AreEqual(client.Sales.Count, 3);
        }

        [Test]
        [TestCase(11)]
        [TestCase(22)]
        public async Task NegativeGetClientInfoByIdTest(int clientId)
        {
            var clientService = serviceProvider.GetService<IClientService>();

            var client = await clientService.GetClientInfoByIdAsync(clientId);

            Assert.IsNull(client);
        }

        [Test]
        public async Task GetSalesByDateTest()
        {
            var clientService = serviceProvider.GetService<ISaleService>();

            var from = DateTime.UtcNow.Date;
            var before = DateTime.UtcNow.Date;

            addDays(ref from, ref before, -3, -3);

            var sales = await clientService.GetSalesByDateAsync(from, before);

            Assert.IsNotNull(sales);
            Assert.AreEqual(sales.Count, 10);

            addDays(ref from, ref before, -3, -2);

            sales = await clientService.GetSalesByDateAsync(from, before);

            Assert.IsNotNull(sales);
            Assert.AreEqual(sales.Count, 20);

            addDays(ref from, ref before, -2, -2);

            sales = await clientService.GetSalesByDateAsync(from, before);

            Assert.IsNotNull(sales);
            Assert.AreEqual(sales.Count, 10);

            addDays(ref from, ref before, -3, -1);

            sales = await clientService.GetSalesByDateAsync(from, before);

            Assert.IsNotNull(sales);
            Assert.AreEqual(sales.Count, 30);
        }

        [Test]
        public async Task NegativeGetSalesByDateTest()
        {
            var clientService = serviceProvider.GetService<ISaleService>();

            var from = DateTime.UtcNow.Date;
            var before = DateTime.UtcNow.Date;

            var sales = await clientService.GetSalesByDateAsync(from, before);

            Assert.IsEmpty(sales);
        }

        private void addDays(ref DateTime date1, ref DateTime date2, int daysForDate1, int daysForDate2)
        {
            date1 = DateTime.UtcNow.Date.AddDays(daysForDate1);
            date2 = DateTime.UtcNow.Date.AddDays(daysForDate2);
        }
    }
}