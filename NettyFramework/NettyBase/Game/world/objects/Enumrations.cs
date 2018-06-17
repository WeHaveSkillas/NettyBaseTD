﻿namespace NettyBase.Game.world.objects
{
    #pragma warning disable 1591
    enum RewardType
    {
        CREDITS,
        URIDIUM,
        EXPERIENCE,
        HONOR,
        ITEM,
        GALAXY_GATES_ENERGY, 
        BOOSTER,
        AMMO,
        ORE
    }
    
    /// <summary>
    /// Faction Ids
    /// </summary>
    public enum Faction
    {
        NONE = 0,
        MMO = 1,
        EIC = 2,
        VRU = 3
    }

    /// <summary>
    /// Rank Ids
    /// </summary>
    public enum Rank
    {
        NONE = 0,
        BASIC_PILOT = 1,
        SPACE_PILOT = 2,
        CHIEF_PILOT = 3,
        BASIC_SERGEANT = 4,
        SERGEANT = 5,
        CHIEF_SERGEANT = 6,
        BASIC_LIEUTENANT = 7,
        LIEUTENANT = 8,
        CHIEF_LIEUTENANT = 9,
        BASIC_CAPTAIN = 10,
        CAPTAIN = 11,
        CHIEF_CAPTAIN = 12,
        BASIC_MAJOR = 13,
        MAJOR = 14,
        CHIEF_MAJOR = 15,
        BASIC_COLONEL = 16,
        COLONEL = 17,
        CHIEF_COLONEL = 18,
        BASIC_GENERAL = 19,
        GENERAL = 20,
        ADMINISTRATOR = 21,
        WANTED = 22
    }

    /// <summary>
    /// Drone types
    /// </summary>
    public enum DroneType
    {
        UNDEFINED = 0,
        FLAX = 1,
        IRIS = 2,
        APIS = 3,
        ZEUS = 4
    }

    public enum DroneFormation
    {
        STANDARD,
        TURTLE,
        ARROW,
        LANCE,
        STAR,
        PINCER,
        DOUBLE_ARROW,
        DIAMOND,
        CHEVRON,
        MOTH,
        CRAB,
        HEART,
        BARRAGE,
        BAT
    }

    public enum DamageType
    {
        DEFINED,
        PERCENTAGE
    }

    public enum HealType
    {
        HEALTH,
        SHIELD
    }

    public enum AILevels
    {
        PASSIVE,
        AGGRESSIVE,
        MOTHERSHIP,
        DAUGHTER,
        GALAXY_GATES,
        INVASION
    }

    public enum ShipVisuals
    {
        TRAVEL_MODE = 0,
        HEALING_POD = 1,
        FORTIFY = 2,
        PROTECT_OWNER = 3,
        PROTECT_TARGET = 4,
        DRAW_FIRE_OWNER = 5,
        DRAW_FIRE_TARGET = 6,
        ULTIMATE_EMP_TARGET = 7,
        INACTIVE = 8,
        FORTRESS = 9,
        PRISMATIC_SHIELD = 10,
        WEAKEN_SHIELDS = 11,
        WEAKEN_SHIELDS_TARGET = 12,
        SINGULARITY = 13,
        SINGULARITY_TARGET = 14,
        SHIP_WARP = 15,
        NPC_INFILTRATOR = 16,
        LEONOV_EFFECT = 17,
        WIZARD_ATTACK = 18,
        GHOST_EFFECT = 19,
        MIRRORED_CONTROLS = 20,
        STICKY_BOMB = 21,
        GREEN_GLOW = 22,
        RED_GLOW = 23,
        GENERIC_GLOW = 24,
        EMERGENCY_REPAIR = 25,
        INVINCIBILITY = 26,
        BATTLESTATION_DEFLECTOR = 27,
        BATTLESTATION_DOWNTIME_TIMER = 28,
        BATTLESTATION_INSTALLING = 29,
        BATTLESTATION_CONSTRUCTING = 30,
        OWNS_BATTLESTATION = 31,
        BUFFZONE = 32,
        BLOCKED_ZONE_EXPLOSION = 33,
        NPC_DECLOAK_ZONE = 34,
        LEGENDARY_NPC_NAME = 35
    }

    enum OreCollection
    {
        IN_PROGRESS,
        FINISHED,
        FAILED_CARGO_FULL,
        FAILED_ALREADY_COLLECTED
    }
    #pragma warning restore 1591

}
