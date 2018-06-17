using System;
using NettyBase.Logger.types;
using NettyBase.Main.global_managers;

namespace NettyBase.Main.interfaces
{
    abstract class DBManagerUtils
    {
        internal DebugLog Log => SqlDatabaseManager.Log;

        internal int intConv(object i) => Convert.ToInt32(i);

        internal double doubleConv(object i) => Convert.ToDouble(i);

        internal long longConv(object i) => Convert.ToInt64(i);

        internal string stringConv(object i) => Convert.ToString(i);

        public abstract void Initiate();
    }
}
