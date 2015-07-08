using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Beetle.Redis;

namespace BettleRedis
{
    class Program
    {
        static void Main(string[] args)
        {
            StringKey key = "JOHN";
            string Remark = "johnwonder";

            key.Set(Remark);
            Console.WriteLine(key.Get<string>());


            JsonKey jk = "john_json";
            UserBase ub = new UserBase();
            ub.Name = "john360";
            ub.City = "wx";
            ub.Country = "cn";
            ub.Age = 10;
            jk.Set(ub);

            Console.WriteLine(jk.Get<UserBase>().Name);
            //ProtobufList
            ProtobufKey rk = "john_protobuf";
            rk.Delete();
            UserBase1 userB = new UserBase1();
            userB.Name = "john123";
            userB.City = "wx";
            userB.Country = "cn";
            userB.Age = 10;
            rk.Set(userB);

            Console.WriteLine(rk.Get<UserBase1>().Name);
            Console.ReadLine();
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
}
