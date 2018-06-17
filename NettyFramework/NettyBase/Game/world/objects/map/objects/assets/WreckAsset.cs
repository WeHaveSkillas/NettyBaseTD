namespace NettyBase.Game.world.objects.map.objects.assets
{
    class WreckAsset : Asset
    {
        public WreckAsset(Player destroyedPlayer) : base(destroyedPlayer.Spacemap.GetNextObjectId(), destroyedPlayer.Name, AssetTypes.WRECK, destroyedPlayer.FactionId, destroyedPlayer.Clan, 1, 1, destroyedPlayer.Position, destroyedPlayer.Spacemap, false, false, false)
        {
        }
    }
}
