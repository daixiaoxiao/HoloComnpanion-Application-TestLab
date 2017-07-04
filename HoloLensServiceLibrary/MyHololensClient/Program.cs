using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using HoloLensServiceLibrary;

namespace MyHololensClient
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicHttpBinding basicHttpBing = new BasicHttpBinding();
            basicHttpBing.MaxReceivedMessageSize = int.MaxValue;
            MyHololensClientProxy myClient = new MyHololensClientProxy(basicHttpBing, new EndpointAddress("http://146.122.36.60:9001/MyHololenService"));
            Console.WriteLine(myClient.GetData(1314));

            CompositeType composite = new CompositeType(true, "Using CompositeType");
            composite = myClient.GetDataUsingDataContract(composite);
            Console.WriteLine(composite.StringValue);

            List<Product> products = myClient.GetProductList();
            foreach (Product p in products)
            {
                Console.WriteLine("productID " + p.ProductId + " ProductName " + p.Name + " Price " + p.Price + " Belong to Category " + p.CategoryName);
            }

            Dictionary<string, string[]> temp = new Dictionary<string, string[]>();
            temp = myClient.GetObjectFile("svg.obj");

            Console.ReadLine();        
        }
    }
}
