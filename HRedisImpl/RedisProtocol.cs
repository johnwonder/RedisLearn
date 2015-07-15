﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRedisImpl
{
    public class MessageFormat
    {
        public static readonly string Head = "*{0}\r\n";


        public static readonly string Argument = "${0}\r\n{1}\r\n";

        public static readonly char CR ='\r';

        /// <summary>
        /// 
        /// </summary>
        public static readonly char LF = '\n';

        /// <summary>
        /// 错误消息
        /// </summary>
        public static readonly char ReplyError = '-';

        /// <summary>
        /// 状态消息
        /// </summary>
        public static readonly char ReplyStatus = '+';

        /// <summary>
        /// 大块消息
        /// </summary>
        public static readonly char ReplyBulk = '$';

        /// <summary>
        /// 多条大块消息
        /// </summary>
        public static readonly  char ReplyMultiBulk = '*';

        /// <summary>
        /// 数字消息
        /// </summary>
        public static readonly char ReplyFigure = ':';


    }

    public class ReplyFormat
    {
        public static readonly string ReplySuccess = "OK";
        public static readonly string PingSuccess = "PONG";
    }
}
