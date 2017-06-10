

using System.Threading.Tasks;
using RetroMud.Instance.Tcp;

namespace RetroMud.Instance
{
    public class ServiceHolder
    {
        private TcpServer _tcpServer;

        public void Start()
        {
            _tcpServer = new TcpServer();

            Task.Run(() => _tcpServer.StartServer());
        }

        public void Stop()
        {
            _tcpServer.StopServer();
        }
    }
}
