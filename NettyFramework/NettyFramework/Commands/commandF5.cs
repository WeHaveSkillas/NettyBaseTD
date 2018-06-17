using NettyFramework.Utils;

namespace NettyFramework.Commands
{
    public class commandF5
    {
        public const short ID = 23340;

        public string replacement = "";

        public commandWw vare23;

        public string wildcard = "";


        public commandF5(string wildcard, string replacement, commandWw vare23)
        {
            this.replacement = replacement;
            this.wildcard = wildcard;
            this.vare23 = vare23;
        }

        public byte[] write()
        {
            var cmd = new ByteArray(ID);
            cmd.writeUTF(replacement);
            cmd.AddBytes(vare23.write());
            cmd.writeUTF(wildcard);
            return cmd.Message.ToArray();
        }
    }
}
