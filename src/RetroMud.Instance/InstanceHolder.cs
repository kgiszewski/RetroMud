using System.Threading.Tasks;
using RetroMud.Core.Context;
using RetroMud.Tcp.Server;

namespace RetroMud.Instance
{
    public class InstanceHolder
    {
        private TcpServer _tcpServer;

        public void Start()
        {
            InstanceContext.Instance.Start();
            
            _tcpServer = new TcpServer();

            Task.Run(() => _tcpServer.StartServer());
        }

        public void Stop()
        {
            _tcpServer.StopServer();
        }
    }
}
