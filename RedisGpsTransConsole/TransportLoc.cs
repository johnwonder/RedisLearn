using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cassandra;
using ServiceStack.Redis;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;

namespace RedisGpsTransConsole
{
    public class TransportLoc
    {
        static RedisClient redisClient = new RedisClient("127.0.0.1", 6379);

        static RedisClient redisClientUnix = new RedisClient("192.168.1.176", 6379);

        IRedisClientsManager clientManager = new PooledRedisClientManager(50,10,new string[2] { "192.168.1.176:6379", "192.168.1.176:6380" });
        public void StartUnix()
        {
            redisClientUnix.Set("john","wonder");


            string v = redisClientUnix.Get<string>("john");
        }

        public void Start()
        {
            try
            {
                // Connect to the demo keyspace on our cluster running at 127.0.0.1
               // Cluster cluster = Cluster.Builder().AddContactPoint("192.168.1.166").Build();
               // ISession session = cluster.Connect("logistics");
                //session.Execute("insert into dipper_frame (plate_number,time, acc, mileage,lat,lng) values ('测A12345', " + ConvertDateTimeInt(DateTime.Now) + ", true, 1234.123, 121.123456,31.654321)");
               // RowSet rows = session.Execute("select * from dipper_frame");

                InitDicGuid_Licenselate();


                IRedisClientsManager clientManager = new PooledRedisClientManager();
                IRedisClient client = clientManager.GetClient();
                string[] lpArray =  dicGuid_Licenselate.Values.Distinct().ToArray();
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

                try
                {
                    using (var redisLocs = client.As<Loc>())
                    {
                        //Loc l = redisLocs["苏BJ6615"];
                        //Console.WriteLine("车辆：" + l.lat);
                        redisLocs.FlushAll();
                        foreach (string row in lpArray)
                        {
                            Console.WriteLine("车辆：" + row);
                            //if (redisLocs.ContainsKey(row))
                            //{
                            //    redisLocs.Sets[row].Clear();
                            //    //if (l != null)
                            //    //{
                            //    //    //Console.WriteLine("车辆：" + l.lat);
                            //    //    bool isRemove = redisLocs.RedisClient.Remove(row);
                            //    //    Console.WriteLine("isRemove：" + isRemove);
                            //    //}
                            //}
                            Console.WriteLine("总车辆数：" + lpArray.Length);
                            Console.WriteLine("车辆数:" + redisLocs.Lists[row].Count);
                            //redisLocs.RemoveAllFromList(redisLocs.Lists[row]);
                            Console.WriteLine("delete :" + row);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.Read();
                    //throw;
                }
               
                //Console.Read();

                sw.Start();
                using (var redisLocs = client.As<Loc>())
                {
                    foreach (string row in lpArray)
                    {
                        for (int i = 0; i < 1; i++)//一个月
                        {
                            for (int j = 0; j < 2000; j++)
                            {
                                //string plateNumber = (string)row["plate_number"];

                                redisLocs.Lists[row].Add(new Loc { lat = 20 * j, lng = 20, plate_number = row, acc = true, mileage = 15 * j, time = DateTime.Now.AddMinutes(j) });
                                //Id = redisLocs.GetNextSequence(),
                                //redisLocs.Store(new Loc {  lat = (double)row["lat"], lng = (double)row["lng"], plate_number = (string)row["plate_number"], acc = (bool)row["acc"], mileage = (float)row["mileage"], time = ConvertFromDateTimeOffset((DateTimeOffset)row["time"]) });//);
                                Console.WriteLine("insert :" + row);
                            }
                        }
                       
                    }
                    
                }
 
                sw.Stop();
                Console.WriteLine("insert complete:" + sw.Elapsed.Milliseconds);
              
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
            Console.Read();
        }

        public void StartInsert(string lp)
        {
            try
            {
                // Connect to the demo keyspace on our cluster running at 127.0.0.1
                Cluster cluster = Cluster.Builder().AddContactPoint("192.168.1.166").Build();
                ISession session = cluster.Connect("logistics");
                //session.Execute("insert into dipper_frame (plate_number,time, acc, mileage,lat,lng) values ('测A12345', " + ConvertDateTimeInt(DateTime.Now) + ", true, 1234.123, 121.123456,31.654321)");


                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

                RowSet rows = session.Execute("select * from dipper_frame where plate_number='"+lp+"'");
               

                var redisLocs = redisClient.As<Loc>();
                foreach (Row row in rows)
                {
                    string plateNumber = (string)row["plate_number"];

                    //Id = redisLocs.GetNextSequence(),
                    //redisLocs.stor
                        
                        
                    //    (new Loc { lat = (double)row["lat"], lng = (double)row["lng"], plate_number = (string)row["plate_number"], acc = (bool)row["acc"], mileage = (float)row["mileage"], time = ConvertFromDateTimeOffset((DateTimeOffset)row["time"]) });//);
                    //Console.WriteLine("insert :" + plateNumber);

                   
                }


                sw.Stop();
                Console.WriteLine("insert complete:" + sw.Elapsed.Milliseconds);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public void StartInsertUnix(string lp)
        {
            try
            {

                using (IRedisClient client = clientManager.GetClient()) {

                    using (var redisLocs = client.As<Loc>())
                    {

                        //for (int i = 0; i < 1; i++)//一个月
                        //{
                        for (int j = 0; j < 2000; j++)
                        {
                            //string plateNumber = (string)row["plate_number"];

                            redisLocs.Lists[lp].Add(new Loc { lat = 20 * j, lng = 20, plate_number = lp, acc = true, mileage = 15 * j, time = DateTime.Now.AddMinutes(j) });
                            //Id = redisLocs.GetNextSequence(),
                            //redisLocs.Store(new Loc {  lat = (double)row["lat"], lng = (double)row["lng"], plate_number = (string)row["plate_number"], acc = (bool)row["acc"], mileage = (float)row["mileage"], time = ConvertFromDateTimeOffset((DateTimeOffset)row["time"]) });//);
                            Console.WriteLine("insert :" + lp);
                        }
                        //}


                    }

                }
    
                

             
 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                Console.Read();
            }

        }

        public void StartParall()
        {
            try
            {
 
                IRedisClient client = clientManager.GetClient();
                using (var redisLocs = client.As<Loc>())
                {
                    //Loc l = redisLocs["苏BJ6615"];
                    //Console.WriteLine("车辆：" + l.lat);
                    redisLocs.FlushAll();
                    //foreach (string row in lpArray)
                    //{
                    //    Console.WriteLine("车辆：" + row);
                    //    //if (redisLocs.ContainsKey(row))
                    //    //{
                    //    //    redisLocs.Sets[row].Clear();
                    //    //    //if (l != null)
                    //    //    //{
                    //    //    //    //Console.WriteLine("车辆：" + l.lat);
                    //    //    //    bool isRemove = redisLocs.RedisClient.Remove(row);
                    //    //    //    Console.WriteLine("isRemove：" + isRemove);
                    //    //    //}
                    //    //}
                    //    Console.WriteLine("总车辆数：" + lpArray.Length);
                    //    Console.WriteLine("车辆数:" + redisLocs.Lists[row].Count);
                    //    //redisLocs.RemoveAllFromList(redisLocs.Lists[row]);
                    //    Console.WriteLine("delete :" + row);
                    //}

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.Read();
                //throw;
            }
            InitDicGuid_Licenselate();

            ParallelOptions op = new ParallelOptions();
            op.MaxDegreeOfParallelism = 8;

            string[] lpArray = dicGuid_Licenselate.Values.Distinct().ToArray();
            int recordCount = lpArray.Length;

            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
 
            sw.Start();
            Parallel.For(0, recordCount, op, (k) =>
            {
                int index = (int)k;
                #region 2.1.1.1转换成位置对象，转存至真好运数据库
                string lp = lpArray[index];
                try
                {
                    StartInsertUnix(lp);
                }
                catch (Exception ex)
                {
                    //LogHelper.error(string.Format("查询位置异常：车牌[{2}]\t时段[{0}]-[{1}]\t{3}\r\n{4}", dtStart.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"), lp, ex.Message, ex.StackTrace));
                }
                #endregion

            });
            sw.Stop();
            Console.WriteLine("insert complete:" + sw.Elapsed.Milliseconds);
            Console.Read();
        }

        public void QueryRedis(string lp)
        {
 
            Console.WriteLine("开始查询车辆：");

            List<Loc> locList = null;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
 
            IRedisClientsManager clientManager = new  PooledRedisClientManager();
            IRedisClient client =   clientManager.GetClient();

            using (var redis = client.As<Loc>())
            {
                locList = redis.GetAllItemsFromList(redis.Lists[lp]);
            } 
              
            sw.Stop();
            string msg = string.Empty;
            //msg += string.Format("从cassandra读取数据：【{0}】\t时段:[{1}]-[{2}]", lp);
            msg += "\t查询用时：" + sw.ElapsedMilliseconds + " ms";
            Console.WriteLine(msg);
            if (locList != null)
            {
                sw.Restart();
                foreach (var loc in locList)
                {
                    Console.WriteLine("PlateNum:" + loc.plate_number +"lat:"+loc.lat);
                }
                sw.Stop();
                msg = "\t查询总条数：[" + locList.Count + "]\t遍历用时：[" + sw.ElapsedMilliseconds + "] ms";
                Console.WriteLine(msg);
                //sw.Restart();
            }
            Console.Read();
            
        
        }

        public void QueryParall()
        {
            #region 2.1.1遍历每日每条车辆UniqueId，查出其对应的二进制位置内容
            Console.WriteLine("初始化车辆字典开始");
            InitDicGuid_Licenselate();

            Console.WriteLine("初始化车辆字典结束！");

            ParallelOptions op = new ParallelOptions();
            op.MaxDegreeOfParallelism = 8;

            string[] lpArray = dicGuid_Licenselate.Values.Distinct().ToArray();
            int recordCount = lpArray.Length;
            Parallel.For(0,1, op, (k) =>
            {
                int index = (int)k;
                #region 2.1.1.1转换成位置对象，转存至真好运数据库
                string lp = lpArray[index];
                try
                {
                    QueryRedis(lp);
                }
                catch (Exception ex)
                {
                    //LogHelper.error(string.Format("查询位置异常：车牌[{2}]\t时段[{0}]-[{1}]\t{3}\r\n{4}", dtStart.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"), lp, ex.Message, ex.StackTrace));
                }
                #endregion

            });

            #endregion
        }

        Dictionary<string, string> dicGuid_Licenselate = new Dictionary<string, string>();
        /// <summary>
        /// 初始化 车辆GUID及车牌号字典
        /// </summary>
        private void InitDicGuid_Licenselate()
        {
            SqlConnection Connection = new SqlConnection("Data Source=192.168.1.49;Initial Catalog=TestDB;User ID=sa;Password=123456");
            SqlCommand cmd = (SqlCommand)Connection.CreateCommand();

            cmd.CommandText = "select UniqueID,LicensePlate from TestDB.dbo.Vehicle";

            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            DbDataReader reader = (DbDataReader)cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                if (!dicGuid_Licenselate.ContainsKey(reader["UniqueID"].ToString()))
                {
                    dicGuid_Licenselate.Add(reader["UniqueID"].ToString(), reader["LicensePlate"].ToString());
                }
            }
        }

        /// <summary>
        /// https://msdn.microsoft.com/zh-cn/library/bb546101%28v=vs.110%29.aspx
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        static DateTime ConvertFromDateTimeOffset(DateTimeOffset dateTime)
        {
            if (dateTime.Offset.Equals(TimeSpan.Zero))
                return dateTime.UtcDateTime;
            else if (dateTime.Offset.Equals(TimeZoneInfo.Local.GetUtcOffset(dateTime.DateTime)))
                return DateTime.SpecifyKind(dateTime.DateTime, DateTimeKind.Local);
            else
                return dateTime.DateTime;
        }
    }

    /// <summary>
    /// 位置信息
    /// </summary>
    public class Loc
    {
        //plate_number TEXT,
        //time timestamp,
        //acc boolean,
        //mileage float,
        //lat double,
        //lng double,

        //public object Id { get; set; }

        public string plate_number { get; set; }

        public DateTime time { get; set; }
        public bool acc { get; set; }
        public float mileage { get; set; }

        public double lat { get; set; }

        public double lng { get; set; }
    }

    public class LocationInfo
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
