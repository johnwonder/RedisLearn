using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace RedisImpl
{
    public  class RedisBaseClient
    {
        private Configuration configuration;

        private Socket socket;

        private byte[] ReceiveBuffer = new byte[100000];

        public RedisBaseClient(Configuration config)
        {
            configuration = config;
        }

        public RedisBaseClient():this(new Configuration())
        {

        }

        public void Connect()
        {
            if (socket != null && socket.Connected)
                return;

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp)
                {
                    NoDelay = configuration.NoDelaySocket
                };
            socket.Connect(configuration.Host, configuration.Port);
            if (socket.Connected)
                return;

            Close();
        }

        public void Close()
        {
            socket.Disconnect(false);
            socket.Close();
        }

        public string SendCommand(RedisCommand command,params string[] args)
        {
            const string headstr = "*{0}\r\n";
            const string bulkstr = "${0}\r\n{1}\r\n";

            var sb = new StringBuilder();

            sb.AppendFormat(headstr, args.Length + 1);

            var cmd = command.ToString();
            sb.AppendFormat(bulkstr, cmd.Length, cmd);

            foreach (var arg in args)
            {
                sb.AppendFormat(bulkstr, arg.Length, arg);
            }

            byte[] c = Encoding.UTF8.GetBytes(sb.ToString());
            try
            {
                Connect();
                socket.Send(c);

                socket.Receive(ReceiveBuffer);
                Close();

                return ReadData();
            }
            catch (SocketException e)
            {

                Close();
            }
            return null;
        }

        private string ReadData()
        {
            var data = Encoding.UTF8.GetString(ReceiveBuffer);
            char c = data[0];

            if (c == '-')
                throw new Exception(data);
            if (c == '+')
                return data;
            return data;
        }

        public void CreatePipeline()
        {
            //SendCommand(RedisCommand.MULTI, new string[] { }, true);
        }

        public string EnqueueCommand(RedisCommand command,params string[] args)
        {
            return SendCommand(command, args, true);
        }

        public string FlushPipeline()
        {
            var result = SendCommand(RedisCommand.EXEC, new string[] { }, true);
            Close();
            return result;
        }

        public string SendCommand(RedisCommand command,string[] args,bool isPipeline =false)
        {
            const string headStr = "*{0}\r\n";

            const string bulkstr = "${0}\r\n{1}\r\n";

            var sb = new StringBuilder();
            sb.AppendFormat(headStr, args.Length + 1);

            var cmd = command.ToString();
            sb.AppendFormat(bulkstr, cmd.Length, cmd);

            foreach (var arg in args)
            {
                sb.AppendFormat(bulkstr, arg.Length, arg);
            }

            byte[] c = Encoding.UTF8.GetBytes(sb.ToString());
            try
            {
                Connect();
                socket.Send(c);

                socket.Receive(ReceiveBuffer);
                if (!isPipeline)
                {
                    Close();
                }
                return ReadData();
            }
            catch (SocketException e)
            {
                Close();
            }
            return null;
        }

        public string SetByPipeline(string key,string value,int second)
        {
            this.CreatePipeline();
            this.EnqueueCommand(RedisCommand.SET, key, value);
            this.EnqueueCommand(RedisCommand.EXPIRE, key, second.ToString());

            return this.FlushPipeline();
        }
    }
}
