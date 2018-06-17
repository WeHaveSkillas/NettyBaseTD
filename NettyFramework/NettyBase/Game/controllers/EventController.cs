using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NettyBase.Game.controllers.events;
using NettyBase.Game.world.objects.players;

namespace NettyBase.Game.controllers
{
    class EventController
    {
        public PlayerEvent Event { get; set; }
        private IEvent CurrentEvent { get; set; }

        public EventController(PlayerEvent gameEvent)
        {
            Event = gameEvent;
        }

        public void Set()
        {
        }

        public void Start()
        {

        }

        public void Update()
        {

        }

        public void Finish()
        {

        }
    }
}
