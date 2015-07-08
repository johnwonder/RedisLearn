using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beetle.Redis;

namespace BettleRedis
{
    public class  StringsGetter
    {
        //static RedisClient redisClient = new RedisClient("127.0.0.1", 6379);
        public static void GetStrings(RedisClient redisClient)
        {
            //string[] keys = new string[] { "_test1","_test2","_test3" };
            //string[] values = new string[] { "_test1","_test2","_test3" };

            //for (int i = 0; i < keys.Length; i++)
            //{
            //    redisClient.
            //}

        }

        public static void JsonList_ToList()
        {
            JsonList<UserBase> sl = new JsonList<UserBase>("json_list");
            sl.Clear();
            UserBase[] values = new UserBase[] { 
                new UserBase{ Name="a" },
                new UserBase { Name="b" },
                new UserBase { Name="c" },
                new UserBase { Name ="D" }
            };

            foreach (UserBase item in values)
            {
                sl.Add(item);
            }

           
            //IList<UserBase> result = 
        }
    }
}
