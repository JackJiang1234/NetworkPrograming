using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpTimeClient
{
    class Program
    {
        private const int portNum = 13;
        private const string hostName = "localhost";

        public static int Main(String[] args)
        {
            try
            {
                TcpClient client = new TcpClient(hostName, portNum);

                NetworkStream ns = client.GetStream();

                byte[] bytes = new byte[1024];
                int bytesRead = ns.Read(bytes, 0, bytes.Length);

                Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRead));

                client.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return 0;
        }
    }
}
