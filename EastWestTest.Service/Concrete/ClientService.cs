using EastWestTest.Repository.Abstract;
using EastWestTest.Repository.Entities;
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
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork unitOfWork;

        public ClientService(IUnitOfWork unitOfWorkArg)
        {
            unitOfWork = unitOfWorkArg;
        }

        public async Task<List<ClientInfo>> GetAllClientsInfoAsync()
        {
            return await unitOfWork.Clients.Include(c => c.Sales)
                .AsNoTracking()
                .Select(x => x.ClientEntityToClientInfo())
                .ToListAsync();
        }

        public async Task<ClientInfo> GetClientInfoByIdAsync(int clientId)
        {
            if (clientId <= 0)
                throw new ArgumentException(nameof(clientId));

            ClientEntity clientEntity = await unitOfWork.Clients.Include(c => c.Sales)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == clientId);

            if(clientEntity != null)
                return clientEntity.ClientEntityToClientInfo();

            return null;
        }
    }
}
