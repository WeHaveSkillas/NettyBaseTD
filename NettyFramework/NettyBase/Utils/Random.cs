﻿using System;
using NettyBase.Main.interfaces;

namespace NettyBase.Utils
{
    class Random : ITick
    {
        public void Tick()
        {
            if (LastTimeResetted.AddMinutes(1) < DateTime.Now)
                Reset();
        }

        private static System.Random _rnd = new System.Random();
        private static DateTime LastTimeResetted = DateTime.Now;

        private static void Reset()
        {
            _rnd = new System.Random();
        }

        public static int Next(int n1, int n2)
        {
            return _rnd.Next(n1, n2);
        }

        public static int Next(int n)
        {
            return _rnd.Next(n);
        }
    }
}
