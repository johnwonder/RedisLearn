using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;

namespace HRedisImpl
{
    internal static  class SocketExtensions
    {
        public static bool IsConnected(this Socket socket)
        {
            try
            {
                if (socket == null)
                    return false;
                if (!socket.Connected)
                    return false;

                return !(socket.Poll(1000, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException exception)
            {

                Debug.Write(exception.Message);
            }
            return false;
        }
    }
}
