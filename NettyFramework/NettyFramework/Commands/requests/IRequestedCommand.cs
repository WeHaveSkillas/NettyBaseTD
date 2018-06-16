using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NettyFramework.Commands.requests
{
    interface IRequestedCommand
    {
        void readCommand(byte[] bytes);
    }
}
