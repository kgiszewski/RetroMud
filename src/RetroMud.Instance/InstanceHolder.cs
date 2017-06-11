using System;
using System.Threading.Tasks;
using RetroMud.Core.Config;
using RetroMud.Core.Context;
using RetroMud.Core.MessageHandlers;
using RetroMud.Tcp.Server;

namespace RetroMud.Instance
{
    public class InstanceHolder
    {
        private TcpServer _tcpServer;

        public void Start()
        {
            //this is to trick the preload the assembly that contains the core handlers
            var foo = typeof(FooHandler);

            GameContext.Instance.Configuration = new GameContextConfiguration();
            InstanceContext.Instance.Configuration = new InstanceConfiguration();

            _tcpServer = new TcpServer();

            Task.Run(() => _tcpServer.StartServer());
        }

        public void Stop()
        {
            _tcpServer.StopServer();
        }
    }
}
