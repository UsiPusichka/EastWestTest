using EastWestTest.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EastWestTest.Service.Abstract
{
    public interface IClientService
    {
        Task<List<ClientInfo>> GetAllClientsInfoAsync();
        Task<ClientInfo> GetClientInfoByIdAsync(int clientId);
    }
}
