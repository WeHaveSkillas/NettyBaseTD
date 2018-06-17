﻿using System.Collections.Generic;
using NettyBase.Game.world.objects.characters;
using NettyBase.Game.world.objects.characters.cooldowns;
using NettyBase.Networking.game_server;
using NettyFramework.Commands;

namespace NettyBase.Game.world.objects.players.settings.slotbars
{
    class SpecialItem : SlotbarItem
    {
        public SpecialItem(string itemId, int counterValue, int maxCounter, List<ClientUITooltipTextFormat> toolTipItemBar = null, short counterType = 2, bool selected = false, bool visible = true, bool buyable = false) : base(itemId, counterValue, maxCounter, toolTipItemBar, counterType, selected, visible, buyable)
        {
        }

        public override void Execute(Player player)
        {
            var gameSession = World.StorageManager.GameSessions[player.Id];

            Cooldown cooldown;
            switch (ItemId)
            {
                case "equipment_extra_cpu_ish-01":
                    if (player.Cooldowns.Any(x => x is ISHCooldown)) return;

                    //todo: fix

//                    GameClient.SendToPlayerView(player, netty.commands.new_client.LegacyModule.write("0|n|ISH|" + player.Id), true);
                    player.Controller.Effects.SetInvincible(3000);

                    cooldown = new ISHCooldown();
                    player.Cooldowns.Add(cooldown);
                    cooldown.Send(gameSession);
                    break;

                case "ammunition_mine_smb-01":
                    if (player.Cooldowns.Any(x => x is SMBCooldown)) return;
                    if (player.State.InDemiZone) return;

                    //todo: fix
                    //GameClient.SendToPlayerView(player, netty.commands.new_client.LegacyModule.write("0|n|SMB|" + player.Id), true);
                    player.Controller.Damage?.Area(20, 1000, true, DamageType.PERCENTAGE);

                    cooldown = new SMBCooldown();
                    player.Cooldowns.Add(cooldown);
                    cooldown.Send(gameSession);
                    break;

                case "ammunition_specialammo_emp-01":
                    if (player.Cooldowns.Any(x => x is EMPCooldown)) return;
                    if (player.State.InDemiZone) return;

                    //GameClient.SendToPlayerView(player, netty.commands.new_client.LegacyModule.write("0|n|EMP|" + player.Id), true);

                    //todo: fix
                    //GameClient.SendToPlayerView(player, netty.commands.new_client.LegacyModule.write("0|UI|MM|NOISE|1|1"));

                    //I can't use GameHandler.SendSelectedPacket because i need to set Selected to null
                    foreach (var entry in player.Spacemap.Entities)
                    {
                        var entity = entry.Value;

                        if (entity.Selected != null && entity.SelectedCharacter == player && entity is Player playerEntity)
                        {
                            var entitySession = playerEntity.GetGameSession();
                            if (entitySession != null)
                            {                //todo: fix

                                //Packet.Builder.LegacyModule(entitySession, "0|A|STM|msg_own_targeting_harmed");
                                //Packet.Builder.ShipSelectionCommand(entitySession, null);
                            }
                            entity.Controller.Attack.Attacking = false;
                            entity.Selected = null;
                        }
                    }

                    player.Controller.Effects.NotTargetable(3000);
                    player.AttachedNpcs.Clear();

                    cooldown = new EMPCooldown();
                    player.Cooldowns.Add(cooldown);
                    cooldown.Send(gameSession);
                    break;
            }
        }
    }
}
