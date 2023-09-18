using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.StackExchange
{
    public static class RedisConnector
    {
        public static ConnectionMultiplexer Connection { get; set; }
    }
}
