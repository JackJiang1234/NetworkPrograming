using System;
using System.Net;
using System.Threading.Tasks;

namespace TestBasicNetObject
{
    public class Program
    {
        static void Main(string[] args)
        {
            var localHostName = Dns.GetHostName();
            var ips = Dns.GetHostAddressesAsync(Dns.GetHostName()).Result;

            Console.WriteLine($"local host name:{localHostName}");
            foreach(var ip in ips)
            {
                Console.WriteLine($"ip address:{ip}");
            }

            Console.ReadLine();
        }
    }
}
