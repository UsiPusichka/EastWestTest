using System.Collections.Generic;

namespace EastWestTest.Service.Models
{
    public class ClientInfo : Client
    {
        public List<Sale> Sales { get; set; }
    }
}
