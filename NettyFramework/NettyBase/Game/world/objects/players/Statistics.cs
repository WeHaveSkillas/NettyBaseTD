﻿using System;
using System.Collections.Generic;
using NettyBase.Game.world.objects.players.statistics;

namespace NettyBase.Game.world.objects.players
{
    class Statistics : PlayerBaseClass
    {
        /// <summary>
        /// Killed entities
        /// </summary>
        public Dictionary<string, int> SHIPS_KILLED { get; set; }

        /// <summary>
        /// Deaths
        /// </summary>
        public List<DeathStat> DEATHS { get; set; }

        /// <summary>
        /// Spent Ammunitions
        /// </summary>
        public Dictionary<string, int> SPENT_AMMO { get; set; }

        /// <summary>
        /// Damage dealt to all sort of entities
        /// </summary>
        public int TOTAL_DAMAGE_DEALT { get; set; }

        public int COLLECTABLES_COLLECTED { get; set; }

        /// <summary>
        /// Average movement per minute
        /// </summary>
        public double AVG_MOVE_PER_MIN { get; set; }

        /// <summary>
        /// Averege session lenght
        /// </summary>
        public int AVG_SESSION_LENGHT { get; set; } 

        public DateTime LAST_TIME_CONNECTED { get; set; }

        public Statistics(Player player) : base(player)
        {
        }
    }
}
