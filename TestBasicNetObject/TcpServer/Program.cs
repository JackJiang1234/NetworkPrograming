using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Bind(new IPEndPoint(IPAddress.Loopback, 4000));
                socket.Listen(10);
                while (true)
                {
                    var newSocket = socket.Accept();
                    Task.Factory.StartNew(AcceptMessage, newSocket);
                }
            }  
        }

        static void AcceptMessage(object state)
        {
            var newSocket = (Socket)state;
            if (newSocket.Connected)
            {
                Console.WriteLine($"from {newSocket.RemoteEndPoint} connect...");
                using (var netStream = new NetworkStream(newSocket))
                {
                    var dateSize = new byte[4];
                    netStream.Read(dateSize, 0, 4);
                    int size = BitConverter.ToInt32(dateSize, 0);
                    var message = new byte[size];
                    int dataLeft = size;
                    int start = 0;
                    while(dataLeft > 0)
                    {
                        int recv = netStream.Read(message, start, dataLeft);
                        start += recv;
                        dataLeft -= recv;
                    }
                    Console.WriteLine($"accept content:{Encoding.UTF8.GetString(message)}");
                }
            }
            else
            {
                Console.WriteLine($"from {newSocket.RemoteEndPoint}  connect failed.");
            }
        }
    }
}