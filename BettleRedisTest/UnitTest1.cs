using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Beetle.Redis;

namespace BettleRedisTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LST_POP_PUSH()
        {
               
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

    [Serializable]
    public class Contract
    {
        public string Email { get; set; }

        public string QQ { get; set; }

        public string Phone { get; set; }

    }


}
