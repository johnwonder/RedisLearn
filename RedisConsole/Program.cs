using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;

namespace RedisConsole
{
    class Program
    {
        static RedisClient redisClient = new RedisClient("127.0.0.1",6379);
        static void Main(string[] args)
        {
            //if (redisClient.IsSocketConnected())
            //{
            //    Console.WriteLine("Connected");
            //}

            redisClient.Set<string>("name", "country");
            Console.WriteLine(redisClient.Get<string>("name"));
            Console.ReadKey();
        }
    }
}
