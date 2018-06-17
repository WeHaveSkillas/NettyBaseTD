﻿using System.Collections.Generic;
using NettyBase.Game.world.objects.characters.cooldowns;
using NettyBase.Networking.game_server;
using NettyFramework.Commands;

namespace NettyBase.Game.world.objects.players.settings.slotbars
{
    class FormationItem : SlotbarItem
    {
        public FormationItem(string itemId, int counterValue, int maxCounter, List<ClientUITooltipTextFormat> toolTipItemBar = null, short counterType = 2, bool selected = false, bool visible = true, bool buyable = false) : base(itemId, counterValue, maxCounter, toolTipItemBar, counterType, selected, visible, buyable)
        {
            CounterValue = 0;
            CounterType = SlotbarCategoryItemModule.NONE;
        }

        public override void Execute(Player player)
        {
            if (player.Cooldowns.Any(x => x is DroneFormationCooldown)) return;

            var gameSession = World.StorageManager.GameSessions[player.Id];
            var formationName = ItemId.Split('_')[2];
            var formation = DroneFormation.STANDARD;

            #region Formations Switch
            switch (formationName)
            {
                case "default":
                    formation = DroneFormation.STANDARD;
                    break;
                case "f-01-tu":
                    formation = DroneFormation.TURTLE;
                    break;
                case "f-02-ar":
                    formation = DroneFormation.ARROW;
                    break;
                case "f-03-la":
                    formation = DroneFormation.LANCE;
                    break;
                case "f-04-st":
                    formation = DroneFormation.STAR;
                    break;
                case "f-05-pi":
                    formation = DroneFormation.PINCER;
                    break;
                case "f-06-da":
                    formation = DroneFormation.DOUBLE_ARROW;
                    break;
                case "f-07-di":
                    formation = DroneFormation.DIAMOND;
                    break;
                case "f-08-ch":
                    formation = DroneFormation.CHEVRON;
                    break;
                case "f-09-mo":
                    formation = DroneFormation.MOTH;
                    break;
                case "f-10-cr":
                    formation = DroneFormation.CRAB;
                    break;
                case "f-11-he":
                    formation = DroneFormation.HEART;
                    break;
                case "f-12-ba":
                    formation = DroneFormation.BARRAGE;
                    break;
                case "f-13-bt":
                    formation = DroneFormation.BAT;
                    break;
            }
            #endregion Formations Switch

            player.Formation = formation;

            var cld = new DroneFormationCooldown();
            cld.Send(gameSession);
            player.Cooldowns.Add(cld);

            //Todo: fix
            //GameClient.SendRangePacket(player, netty.commands.new_client.DroneFormationChangeCommand.write(player.Id, (int)formation), true);

            //GameHandler.SendRangePacket(player, PacketBuilder.FormationChange(player.Id, (int)formation), true);
            Selected = true;
            player.Updaters.Update();
            gameSession.Client.Send(ChangeStatus());
            //gameSession.GameHandler.sendPacket(ChangeStatus());
        }
    }
}
