using System.Threading.Tasks;
using RetroMud.Core.Context;
using RetroMud.Messaging.Server;

namespace RetroMud.Instance
{
    public class InstanceHolder
    {
        private ITcpServer _tcpServer;

        public void Start()
        {
            InstanceContext.Instance.Start();

            _tcpServer = TcpServerFactory.GetServer();

            Task.Run(() => _tcpServer.Start());
        }

        public void Stop()
        {
            _tcpServer.Stop();
            InstanceContext.Instance.Stop();
        }
    }
}
