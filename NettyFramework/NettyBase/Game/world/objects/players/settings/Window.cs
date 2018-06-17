using System.Collections.Generic;
using NettyFramework.Commands;

namespace NettyBase.Game.world.objects.players.settings
{
    class Window
    {
        public static short STANDARD = ClientUITooltipTextFormat.STANDARD;
        public static short RED = ClientUITooltipTextFormat.RED;

        public WindowButtonModule _window { get; set; }
        public List<ClientUITooltipTextFormat> TextFormat { get; set; }

        public Window(string windowId, string tooltipId, bool visible = true) : this(windowId, tooltipId, visible,
            ClientUITooltipTextFormat.STANDARD)
        {
        }

        //If you want to change the text color :D
        public Window(string windowId, string tooltipId, bool visible, short textColor)
        {
            TextFormat = new List<ClientUITooltipTextFormat>();
            TextFormat.Add(new ClientUITooltipTextFormat(textColor, tooltipId, new commandWw(commandWw.LOCALIZED),
                new List<commandF5>()));

            var tooltip = new ClientUITooltip(TextFormat);
            _window = new WindowButtonModule(windowId, visible, tooltip);
        }
    }
}
