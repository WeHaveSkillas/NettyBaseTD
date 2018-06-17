using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class ShipInitializationCommand
    {
        public const short ID = 24873;
        public static byte[] write(int userId, string userName, string typeId, int speed, int shield, int shieldMax, int hitPoints, int hitMax, int cargoSpace, int cargoSpaceMax,
            int nanoHull, int maxNanoHull, int x, int y, int mapId, int factionId, int clanId, int expansionStage, bool premium, double ep, double honourPoints, int level,
            double credits, double uridium, float jackpot, int dailyRank, string clanTag, int galaxyGatesDone, bool useSystemFont, bool cloaked, bool var83D, List<VisualModifierCommand> modifiers)
        {
            var cmd = new ByteArray(ID);
            cmd.writeInt(galaxyGatesDone << 11 | galaxyGatesDone >> 21);
            cmd.writeDouble(credits);
            cmd.writeInt(mapId << 1 | mapId >> 31);
            cmd.writeDouble(ep);
            cmd.writeInt(hitMax << 7 | hitMax >> 25);
            cmd.writeBoolean(var83D);
            cmd.writeInt(nanoHull >> 16 | nanoHull << 16);
            cmd.writeFloat(jackpot);
            cmd.writeInt(factionId >> 4 | factionId << 28);
            cmd.writeInt(hitPoints >> 13 | hitPoints << 19);
            cmd.writeInt(cargoSpaceMax << 8 | cargoSpaceMax >> 24);
            cmd.writeInt(userId << 14 | userId >> 18);
            cmd.writeInt(shield << 7 | shield >> 25);
            cmd.writeInt(clanId >> 3 | clanId << 29);
            cmd.writeBoolean(cloaked);
            cmd.writeInt(speed << 9 | speed >> 23);
            cmd.writeInt(x << 12 | x >> 20);
            cmd.writeBoolean(useSystemFont);
            cmd.writeInt(maxNanoHull >> 12 | maxNanoHull << 20);
            cmd.writeShort(10323);
            cmd.writeBoolean(premium);
            cmd.writeInt(modifiers.Count);
            foreach(var _loc2_ in modifiers)
            {
                cmd.AddBytes(_loc2_.write());
            }
            cmd.writeInt(dailyRank << 2 | dailyRank >> 30);
            cmd.writeDouble(uridium);
            cmd.writeInt(cargoSpace >> 1 | cargoSpace << 31);
            cmd.writeInt(level >> 14 | level << 18);
            cmd.writeShort(27608);
            cmd.writeUTF(typeId);
            cmd.writeDouble(honourPoints);
            cmd.writeInt(shieldMax >> 9 | shieldMax << 23);
            cmd.writeUTF(clanTag);
            cmd.writeUTF(userName);
            cmd.writeInt(y >> 11 | y << 21);
            cmd.writeInt(expansionStage >> 15 | expansionStage << 17);
            return cmd.ToByteArray();
        }
    }
}
