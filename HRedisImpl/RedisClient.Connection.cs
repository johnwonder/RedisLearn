using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRedisImpl
{
    public partial  class RedisClient
    {
        public bool Ping()
        {
            return Execute(RedisCommand.PING).ToString().Equals(ReplyFormat.PingSuccess);
        }

        public bool Select()
        {
            return Execute(RedisCommand.SELECT).ToString().Equals(ReplyFormat.ReplySuccess);
        }
    }
}
