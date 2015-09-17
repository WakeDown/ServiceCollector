using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SnmpScanner
{
    class PjlCommander
    {
        Socket socket = new Socket(AddressFamily.Unknown, SocketType.Stream, ProtocolType.Unknown);
    }
}
