using System.Threading.Tasks;
using RetroMud.Core.Handlers;
using RetroMud.Tcp.Server;

namespace RetroMud.Instance
{
    public class ServiceHolder
    {
        private TcpServer _tcpServer;

        public void Start()
        {
            //this is to trick the linker to include the core assembly. can remove later
            var foo = typeof(FooHandler);

            _tcpServer = new TcpServer();

            Task.Run(() => _tcpServer.StartServer());
        }

        public void Stop()
        {
            _tcpServer.StopServer();
        }
    }
}
