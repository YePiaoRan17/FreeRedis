﻿using FreeRedis.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FreeRedis
{
    partial class RedisClient
    {

        protected internal enum UseType {
            Pooling,
            Cluster,
            Sentinel,
            SingleInside, 
            SingleTemp, 
            Pipeline, 
            Transaction,
        }

        protected internal abstract class BaseAdapter
        {
            public static ThreadLocal<Random> _rnd = new ThreadLocal<Random>(() => new Random());
            public UseType UseType { get; protected set; }
            protected Exception CannotUseType() => new Exception($"RedisClient: Method cannot be used in {UseType} mode.");

            public abstract IRedisSocket GetRedisSocket(CommandPacket cmd);
            public abstract void Dispose();

            public abstract T CheckSingle<T>(Func<T> func);
            public abstract T2 AdapaterCall<T1, T2>(CommandPacket cmd, Func<RedisResult<T1>, T2> parse);
        }
    }
}
