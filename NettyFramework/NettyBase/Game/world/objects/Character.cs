﻿using System;
using System.Linq;
using System.Threading.Tasks;
using NettyBase.Game.controllers;
using NettyBase.Game.world.objects.characters;
using NettyBase.Main;
using NettyBase.Main.objects;

namespace NettyBase.Game.world.objects
{
    class Character : IAttackable
    {
        /**********
         * BASICS *
         **********/

        public string Name { get; set; }

        public Hangar _hangar;
        public virtual Hangar Hangar
        {
            get
            {
                if (this is Player)
                {
                    var temp = (Player) this;
                    return temp.Hangar;
                }
                return _hangar;
            }
            set
            {
                if (this is Player)
                {
                    var temp = (Player)this;
                    temp.Hangar = value;
                }
                _hangar = value;
            }
        }

        public override Faction FactionId { get; set; }
        public Reward Reward { get; }

        public Clan Clan { get; set; }

        public virtual AbstractCharacterController Controller
        {
            get
            {
                if (this is Player)
                {
                    var temp = (Player) this;
                    return temp.Controller;
                }
                if (this is Npc)
                {
                    var temp = (Npc) this;
                    return temp.Controller;
                }
                if (this is Pet)
                {
                    var temp = (Pet) this;
                    return temp.Controller;
                }
                return null;
            }
        }

        /************
         * POSITION *
         ************/
        public override Vector Position { get; set; }

        public int VirtualWorldId { get; set; }

        private Spacemap _baseSpacemap;
        public override Spacemap Spacemap
        {
            get
            {
                var spacemap = _baseSpacemap;
                if (VirtualWorldId != 0 && spacemap.VirtualWorlds.ContainsKey(VirtualWorldId))
                    return spacemap.VirtualWorlds[VirtualWorldId];
                return spacemap;
            }
            set
            {
                if (VirtualWorldId == 0)
                    _baseSpacemap = value;
                else Spacemap.VirtualWorlds[VirtualWorldId] = value;
            }
        }

        /*********
         * STATS *
         *********/
        public override int MaxHealth { get; set; }

        private int _currentHealth;

        public override int CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                _currentHealth = (value < MaxHealth) ? value : MaxHealth;
                if (value < 0) _currentHealth = 0;
            }
        }

        public override int MaxShield { get; set; }
        public override int CurrentShield { get; set; }
        public override double ShieldAbsorption { get; set; }
        public override double ShieldPenetration { get; set; }

        //The max amount of nanohull will be the max ship health
        public override int MaxNanoHull => Hangar.Ship.Health;

        private int _currentNanoHull;

        public override int CurrentNanoHull
        {
            get { return _currentNanoHull; }
            set
            {
                _currentNanoHull = (value < MaxNanoHull) ? value : MaxNanoHull;
                if (value < 0) _currentNanoHull = 0;
            }
        }

        public virtual int Speed => Hangar.Ship.Speed;

        public virtual int Damage { get; set; }
        public virtual int RocketDamage { get; set; }

        /************
         * MOVEMENT *
         ************/
        public bool Moving { get; set; }
        public Vector OldPosition { get; set; }
        public Vector Destination { get; set; }
        public Vector Direction { get; set; }
        public DateTime MovementStartTime { get; set; }
        public int MovementTime { get; set; }

        /*********
         * EXTRA *
         *********/
        public int RenderRange { get; set; }
        public IAttackable Selected { get; set; }
        public Character SelectedCharacter => Selected as Character;

        public Range Range { get; }

        public virtual RocketLauncher RocketLauncher { get; set; }

        public virtual Skilltree Skills { get; set; }

        public DroneFormation Formation = DroneFormation.STANDARD;

        public CooldownsAssembly Cooldowns;

        public Updaters Updaters { get; set; }

        protected Character(int id, string name, Hangar hangar, Faction factionId, Vector position, Spacemap spacemap,
            Reward rewards, Clan clan = null) : base(id)
        {
            Name = name;
            Hangar = hangar;
            FactionId = factionId;
            Position = position;
            Spacemap = spacemap;
            Reward = rewards;
            Clan = clan;

            //Default initialization
            Moving = false;
            OldPosition = new Vector(0, 0);
            Destination = position;
            Direction = new Vector(0, 0);
            MovementStartTime = new DateTime();
            MovementTime = 0;

            RenderRange = 2000;
            Range = new Range {Character = this};

            Skills = new Skilltree(this);
            Updaters = new Updaters(this);
            Cooldowns = new CooldownsAssembly(this);

            LastCombatTime = DateTime.Now;
            if (clan == null)
            {
                Clan = Global.StorageManager.Clans[0];
            }
            
            Ticked += AssembleTick;
        }

        public virtual void AssembleTick(object sender, EventArgs eventArgs)
        {
            Parallel.Invoke(() =>
            {
                Cooldowns.Tick();
                RocketLauncher?.Tick();
            });
        }

        public EventHandler Ticked;
        public override void Tick()
        {
            Ticked?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateShip()
        {
            var sessions =
                World.StorageManager.GameSessions.Where(
                    x => x.Value.Client != null && InRange(x.Value.Player));
            foreach (var session in sessions)
            {
                if (session.Key != Id)
                {
                    //TODO: fix
                    //Packet.Builder.ShipCreateCommand(session.Value, this);
                }
            }

            if (this is Player)
            {
                //TODO: fix
                //Packet.Builder.ShipInitializationCommand(World.StorageManager.GetGameSession(Id));
            }

            Updaters.Update();
        }

        public virtual void SetPosition(Vector targetPosition)
        {
            Destination = targetPosition;
            Position = targetPosition;
            OldPosition = targetPosition;
            Direction = targetPosition;
            Moving = false;

            MovementController.Move(this, MovementController.ActualPosition(this));
        }

        public override void Destroy()
        {
            Controller.Destruction.Destroy(this);
        }

        public override void Destroy(Character destroyer)
        {
            if (destroyer == null)
            {
                Destroy();
                return;
            }
            destroyer.Controller.Destruction.Destroy(this);
        }
    }
}
