using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRedisImpl
{
    public sealed partial class RedisClient : RedisBaseClient
    {
        public IJsonConvert RJsonConvert { get; set; }

        public RedisClient(RedisConfig configuration)
            : base(configuration)
        {
            RJsonConvert = Configuration.JsonConvert ?? new JsonConvert();
            _configuration = configuration;
        }

        private RedisConfig _configuration;

        public RedisClient(string ip, int port)
            : this(new RedisConfig()
            {

                Host = ip,
                Port = port,
                ReceiveTimeout = 60,
                SendTimeout = 60
            })
        {

        }

        public int DelKey(string key)
        {
            int nums;
            var reply = Execute(RedisCommand.DEL, key).ToString();
            if (int.TryParse(reply, out nums))
                return nums;
            throw new Exception(reply);
        }
    }
}
