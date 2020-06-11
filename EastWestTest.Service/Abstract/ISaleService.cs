using EastWestTest.Service.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EastWestTest.Service.Abstract
{
    public interface ISaleService
    {
        Task<List<SaleInfo>> GetSalesByDateAsync(DateTime from, DateTime before);
    }
}
