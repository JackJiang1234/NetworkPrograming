using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpClientA
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint myhost = new IPEndPoint(IPAddress.Any, 8081);
            socket.Bind(myhost);
            EndPoint remote = new IPEndPoint(IPAddress.Loopback, 8080);
            var buffer = new byte[1024];

            while (true)
            {
                Console.Write("please input send message:");
                string tempStr = Console.ReadLine();
                var bytes = Encoding.Unicode.GetBytes(tempStr);
                socket.SendTo(bytes, bytes.Length, SocketFlags.None, remote);
                var length = socket.ReceiveFrom(buffer, ref remote);
                var back = Encoding.Unicode.GetString(buffer, 0, length);
                Console.WriteLine($"back content: {back}");
                if (tempStr == "exit")
                {
                    break;
                }
            }
            socket.Shutdown(SocketShutdown.Both);
            socket.Dispose();
        }
    }
}