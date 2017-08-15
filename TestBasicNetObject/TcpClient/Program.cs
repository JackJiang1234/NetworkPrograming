using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TcpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                socket.Connect(IPAddress.Loopback, 4000);
                var sendBytes = Encoding.UTF8.GetBytes("hello tcp application!");
                var dataSize = BitConverter.GetBytes(sendBytes.Length);
                using (var netStream = new NetworkStream(socket))
                {
                    //tcp 消息边界 
                    /*
                     1. 发送固定长度消息
                     2. 将消息长度和消息一起发送
                     3. 使用特殊标记分隔消息
                     */
                    netStream.Write(dataSize, 0, dataSize.Length);
                    netStream.Write(sendBytes, 0, sendBytes.Length);
                    netStream.Flush();
                }
            }
            Console.WriteLine("send completed.");
            Console.ReadLine();
        }
    }
}