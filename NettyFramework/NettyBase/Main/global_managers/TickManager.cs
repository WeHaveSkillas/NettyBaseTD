﻿using System.Collections.Generic;
using System.Threading.Tasks;
using NettyBase.Main.interfaces;

namespace NettyBase.Main.global_managers
{
    class TickManager
    {
        public static short TICKS_PER_SECOND = 64;

        /// <summary>
        /// ITick, Delay *TODO* 
        /// </summary>
        private List<ITick> Tickables = new List<ITick>();

        private List<ITick> PendingToBeAdded = new List<ITick>();
        private List<ITick> PendingRemoval = new List<ITick>();

        public void Add(ITick tick)
        {           
            if (!Tickables.Contains(tick))
                PendingToBeAdded.Add(tick);
        }

        public void Remove(ITick tick)
        {
            if (Tickables.Contains(tick))
                PendingRemoval.Remove(tick);
        }

        public bool Exists(ITick tickable)
        {
            if (Tickables.Count == 0) return false;
            if (Tickables.Contains(tickable)) return true;
            return false;
        }

        public async void Tick()
        {
            while (true)
            {
                UpdateCollection();
                foreach (var tickable in Tickables) { 
                    await Task.Factory.StartNew(tickable.Tick);
                }
                await Task.Delay(84);
            }
        }

        private void UpdateCollection()
        {
            if (PendingToBeAdded.Count > 0)
            {
                Tickables.AddRange(PendingToBeAdded);
                PendingToBeAdded.Clear();
            }
            if (PendingRemoval.Count > 0)
            {
                foreach (var tick in PendingRemoval)
                    Tickables.Remove(tick);
                PendingRemoval.Clear();
            }
        }
    }
}
