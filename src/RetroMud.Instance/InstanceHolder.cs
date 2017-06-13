using System.Threading.Tasks;
using log4net.Config;
using RetroMud.Core.Context;
using RetroMud.Core.Logging;
using RetroMud.Messaging.Server;

namespace RetroMud.Instance
{
    public class InstanceHolder
    {
        private ITcpServer _tcpServer;

        public void Start()
        {
            InstanceContext.Instance.Start();

            XmlConfigurator.Configure();

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
