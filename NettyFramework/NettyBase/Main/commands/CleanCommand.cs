using NettyBase.Utils;

namespace NettyBase.Main.commands
{
    class CleanCommand : Command
    {
        public CleanCommand() : base("clean", "Clears the console")
        {

        }

        public override void Execute(string[] args = null)
        {
            Draw.Logo();
        }
    }
}