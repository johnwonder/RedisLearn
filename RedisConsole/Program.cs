using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Redis;
using System.Data.SqlClient;

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

//            redisClient.Set<string>("name", "country");
////            Console.WriteLine(redisClient.Get<string>("name"));

//            SqlConnection sqlConn = new SqlConnection();
//            sqlConn.ConnectionString = "Data Source=192.168.1.49;Initial Catalog=LocationInfo201509;User ID=sa;Password=123456;Max Pool Size=1000";
//            sqlConn.Open();


//            SqlCommand sqlCmd = new SqlCommand();
//            sqlCmd.Connection = sqlConn;
//            sqlCmd.CommandText = @"SELECT [VehicleUniqueID],[GPSTime],[Body],[CreatedTime]
//                                ,[ModifiedTime]
//                                FROM [LocationInfo201509].[dbo].[LocationInfoBlob_20150930]";
     

//            SqlDataReader sqlReader = sqlCmd.ExecuteReader();
//            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
//            sw.Start();
//            while (sqlReader.Read())
//            {
//                string vid = sqlReader.GetValue(0).ToString();
//                redisClient.Set(vid, sqlReader.GetValue(2));
//                Console.WriteLine(vid);
//            }
//            sw.Stop();
//            Console.WriteLine("insert complete:" + sw.Elapsed.Minutes);


            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            LocationInfo info =new LocationInfo();
            info.PlateNumber = "苏D32969";
            info.Name ="john";
            redisClient.StoreAsHash<LocationInfo>(info);

            LocationInfo loc = redisClient.GetFromHash<LocationInfo>(info.PlateNumber);
            Console.WriteLine(loc.Name);
            //Console.WriteLine("key count:"+strKeys.Count);

            //foreach (var item in strKeys)
            //{
            //    Console.WriteLine("key:" + item);

            //    byte[] bvalue = redisClient.Get(item);


            //}
            //sw.Stop();
            //Console.WriteLine(strKeys.Count);
            //Console.WriteLine("query complete:" + sw.Elapsed.Milliseconds);


            //sqlReader.Dispose();
            //sqlReader.Close();
            //sqlConn.Close();
            Console.ReadKey();
        }
    }

    public class LocationInfo
    {
        public string PlateNumber { get; set; }

        public string Name { get; set; }
    }
}
