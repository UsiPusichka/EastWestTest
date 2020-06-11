using EastWestTest.Repository.Abstract;
using EastWestTest.Service.Abstract;
using EastWestTest.Service.Extensions;
using EastWestTest.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EastWestTest.Service.Concrete
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork unitOfWork;

        public SaleService(IUnitOfWork unitOfWorkArg)
        {
            unitOfWork = unitOfWorkArg;
        }

        public async Task<List<SaleInfo>> GetSalesByDateAsync(DateTime from, DateTime before)
        {
            if (from > before)
                throw new ArgumentException("Date from more than date before");

            DateTime fromDateFirstSecond = from.Date;
            DateTime beforeDateLastSecond = before.Date;

            fromDateFirstSecond = from.Date;
            beforeDateLastSecond = before.AddDays(1).AddSeconds(-1);

            return await unitOfWork.Sales.Include(c => c.Client)
                .AsNoTracking()
                .Where(x => fromDateFirstSecond <= x.CreateDate && x.CreateDate <= beforeDateLastSecond)
                .Select(x => x.SaleEntityToSaleInfo())
                .ToListAsync();
        }
    }
}
