using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisImpl
{
    public  class Configuration
    {
        public string Host { get; set; }
        public int Port { get; set; }

        /// <summary>
        /// Socket 是否正在使用 Nagle算法
        /// </summary>
        public bool NoDelaySocket { get; set; }

        public Configuration()
        {
            Host = "localhost";
            Port = 6379;
            NoDelaySocket = false;
        }
    }
}
