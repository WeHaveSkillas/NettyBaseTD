using NettyBase.Game.controllers.player;

namespace NettyBase.Game.world.objects.players.equipment.extras
{
    class Robot : Extra
    {
        public Robot(Player player, int id, string lootId, int amount) : base(player, id, lootId, amount)
        {
        }

        public int GetLevel()
        {
            switch (LootId)
            {
                case "equipment_extra_repbot_rep-s":
                    return 1;
                case "equipment_extra_repbot_rep-1":
                    return 2;
                case "equipment_extra_repbot_rep-2":
                    return 3;
                case "equipment_extra_repbot_rep-3":
                    return 4;
                case "equipment_extra_repbot_rep-4":
                    return 5;
            }
            return 0;
        }

        public override void execute()
        {
            base.execute();
            Player.Controller.Repairing = true;
            Player.Controller.CPUs.Activate(CPU.Types.ROBOT);
        }
    }
}
