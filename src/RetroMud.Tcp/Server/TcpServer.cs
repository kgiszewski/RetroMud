using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RetroMud.TcpClient.Server
{
    public class TcpServer
    {
        private readonly TcpListener _tcpListener;
        private Thread _serverThread;
        private bool _stopServer;
        private readonly List<SocketHandler> _listeners = new List<SocketHandler>();

        public TcpServer()
        {
            try
            {
                _tcpListener = new TcpListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"),  30001));
            }
            catch (Exception ex)
            {
                _tcpListener = null;
            }
        }

        public void StartServer()
        {
            if (_tcpListener != null)
            {
                _tcpListener.Start();
                _serverThread = new Thread(ServerThreadStart);
                _serverThread.Start();
            }
        }

        private void ServerThreadStart()
        {
            while (!_stopServer)
            {
                try
                {
                    //blocking method
                    var socket = _tcpListener.AcceptSocket();
                    
                    var socketListener = new SocketHandler(socket);
                    socketListener.StartSocketListener();

                    _listeners.Add(socketListener);
                }
                catch (SocketException se)
                {
                    _stopServer = true;
                }

                _purgeCompletedListeners();
            }
        }

        public void StopServer()
        {
            if (_tcpListener != null)
            {
                _stopServer = true;
                _tcpListener.Stop();
            }
        }

        private void _purgeCompletedListeners()
        {
            foreach (var listener in _listeners.Where(x => x.Completed).ToList())
            {
                listener.Cleanup();
            }
        }
    }
}
