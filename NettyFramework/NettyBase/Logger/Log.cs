using System;

namespace NettyBase.Logger
{
    abstract class Log
    {
        public const string BASE_DIR = "/logs";

        protected Writer Writer { get; set; }

        public static DateTime LastLogTime { get; set; }

        public abstract void Initialize(string fileName = "");
    }
}
