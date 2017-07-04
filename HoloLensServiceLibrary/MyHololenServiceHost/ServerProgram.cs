using HoloLensServiceLibrary;
using NetFwTypeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace HololensServiceHost
{
    class ServerProgram
    {
        private static int PortNumber = 9002;
        private static Type serviceType = typeof(TLHoloCompanionService);
        private static string serviceAddress = "/TLCompanionsService";
        private static bool ipcHttpState = false;

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

        public static int GetAvailablePort(int startingPort)
        {
            IPEndPoint[] endPoints;
            List<int> portArray = new List<int>();

            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();

            //getting active connections
            TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
            portArray.AddRange(from n in connections
                               where n.LocalEndPoint.Port >= startingPort
                               select n.LocalEndPoint.Port);

            //getting active tcp listners - WCF service listening in tcp
            endPoints = properties.GetActiveTcpListeners();
            portArray.AddRange(from n in endPoints
                               where n.Port >= startingPort
                               select n.Port);

            //getting active udp listeners
            endPoints = properties.GetActiveUdpListeners();
            portArray.AddRange(from n in endPoints
                               where n.Port >= startingPort
                               select n.Port);

            portArray.Sort();

            for (int i = startingPort; i < ushort.MaxValue; i++)
                if (!portArray.Contains(i))
                    return i;

            return 0;
        }

        public static void parseArgs(string arg)
        {
            if (arg == "-test")
            {
                serviceType = typeof(TestHoloCompanionsService);
                serviceAddress = "/TestHoloCompanionsService";
            }
            else if (arg == "-help")
            {
                Console.WriteLine("You can specify portnumber and IPC.\n");
                Console.WriteLine("The Program will run on TCP under Port 9002 with TLHoloCompanionService by default.\n");
                Console.WriteLine("Usage: ServerProgram -Port:<PortNumber> -Ipc:<Http/Tcp> -test -help\n");
                Console.WriteLine("Example : ServerProgram -Port:2000 -Ipc:Http -test -help");
            }
            else
            {
                char[] separator = ":".ToCharArray();
                var parts = arg.Split(separator);

                if (parts[0] == "-Port")
                {
                    bool test = int.TryParse(parts[1], out PortNumber);
                    if (test == false)
                    {
                        Console.WriteLine("You are not entering correct Int PortNumber.\n");
                        Console.ReadKey();
                    }
                }
                if (parts[0] == "-Ipc")
                {
                    if (parts[1] == "Http")
                    {
                        ipcHttpState = true;
                    }
                    else if (parts[1] == "Tcp")
                    {
                        ipcHttpState = false;
                    }
                    else
                    {
                        Console.WriteLine("You are entering wrong Ipc type, please specify only Http or Tcp.");
                        Console.Read();
                    }
                }
            }
        }

        public static void turnOnPortInFireWall(int port)
        {
            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            firewallPolicy.Rules.Remove("WCFServicePort");
            INetFwRule firewallRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            firewallRule.Name = "WCFServicePort";
            firewallRule.Description = "Enables Port" + port.ToString() + "for WCF Server.";
            firewallRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
            firewallRule.LocalPorts = port.ToString();
            firewallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
            firewallRule.Enabled = true;
            firewallRule.Grouping = "@firewallapi.dll, -23255";
            firewallRule.Profiles = firewallPolicy.CurrentProfileTypes;
            firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
            firewallRule.InterfaceTypes = "All";

            firewallPolicy.Rules.Add(firewallRule);
        }

        public static void turnOffPortInFireWall()
        {
            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            firewallPolicy.Rules.Remove("WCFServicePort");
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Add -help to know more if you want to specify arguments."); 
            }
            else if(args.Length > 4)
            {
                Console.WriteLine("Too many Arguments, at most 4 needed.");
                Console.ReadKey();
            }
            else
            {
                for (int i = 0; i < args.Length; i++)
                {
                    parseArgs(args[i]);
                }
            }

            ServiceHost svcHost = null;
            try
            {
                ServiceEndpoint httpEndpoint = null;
                ServiceEndpoint tcpEndpoint = null;
                string ip = GetLocalIPAddress();
                string baseAddress = null;

                //string HttpBaseAddress = "http://" + ip + ":" + PortNumber.ToString();
                //string TcpBaseAddress = "net.tcp://" + ip + ":" + PortNumber.ToString();
                if (ipcHttpState)
                {
                    baseAddress = "http://" + ip + ":" + PortNumber.ToString();
                    turnOnPortInFireWall(PortNumber);
                }
                else
                {
                    baseAddress = "net.tcp://" + ip + ":" + PortNumber.ToString();
                }

                                
                svcHost = new ServiceHost(serviceType, new Uri[] { new Uri(baseAddress)});

                //Enable metadata exchange
                ServiceMetadataBehavior smb = svcHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
                if (smb == null)
                    smb = new ServiceMetadataBehavior();

                if (ipcHttpState)
                {
                    smb.HttpGetEnabled = true;
                    smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                }

                svcHost.Description.Behaviors.Add(smb);
                svcHost.Description.Behaviors.Remove(
                            typeof(ServiceDebugBehavior));
                svcHost.Description.Behaviors.Add(
                            new ServiceDebugBehavior { IncludeExceptionDetailInFaults = true });

                //The URI specified for the endpoint address can be a fully-qualified path or a path that is relative to the service's base address. 
                //If you do not specify a relative address, the service uses the base address. 
                if (ipcHttpState)
                {
                    //WSDualHttpBinding httpBinding = new WSDualHttpBinding();
                    //httpBinding.Security.Mode = WSDualHttpSecurityMode.None;
                    NetHttpBinding httpBinding = new NetHttpBinding();
                    httpBinding.Security.Mode = BasicHttpSecurityMode.None;

                    httpEndpoint = svcHost.AddServiceEndpoint(typeof(IHololensService), httpBinding,
                                                              serviceAddress);
                    //Add MEX endpoint
                    svcHost.AddServiceEndpoint(
                      ServiceMetadataBehavior.MexContractName,
                      MetadataExchangeBindings.CreateMexHttpBinding(),
                      "mex"
                    );
                }
                else
                {
                    NetTcpBinding tcpBinding = new NetTcpBinding();
                    tcpBinding.Security.Mode = SecurityMode.None; 
                    tcpEndpoint = svcHost.AddServiceEndpoint(typeof(IHololensService), tcpBinding,
                                                             serviceAddress);
                    //Add MEX endpoint
                    svcHost.AddServiceEndpoint(
                        ServiceMetadataBehavior.MexContractName,
                        MetadataExchangeBindings.CreateMexTcpBinding(),
                        "mex"
                    );
                }

                svcHost.Open();
                
                Console.WriteLine("\n\nService is Running at following address\n");
                
                if (ipcHttpState)
                {
                    Console.WriteLine("{0} ({1})\n", httpEndpoint.Address.ToString(), httpEndpoint.Binding.Name);
                }
                else
                {
                    Console.WriteLine("{0} ({1})\n", tcpEndpoint.Address.ToString(), tcpEndpoint.Binding.Name);
                }         
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
                Console.WriteLine("It is closeing the ServiceHost...");
                svcHost.Close();
                svcHost = null;
                if (ipcHttpState)
                {
                    turnOffPortInFireWall();
                }                
            }
        }
    }
}
