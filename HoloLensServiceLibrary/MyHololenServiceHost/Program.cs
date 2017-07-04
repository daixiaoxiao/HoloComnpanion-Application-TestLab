using HoloLensServiceLibrary;
using System;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace MyHololenServiceHost
{
    class Program
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        static void Main(string[] args)
        {
            ServiceHost svcHost = null;
            try
            {
                string ip = GetLocalIPAddress();
                string httpBaseAddress = "http://" + ip + ":9001/MyHololenService";
                string tcpBaseAddress = "net.tcp://" + ip + ":9002/MyHololenService";
                svcHost = new ServiceHost(typeof(MyHololenService), new Uri[] { new Uri(httpBaseAddress),
                                                                                new Uri(tcpBaseAddress)
                                            });
                //The URI specified for the endpoint address can be a fully-qualified path or a path that is relative to the service's base address. 
                //If you do not specify a relative address, the service uses the base address. 
                ServiceEndpoint tcpEndpoint = svcHost.AddServiceEndpoint(typeof(IMyHololenService), new NetTcpBinding(),
                                                                         "");

                ServiceEndpoint httpEndpoint = svcHost.AddServiceEndpoint(typeof(IMyHololenService), new BasicHttpBinding(),
                                                                                 "");

                //Enable metadata exchange
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                svcHost.Description.Behaviors.Add(smb);

                svcHost.Open();
                
                Console.WriteLine("\n\nService is Running at following address\n");
                Console.WriteLine("{0} ({1})\n", tcpEndpoint.Address.ToString(), tcpEndpoint.Binding.Name);
                Console.WriteLine("{0} ({1})", httpEndpoint.Address.ToString(), httpEndpoint.Binding.Name);
            }
            catch (Exception eX)
            {
                svcHost = null;
                Console.WriteLine("Service can not be started \n\nError Message [" + eX.Message + "]");
            }
            if (svcHost != null)
            {
                Console.WriteLine("\nPress any key to close the Service");
                Console.ReadKey();
                svcHost.Close();
                svcHost = null;
            }
        }
    }
}
