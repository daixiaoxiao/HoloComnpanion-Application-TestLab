using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using HoloLensServiceLibrary;
using NetFwTypeLib;

namespace MyHololensClient
{
    class ClientProgram
    {
        static void Main(string[] args)
        {
            InstanceContext site = new InstanceContext(new HololensCallbackHandler());

            //WSDualHttpBinding httpBinding = new WSDualHttpBinding();
            ////Add the ip address of client
            //httpBinding.ClientBaseAddress = new Uri("http://10.2.9.160:9009/");
            //httpBinding.MaxReceivedMessageSize = int.MaxValue;
            //httpBinding.Security.Mode = WSDualHttpSecurityMode.None;

            NetHttpBinding httpBinding = new NetHttpBinding();
            httpBinding.MaxReceivedMessageSize = int.MaxValue;
            httpBinding.Security.Mode = BasicHttpSecurityMode.None;

            //Add the ip address of server
            string serverEndptString = "10.2.9.160:9002/TestHoloCompanionsService";
            EndpointAddress httpEndptadr = new EndpointAddress("http://" + serverEndptString);

            NetTcpBinding tcpBinding = new NetTcpBinding();
            tcpBinding.Security.Mode = SecurityMode.None;
            tcpBinding.MaxReceivedMessageSize = int.MaxValue;
            EndpointAddress tcpEndptadr = new EndpointAddress("net.tcp://" + serverEndptString);

            //Any way i can automatically find the ip address and ServiceSource?
            //HololensClientProxy myClient = new HololensClientProxy(site, tcpBinding, tcpEndptadr);
            HololensClientProxy myClient = new HololensClientProxy(site, httpBinding, httpEndptadr);
            try
            {
                CompositeType composite = new CompositeType(true, "Using CompositeType");

                Task.Run(async () =>
                                    {
                                        //Do any async anything you need here without worry
                                        Console.WriteLine(await myClient.GetData_Async(1314));
                                        Console.WriteLine((await myClient.GetDataUsingDataContract_Async(composite)).StringValue);
                                    }).GetAwaiter().GetResult();
                Console.WriteLine(myClient.GetData(1314));

                // Call the AddTo service operation.
                double value = 100.00D;
                myClient.AddTo(value);

                myClient.Clear();



                //CompositeType composite = new CompositeType(true, "Using CompositeType");
                //composite = myClient.GetDataUsingDataContract(composite);
                //Console.WriteLine(composite.StringValue);

                //List<Product> products = myClient.GetProductList();
                //foreach (Product p in products)
                //{
                //    Console.WriteLine("productID " + p.ProductId + " ProductName " + p.Name + " Price " + p.Price + " Belong to Category " + p.CategoryName);
                //}

                //string[] componentNames = myClient.GetGeometryComponentNames();
                //Dictionary<string, string[]> temp = new Dictionary<string, string[]>();
                //temp = myClient.GetGeometryObject();

                //var component_temp = new Dictionary<string, string[]>();
                //component_temp = myClient.GetGeometryObjectByName("tailplane");

                Console.ReadLine();
            }
            catch (TimeoutException timeProblem)
            {
                Console.WriteLine("The service operation timed out. " + timeProblem.Message);
                myClient.Abort();
                Console.Read();
            }
            catch (CommunicationException commProblem)
            {
                Console.WriteLine("There was a communication problem. " + commProblem.Message);
                myClient.Abort();
                Console.Read();
            }

        }
    }
}
