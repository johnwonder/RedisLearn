using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRedisImpl
{
    public  class PoolManager
    {
        //private List<RedisServer>
    }

    internal class RedisServer
    {
        internal    long Age
        {
            get { return Age; }
            set
            {
                if (value >= int.MaxValue)
                    Age = 0;
            }
        }

        internal bool IsMaster { get; set; }


        public RedisServer(ServerConfig config)
        {
            IsMaster = config.IsMaster;
        }
    }
}
