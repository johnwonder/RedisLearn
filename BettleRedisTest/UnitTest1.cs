using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Beetle.Redis;

namespace BettleRedisTest
{
    /// <summary>
    /// http://www.cnblogs.com/smark/p/3476596.html
    /// http://my.oschina.net/ikende/blog/152082
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LST_POP_PUSH()
        {
            ProtobufList<UserBase1> list = "USERS";//new ProtobufList<UserBase>();
            list.Push(new UserBase1 { Name ="john",Age =10 ,City ="wx", Country ="cn" });
            Assert.AreEqual("john", list.Pop().Name);

        }
        [TestMethod]
        public void LST_REMOVE_ADD()
        {
            ProtobufList<UserBase1> lst = "USERS";
            lst.Add(new UserBase1 { Name ="john",Age =10, City ="wx", Country ="zc" });
            lst.Add(new UserBase1 { Name = "bbq", Age = 19, City = "gz", Country = "us" });
            Assert.AreEqual("bbq", lst.Remove().Name);
        }
        //private static Beetle.Redis.RedisClient db = new Beetle.Redis.RedisClient("redisClientSection");
        [TestMethod]
        public void LST_LENGTH()
        {
            ProtobufList<UserBase1> lst = "USERS";
            lst.Clear();
            lst.Add(new UserBase1 { Name = "john", Age = 19, City = "wx", Country = "zn" });
            lst.Add(new UserBase1 { Name = "bbq", Age = 19, City = "wx", Country = "zn" });
            Assert.AreEqual(lst.Count(), 2);
        }

        [TestMethod]
        public void LST_Region()
        {
            ProtobufList<UserBase1> lst = "USERS";
            lst.Clear();
            for (int i = 0; i < 10; i++)
            {
                lst.Add(new UserBase1 { Name = "john" + i, Age = 19, City = "wx", Country = "zc" });
            }

            IList<UserBase1> items = lst.Range();
            Assert.AreEqual(items[0].Name, "john0");
            Assert.AreEqual(items[9].Name, "john9");
            items = lst.Range(5, 7);
            Assert.AreEqual(items[0].Name, "john5");
            Assert.AreEqual(items[2].Name, "john7");
        }

        [TestMethod]
        public void Set_Get_Json()
        {
            //string key = "get_set_json";
            UserBase1 user = new UserBase1();
            user.Country = "cn";
            user.Name = "john";
            user.City = "wx";

            //ProtobufList<UserBase1> proList = new ProtobufList<UserBase1>();
            //proList.Add()

            ProtobufKey k = new ProtobufKey("ss");//还必须加上key的名字啊
            k.Set(user);
            UserBase1 us = k.Get<UserBase1>();
            Assert.AreEqual("john", us.Name);
        }

        [TestMethod]
        public void MapSetdRemove()
        {
            JsonMapSet map = "john_info";
            UserBase ub = new UserBase();
            ub.Name = "john";
            ub.City = "wx";
            ub.Country = "cn";
            ub.Age = 10;
            Contract contract = new Contract();
            contract.Email = "john@16.com";
            contract.QQ = "23423";
            contract.Phone = "2322342";
            map.Set(ub, contract);
            map.Remove<Contract>();

            contract = map.Get<Contract>();
            Assert.AreEqual(null, contract);

        }

        [TestMethod]
        public void MapSet()
        {
            JsonMapSet map = "john_info";
            UserBase ub = new UserBase();
            ub.Name = "johnwonder";
            ub.City = "wx";
            ub.Country = "cn";
            ub.Age = 10;

            Contract contract = new Contract();
            contract.Email = "john@163.com";
            contract.QQ = "230522";
            contract.Phone = "13353454";
            map.Set(ub, contract);
            IList<object> data = map.Get<UserBase, Contract>();
            Assert.AreEqual(ub.Name, ((UserBase)data[0]).Name);
            Assert.AreEqual(contract.Phone, ((Contract)data[1]).Phone);
        }

        [TestMethod]
        public void MapSetClear()
        {
            JsonMapSet map = "John_info";
            UserBase ub = new UserBase();
            ub.Name = "john";
            ub.Country = "cn";
            ub.City = "wx";
            ub.Age = 10;
            Contract contract = new Contract();
            contract.Phone = "345345345";
            contract.Email = "sjo@awd";
            contract.QQ = "asdad";
            map.Set(ub, contract);
            map.Clear();

            IList<object> data = map.Get<UserBase, Contract>();

            Assert.AreEqual(null, data[0]);
            Assert.AreEqual(null, data[1]);
        }
    }


    [Serializable]
    public class UserBase
    {
        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int Age { get; set; }


    }

    [ProtoBuf.ProtoContract]
    public class UserBase1
    {
        [ProtoBuf.ProtoMember(1)]
        public string Name { get; set; }
               [ProtoBuf.ProtoMember(2)]
        public string City { get; set; }
               [ProtoBuf.ProtoMember(3)]
        public string Country { get; set; }
               [ProtoBuf.ProtoMember(4)]
        public int Age { get; set; }


    }

    [Serializable]
    public class Contract
    {
        public string Email { get; set; }

        public string QQ { get; set; }

        public string Phone { get; set; }

    }


}
