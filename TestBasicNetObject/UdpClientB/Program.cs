using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpClientB
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] dataBytes = new byte[1024];
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint myhost = new IPEndPoint(IPAddress.Any, 8080);
            socket.Bind(myhost);
            EndPoint remote = new IPEndPoint(IPAddress.Loopback, 8080);

            while (true)
            {
                Console.WriteLine("wait accept...");
                var length = socket.ReceiveFrom(dataBytes, ref remote);
                var result = Encoding.Unicode.GetString(dataBytes, 0, length);
                Console.WriteLine("accept message:{0}", result);

                var back = Encoding.Unicode.GetBytes("from server," + result);
                socket.SendTo(back, back.Length, SocketFlags.None, remote);
                if (result == "exit")
                {
                    Console.WriteLine("exit");
                    break;
                }
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Dispose();
        }
    }
}