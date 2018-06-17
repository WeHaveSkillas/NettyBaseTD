using System;
using System.Linq;
using NettyBase.Game.world.objects.characters.cooldowns;
using NettyBase.Game.world.objects.map.objects;
using NettyBase.Game.world.objects.map.objects.assets;
using Object = NettyBase.Game.world.objects.map.Object;

namespace NettyBase.Game.world.objects.players.extra.abilities
{
    class AegisHealPod : Ability
    {
        private Asset Beacon;

        public override int ActivatorId => Beacon.Id;

        public AegisHealPod(Player player) : base(player, Abilities.SHIP_ABILITY_AEGIS_HEALING_POD)
        {
        }

        public override void ThreadUpdate() => Pulse();

        public override void Tick()
        {
            Update();
        }

        public override void execute()
        {
            if (!Enabled) return;
            Active = true;
            TimeFinish = DateTime.Now.AddSeconds(10);
            var id = Player.Spacemap.GetNextObjectId();
            Beacon = new Asset(id, "", AssetTypes.HEALING_POD, Player.FactionId, Player.Clan, 1, 1, Player.Position,
                Player.Spacemap, false, false, false);
            Player.Spacemap.AddObject(Beacon);
            Start();
        }

        private void Pulse()
        {
            if (TimeFinish < DateTime.Now)
            {
                Active = false;
                End();
                Cooldown = new AegisHealPodCooldown(this);
                Object beacon;
                Beacon.Spacemap.Objects.TryRemove(Beacon.Id, out beacon);
                return;
            }

            TargetIds.Clear();
            foreach (var entity in Beacon.Spacemap.Entities.Where(x =>
                x.Value.Position.DistanceTo(Beacon.Position) < 300))
            {
                if (Player.Group != null && Player.Group.Members.ContainsKey(entity.Key) || Player == entity.Value)
                {
                    TargetIds.Add(entity.Key);
                    entity.Value.Controller.Heal.Execute(15000, Player.Id);
                }
            }

            ShowEffect();
        }
    }
}
