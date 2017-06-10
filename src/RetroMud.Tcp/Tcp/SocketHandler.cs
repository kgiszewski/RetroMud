using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RetroMud.Instance.Tcp
{
    public class SocketHandler
    {
        private readonly Socket _socket;
        private Thread _workerThread;
        public bool Completed;

        public SocketHandler(Socket clientSocket)
        {
            _socket = clientSocket;
        }

        //Let us see the 'StartSocketListener' method.
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
                var buffer = new byte[128];
                _socket.Receive(buffer);
                Console.WriteLine(Encoding.ASCII.GetString(buffer));

                _socket.Send(Encoding.ASCII.GetBytes("My response to you!"));
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
