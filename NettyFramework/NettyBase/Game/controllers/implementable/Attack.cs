﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NettyBase.Game.controllers.implementable.attack;
using NettyBase.Game.world.objects;
using NettyBase.Game.world.objects.characters.cooldowns;
using NettyBase.Game.world.objects.players.extra.techs;
using NettyBase.Networking.game_server;
using NettyBase.Utils;
using Random = NettyBase.Utils.Random;

namespace NettyBase.Game.controllers.implementable
{
    class Attack : IAbstractCharacter
    {
        public bool Attacking { get; set; }

        public Player MainAttacker { get; set; }

        public ConcurrentDictionary<int, Attacker> Attackers = new ConcurrentDictionary<int, Attacker>();

        public Attack(AbstractCharacterController controller) : base(controller)
        {
        }

        public override void Tick()
        {
            if (Attacking)
                LaserAttack();
            RefreshAttackers();
        }

        public override void Stop()
        {
            Controller.Attack.MainAttacker = null;
            Controller.Attack.Attackers.Clear();
        }

        public List<Character> GetActiveAttackers()
        {
            List<Character> attackers = new List<Character>();
            foreach (var entity in Character.Range.Entities.Values)
            {
                if (entity.EntityState == EntityStates.DEAD) continue;
                if (entity.Selected == Character && entity.Controller.Attack.Attacking)
                {
                    attackers.Add(entity);
                }
            }
            return attackers;
        }

        private DateTime LastLaserAttack = new DateTime();

        private DateTime RSBCooldownEnd = new DateTime();

        public void LaserAttack()
        {
            var enemy = Character.Selected;
            if (!AssembleEnemy(enemy)) return;

            var isRsb = (Character as Player)?.Settings.CurrentAmmo.LootId == "ammunition_laser_rsb-75";
            if (isRsb)
            {
                if (RSBCooldownEnd > DateTime.Now) return;

                var cld = new RSBCooldown();
                cld.Send(((Player)Character).GetGameSession());
                Character.Cooldowns.Add(cld);
                RSBCooldownEnd = cld.EndTime;
            }
            else
            {
                if (LastLaserAttack.AddMilliseconds(750) > DateTime.Now) return;
                LastLaserAttack = DateTime.Now;
            }

            var damage = Character.Damage;
            var absDamage = 0; //This variable will be used for ammo that absobrs shield too

            var laserColor = 0;

            if (Character is Player)
            {
                var gameSession = World.StorageManager.GetGameSession(Character.Id);
                if (gameSession.Player.Equipment.LaserCount() == 0)
                {
                    Attacking = false; // Will stop attacking if there are no lasers equipped.
                    //TODO: fix
                    //Packet.Builder.LegacyModule(gameSession, "0|A|STM|no_lasers_on_board");
                    return;
                }

                if (gameSession.Player.Settings.CurrentAmmo.Shoot() == 0)
                {
                    // NOTHING TO SHOOT
                    //TODO: fix
                    //Packet.Builder.LegacyModule(gameSession, "0|A|STD|No more ammo (todo: find a proper STM message)");
                    var getAmmo = gameSession.Player.Information.Ammunitions.Keys.ToList();
                    if (gameSession.Player.Settings.CurrentAmmo.LootId == getAmmo[0])
                    {
                        Attacking = false;
                        return;
                    }
                    var index = getAmmo.FindIndex(x => x == gameSession.Player.Settings.CurrentAmmo.LootId) - 1;
                    gameSession.Player.Settings.CurrentAmmo =
                        gameSession.Player.Information.Ammunitions[getAmmo[index]];
                    //gameSession.Player.Settings.OldClientShipSettingsCommand.selectedLaser = index;
                    //TODO: Fix
                    //Packet.Builder.SendSlotbars(gameSession);
                }

                var laserTypes = gameSession.Player.Equipment.LaserTypes();
                switch (gameSession.Player.Settings.CurrentAmmo.LootId)
                {
                    case "ammunition_laser_mcb-25":
                        damage *= 2;
                        if (laserTypes == 3)
                            laserColor = 1;
                        break;
                    case "ammunition_laser_mcb-50":
                        damage *= 3;
                        if (laserTypes == 3)
                            laserColor = 2;
                        break;
                    case "ammunition_laser_ucb-100":
                        damage *= 4;
                        laserColor = 3;
                        break;
                    case "ammunition_laser_sab-50":
                        absDamage = damage * 2;
                        damage *= 0;
                        laserColor = 4;
                        break;
                    case "ammunition_laser_cbo-100":
                        absDamage = damage;
                        damage = damage * 2;
                        laserColor = 8;
                        break;
                    case "ammunition_laser_rsb-75":
                        damage *= 6;
                        laserColor = 6;
                        break;
                    case "ammunition_laser_job-100":
                        damage *= 4;
                        laserColor = 9;
                        break;
                }

                //TODO: RECODE!!!
                if (gameSession.Player.Controller.IsAutoRocketCpuActive())
                {
                    LaunchMissle(gameSession.Player.Settings.CurrentRocket.LootId);
                }

                if (gameSession.Player.Controller.IsAutoLauncherCpuActive())
                {
                    var rocketLauncher = Character.RocketLauncher;
                    if (rocketLauncher != null && rocketLauncher.Launchers.Length > 0)
                    {
                        if (rocketLauncher.CurrentLoad != rocketLauncher.GetMaxLoad())
                        {
                            rocketLauncher.Reload();
                        }
                        else
                        {
                            LaunchRocketLauncher();
                            rocketLauncher.Reload();
                        }
                    }
                }

                if (enemy is Character)
                    UpdateAttacker(enemy as Character, gameSession.Player);
            }
            else if (Character is Pet)
            {
                var pet = (Pet) Character;
                if (pet.GetOwner() == null) return;
                var gameSession = World.StorageManager.GetGameSession(pet.GetOwner().Id);
                
                if (gameSession.Player.Settings.CurrentAmmo.Shoot() == 0)
                {
                    // NOTHING TO SHOOT
                    //TODO fix
                    //Packet.Builder.LegacyModule(gameSession,
                    //    "0|A|STD|No more ammo (todo: find a proper STM message)");
                    var getAmmo = gameSession.Player.Information.Ammunitions.Keys.ToList();
                    if (gameSession.Player.Settings.CurrentAmmo.LootId == getAmmo[0])
                    {
                        Attacking = false;
                        return;
                    }
                    var index = getAmmo.FindIndex(x => x == gameSession.Player.Settings.CurrentAmmo.LootId) - 1;
                    gameSession.Player.Settings.CurrentAmmo =
                        gameSession.Player.Information.Ammunitions[getAmmo[index]];
                    //gameSession.Player.Settings.OldClientShipSettingsCommand.selectedLaser = index;
                    //todo : fix
                    //Packet.Builder.SendSlotbars(gameSession);
                }

                var laserTypes = gameSession.Player.Equipment.LaserTypes();
                switch (gameSession.Player.Settings.CurrentAmmo.LootId)
                {
                    case "ammunition_laser_mcb-25":
                        damage *= 2;
                        if (laserTypes == 3)
                            laserColor = 1;
                        break;
                    case "ammunition_laser_mcb-50":
                        damage *= 3;
                        if (laserTypes == 3)
                            laserColor = 2;
                        break;
                    case "ammunition_laser_ucb-100":
                        damage *= 4;
                        laserColor = 3;
                        break;
                    case "ammunition_laser_sab-50":
                        absDamage = damage * 2;
                        damage *= 0;
                        laserColor = 4;
                        break;
                    case "ammunition_laser_cbo-100":
                        absDamage = damage;
                        damage = damage * 2;
                        laserColor = 8;
                        break;
                    case "ammunition_laser_rsb-75":
                        damage *= 6;
                        laserColor = 6;
                        break;
                    case "ammunition_laser_job-100":
                        damage *= 4;
                        laserColor = 9;
                        break;
                }
            }

            damage = RandomizeDamage(damage);
            absDamage = RandomizeDamage(absDamage);
            //todo: fix
            //GameClient.SendToPlayerView(Character,
            //    netty.commands.new_client.AttackLaserRunCommand.write(Character.Id, enemy.Id, laserColor, enemy is Player,
            //        Character is Player), true);

            Controller.Damage?.Laser(enemy, damage, false);
            Controller.Damage?.Laser(enemy, absDamage, true);

            if (Character is Player)
            {
                var player = (Player)Character;
                if (player.Settings.CurrentAmmo.LootId != "ammunition_laser_sab-50") {
                    foreach (var tech in player.Techs)
                    {
                        if (tech is EnergyLeech)
                        {
                            var energyLeech = (EnergyLeech)tech;
                            energyLeech.ExecuteHeal(damage);
                        }
                    }
                }
            }
        }

        public void LaunchMissle(string missleId)
        {
            var enemy = Character.Selected;
            if (!AssembleEnemy(enemy)) return;

            Player player = Character as Player;

            int damage = -1;
            int rocketId = 0;

            switch (missleId)
            {
                case "ammunition_rocket_r-310":
                    rocketId = 1;
                    damage = RandomizeDamage(Character.RocketDamage, (Character is Player && ((Player)Character).Storage.PrecisionTargeterActivated ? 0 : 1));
                    break;
                case "ammunition_rocket_plt-2026":
                    rocketId = 2;
                    damage = RandomizeDamage(Character.RocketDamage * 2, (Character is Player && ((Player)Character).Storage.PrecisionTargeterActivated ? 0 : 1));
                    break;
                case "ammunition_rocket_plt-2021":
                    rocketId = 3;
                    damage = RandomizeDamage(Character.RocketDamage * 3, (Character is Player && ((Player)Character).Storage.PrecisionTargeterActivated ? 0 : 1));
                    break;
                case "ammunition_rocket_plt-3030":
                    rocketId = 4;
                    damage = RandomizeDamage(Character.RocketDamage * 4, (Character is Player && ((Player)Character).Storage.PrecisionTargeterActivated ? 0 : 1));
                    break;
                case "ammunition_specialammo_pld-8":
                    rocketId = 5;
                    Plasma(enemy as Character);
                    break;
                case "ammunition_specialammo_dcr-250":
                    rocketId = 6;
                    Decelerate(enemy as Character);
                    break;
                case "ammunition_specialammo_wiz-x":
                    rocketId = 8;
                    if (Character.Cooldowns.Any(c => c is WizardCooldown)) return;
                    Wizard(enemy as Character);
                    break;
            }

            if (Character.Cooldowns.Any(c => c is RocketCooldown)) return;

            if (player?.Settings.CurrentRocket.Shoot() == 0)
            {
                // NOTHING TO SHOOT
                //todo:fix
                //Packet.Builder.LegacyModule(player.GetGameSession(),
                //    "0|A|STD|No more ammo (todo: find a proper STM message)");
                return;
            }

            double cooldown_time = 2;
            if (player != null && (player.Extras.ContainsKey("equipment_extra_cpu_rok-t01") || player.Information.Premium.Active))
                cooldown_time *= 0.5;

            var cooldown = new RocketCooldown(cooldown_time);

            if (player != null) cooldown.Send(World.StorageManager.GetGameSession(player.Id));
            Character.Cooldowns.Add(cooldown);

            if (player != null && enemy is Character) UpdateAttacker(enemy as Character, player);

            // todo: fix
            //GameClient.SendToPlayerView(Character, netty.commands.new_client.LegacyModule.write("0|v|" + Character.Id + "|" + enemy.Id + "|H|" + rocketId + "|1|" + (Character is Player && ((Player)Character).Storage.PrecisionTargeterActivated ? 1 : 0)), true);
            Controller.Damage?.Rocket(enemy, damage, false);
        }

        public void LaunchRocketLauncher()
        {
            var enemy = Character.Selected;
            if (Character.RocketLauncher == null || !AssembleEnemy(enemy)) return;

            var player = Character as Player;

            int damage = 0;
            int absDamage = 0;
            int rocketId = 0;
            Damage.Types dmgTypes = Damage.Types.ROCKET;

            switch (Character.RocketLauncher.LoadLootId)
            {
                case "ammunition_rocketlauncher_eco-10":
                    rocketId = 9;
                    damage = RandomizeDamage(2000 * Character.RocketLauncher.CurrentLoad);
                    break;
                case "ammunition_rocketlauncher_hstrm-01":
                    rocketId = 10;
                    damage = RandomizeDamage(2000 * Character.RocketLauncher.CurrentLoad);
                    break;
                case "ammunition_rocketlauncher_ubr-100":
                    rocketId = 11;
                    var baseDamage = 4000;
                    if (enemy is Npc) baseDamage = 7500;
                    damage = RandomizeDamage(baseDamage * Character.RocketLauncher.CurrentLoad);
                    break;
                case "ammunition_rocketlauncher_sar-01":
                    rocketId = 12;
                    absDamage = RandomizeDamage(1200 * Character.RocketLauncher.CurrentLoad);
                    dmgTypes = Damage.Types.SHIELD_ABSORBER_ROCKET_CREDITS;
                    break;
                case "ammunition_rocketlauncher_sar-02":
                    rocketId = 13;
                    absDamage = RandomizeDamage(5000 * Character.RocketLauncher.CurrentLoad);
                    dmgTypes = Damage.Types.SHIELD_ABSORBER_ROCKET_URIDIUM;
                    break;
            }

            if (Character.Cooldowns.Any(cooldown => cooldown is RocketLauncherCooldown)) return;

            var newCooldown = new RocketLauncherCooldown();
            Character.Cooldowns.Add(newCooldown);

            Character.RocketLauncher.Shoot();

            //TODO: fix
            //GameClient.SendToPlayerView(Character, netty.commands.new_client.LegacyModule.write("0|RL|A|" + Character.Id + "|" + enemy.Id + "|" + Character.RocketLauncher.CurrentLoad + "|" + rocketId), true);

            Controller.Damage?.Rocket(enemy, absDamage, true, dmgTypes);
            Controller.Damage?.Rocket(enemy, damage, false, dmgTypes);

            Character.RocketLauncher.CurrentLoad = 0;
            if (player != null && enemy is Character)
            {
                //todo: fix
                //Packet.Builder.HellstormStatusCommand(World.StorageManager.GetGameSession(player.Id));
                UpdateAttacker(enemy as Character, player);
            }
        }

        private bool AssembleEnemy(IAttackable attacked)
        {
            if (attacked == null || attacked.EntityState == EntityStates.DEAD)
                return false;
            if (Character.Spacemap.Starter && attacked.FactionId == Character.FactionId)
            {
                Attacking = false;
                var player = Character as Player;
                if (player != null)
                {
                    //todo: fix
                    //Packet.Builder.LegacyModule(player.GetGameSession(), "0|A|STD|Can't attack members of your own company on starter maps.");
                }
                return false;
            }
            if (attacked.Position.DistanceTo(Character.Position) > Character.AttackRange)
            {
                var pCharacter = Character as Player;
                pCharacter?.SendLogMessage("outofrange");
                return false;
            }
            if (attacked is Player)
            {
                var attackedCharacter = (Player) attacked;
                if (attackedCharacter.State.InDemiZone) return false;
            }
            return true;
        }

        private int RandomizeDamage(int baseDmg, double missProbability = 1.00)
        {
            var randNums = Random.Next(0, 6);

            if (missProbability == 0)
                randNums = Random.Next(0, 3) | Random.Next(4, 7);
            if (missProbability < 1.00 && missProbability != 0)
                randNums = Random.Next(0, 7);
            if (missProbability > 1.00 && missProbability < 2.00)
                randNums = Random.Next(0, 4);
            if (missProbability >= 2.00)
                randNums = Random.Next(2, 4);

            switch (randNums)
            {
                case 0:
                    return (int)(baseDmg * 1.10);
                case 1:
                    return (int)(baseDmg * 0.98);
                case 2:
                    return (int)(baseDmg * 1.02);
                case 3:
                    return 0;
                case 4:
                    return (int)(baseDmg * 0.92);
                case 5:
                    return (int)(baseDmg * 0.99);
                default:
                    return baseDmg;
            }
        }

        public void Wizard(Character target)
        {
            if (target == null)
            {
                return;
            }
            var wizCooldown = new WizardCooldown();
            if (Character is Player)
                wizCooldown.Send(World.StorageManager.GetGameSession(Character.Id));
            Character.Cooldowns.Add(new WizardCooldown());

            var wizEffect = new WizardEffect();
            wizEffect.OnStart(target);                
            target.Cooldowns.Add(new WizardEffect());
        }

        public void Decelerate(Character target)
        {
            if (!(target is Player))
            {
                return;
            } 

            var decCooldown = new DecelerationCooldown();
            if (Character is Player)
                decCooldown.Send(World.StorageManager.GetGameSession(Character.Id));
            Character.Cooldowns.Add(new DecelerationCooldown());

            var decEffect = new DecelerationEffect();
            decEffect.OnStart(target);
            target.Cooldowns.Add(new DecelerationEffect());

        }

        public void Plasma(Character target)
        {
            
        }

        public void UpdateAttacker(Character target, Player player)
        {
            if (target == null || player == null) return;
            if (target.Controller.Attack.MainAttacker == null)
            {
                target.Controller.Attack.MainAttacker = player;
            }
            if (!target.Controller.Attack.Attackers.ContainsKey(Character.Id))
            {
                target.Controller.Attack.Attackers.TryAdd(Character.Id, new Attacker(player));
            }
            else
            {
                target.Controller.Attack.Attackers[player.Id].Refresh();
            }
        }

        public void RefreshAttackers()
        {
            Parallel.ForEach(Attackers, attacker =>
            {
                if (attacker.Value?.Player != null && attacker.Value.LastRefresh.AddSeconds(10) > DateTime.Now)
                {
                    //check for fade
                    if (attacker.Value.FadedToGray && MainAttacker == attacker.Value.Player)
                    {
                        //TODO: fix
                        //Packet.Builder.LegacyModule(attacker.Value.Player.GetGameSession(), $"0|n|USH|{Character.Id}");
                        attacker.Value.FadedToGray = false;
                        // fade back to red.
                    }

                    if (!attacker.Value.FadedToGray && MainAttacker != attacker.Value.Player)
                    {
                        // fade to gray
                        //todo: fix
                        //Packet.Builder.LegacyModule(attacker.Value.Player.GetGameSession(),
                        //    $"0|n|LSH|{Character.Id}|{Character.Id}");
                        attacker.Value.FadedToGray = true;
                    }
                }
                else
                {
                    Attacker removedAttacker;
                    Attackers.TryRemove(attacker.Key, out removedAttacker);
                }
            });
            if (MainAttacker != null)
            {
                if (!Attackers.ContainsKey(MainAttacker.Id))
                {
                    MainAttacker = null;
                }
            }
        }
    }
}
