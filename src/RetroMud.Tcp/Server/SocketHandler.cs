using System.Net.Sockets;
using System.Threading;
using RetroMud.Tcp.Messaging;

namespace RetroMud.Tcp.Server
{
    public class SocketHandler
    {
        private readonly Socket _socket;
        private Thread _workerThread;
        public bool Completed;
        private readonly IDispatchMessages _messageDispatcher;

        public SocketHandler(Socket socket)
            :this(socket, new MessageDispatcher())
        {
            
        }

        public SocketHandler(Socket socket, IDispatchMessages messageDispatcher)
        {
            _socket = socket;
            _messageDispatcher = messageDispatcher;
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
                _messageDispatcher.Dispatch(_socket);
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
