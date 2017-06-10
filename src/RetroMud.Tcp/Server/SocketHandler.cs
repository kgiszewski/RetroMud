using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using RetroMud.Tcp.Config;

namespace RetroMud.Tcp.Server
{
    public class SocketHandler
    {
        private readonly Socket _socket;
        private Thread _workerThread;
        public bool Completed;
        private readonly ITcpConfiguration _tcpConfiguration;

        public SocketHandler(Socket socket)
            :this(socket, new TcpConfiguration())
        {
            
        }

        public SocketHandler(Socket socket, ITcpConfiguration tcpConfiguration)
        {
            _socket = socket;
            _tcpConfiguration = tcpConfiguration;
        }

        public void StartSocketListener()
        {
            if (_socket == null) return;

            _workerThread = new Thread(_dispatchSocket);

            _workerThread.Start();
        }

        //find correct handler and execute
        private void _dispatchSocket()
        {
            try
            {
                Console.WriteLine("Dispatching...");
                var buffer = new byte[_tcpConfiguration.ReadBufferSizeInBytes];
                var numberBytes = _socket.Receive(buffer);

                var message = Encoding.UTF8.GetString(buffer, 0, numberBytes);

                Console.WriteLine();

                _socket.Send(Encoding.UTF8.GetBytes("My response to you!" + message));
            }
            catch (SocketException se)
            {
                throw;
            }
            finally
            {
                Completed = true;
            }
        }

        public void Cleanup()
        {
            if (_workerThread.IsAlive)
            {
                _workerThread.Abort();
            }

            _socket.Close();
        }
    }
}
