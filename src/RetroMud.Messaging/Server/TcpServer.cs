﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using RetroMud.Messaging.Config;

namespace RetroMud.Messaging.Server
{
    public class TcpServer : ITcpServer
    {
        private TcpListener _tcpListener;
        private Thread _serverThread;
        private bool _stopServer;
        private readonly List<ISocketHandler> _listeners = new List<ISocketHandler>();
        private readonly ITcpConfiguration _tcpConfiguration;
        private readonly object _cleanupLock = new object();

        public TcpServer()
            : this(new TcpConfiguration())
        {
            
        }

        public TcpServer(ITcpConfiguration tcpConfiguration)
        {
            _tcpConfiguration = tcpConfiguration;
        }

        public void Start()
        {
            try
            {
                _tcpListener = new TcpListener(
                    new IPEndPoint(
                        IPAddress.Parse(_tcpConfiguration.IpAddress
                    ), _tcpConfiguration.Port)
                );
            }
            catch (Exception ex)
            {
                _tcpListener = null;
            }

            if (_tcpListener == null) return;

            _tcpListener.Start();
            _serverThread = new Thread(ServerThreadStart);
            _serverThread.Start();
        }

        private void ServerThreadStart()
        {
            while (!_stopServer)
            {
                try
                {
                    //blocking method
                    var socket = _tcpListener.AcceptSocket();
                    
                    var socketListener = TcpServerFactory.GetSocketHandler(socket);
                    socketListener.StartWorker();

                    lock(_cleanupLock)
                    {
                        _purgeCompletedListeners();
                        _listeners.Add(socketListener);
                    }
                }
                catch (SocketException se)
                {
                    _stopServer = true;
                }
            }
        }

        public void Stop()
        {
            if (_tcpListener == null) return;

            _stopServer = true;
            _tcpListener.Stop();

            lock (_cleanupLock)
            {
                _purgeCompletedListeners();
            }
        }

        private void _purgeCompletedListeners()
        {
            var completedListeners = _listeners.Where(x => x.Completed).ToList();

            foreach (var listener in completedListeners)
            {
                listener.Cleanup();
                _listeners.Remove(listener);
            }

            completedListeners = null;
        }
    }
}
