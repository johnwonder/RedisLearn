﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRedisImpl
{
    public  class RedisSentinel:IDisposable
    {
        private RedisClient client;

        public RedisSentinel(RedisConfig config)
        {
            client = new RedisClient(config);
        }

        public RedisSentinel(string ip,int port):this(new RedisConfig()
        {
            Host = ip,
            Port = port
        })
        {

        }

        public bool Ping()
        {
            try
            {
                return client.Ping();

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public bool PingRaw()
        {
            return client.Ping();
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
