using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRedisImpl
{
    public enum RedisCommand
    {
        GET,
        INFO,
        SET,
        EXPIRE,
        MULTI,
        EXEC,
        QUIT,
        SUBSCRIBE,
        UNSUBSCRIBE,
        PSUBSCRIBE,
        PUNSUBSCRIBE,
        PUBLISH,
        PUBSUB,
        AUTH,
        PING,
        DBSIZE,
        DEL,
        SELECT
    }
}
