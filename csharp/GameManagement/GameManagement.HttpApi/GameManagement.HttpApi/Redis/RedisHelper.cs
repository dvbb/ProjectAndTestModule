using CSRedis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System.Collections.Concurrent;

namespace GameManagement.HttpApi.Redis
{
    public class RedisHelper
    {
        private readonly RedisOption _option;
        private readonly CSRedisClient _client;
        private readonly ConcurrentDictionary<string, ConnectionMultiplexer> _connections;

        public RedisHelper(RedisOption option)
        {
            _option = option;
            _client = new CSRedisClient("127.0.0.1:6379");
            _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
        }

        private ConnectionMultiplexer GetConnection()
        {
            return _connections.GetOrAdd(_option.InstanceName,
                p => ConnectionMultiplexer.Connect(_option.Connection));
        }

        public IDatabase GetDatabase()
        {
            return GetConnection().GetDatabase(_option.DefaultDb);
        }

        public void Set(string key,string value)
        {
            _client.Set(key, value);
        }
    }
}
