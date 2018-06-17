using System.Collections.Generic;
using NettyBase.Main.interfaces;
using NettyBase.Main.objects;

namespace NettyBase.Main.global_managers
{
    class CronjobManager : ITick
    {
        public List<Cronjob> Cronjobs = new List<Cronjob>();

        public static void Initiate()
        {
            var cron = new CronjobManager
            {
                Cronjobs = Global.QueryManager.LoadCrons()
            };
            Global.TickManager.Add(cron);
        }
        /// <summary>
        /// Ticks the crons
        /// </summary>
        public void Tick()
        {
            foreach (var cron in Cronjobs)
            {
                cron.Tick();
            }
        }
    }
}
