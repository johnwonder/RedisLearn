using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HRedisImpl;
using System.Diagnostics;

namespace HRedisTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Info_Test()
        {
                  string MasterIp = "127.0.0.1";
                int MasterPort = 6379;
            using (var rcClient = new RedisClient(MasterIp,MasterPort))
            {
                var info = rcClient.Info;
                foreach (var item in info)
                {
                    Debug.Write(item.Key + ":" + item.Value + "\r\n");
                }
            }
        }
    }
}
