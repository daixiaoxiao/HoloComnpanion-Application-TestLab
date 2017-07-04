using HoloLensServiceLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HololensServiceHost
{
    //The TestHoloCompaninoService is the service class used for Testing.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class TestHoloCompanionsService : IHololensService
    {
        double result = 0.0D;
        string equation = null;
        IHololensServiceCallback callback = null;

        public TestHoloCompanionsService()
        {
            callback = OperationContext.Current.GetCallbackChannel<IHololensServiceCallback>();
        }

        public Stream GetGeometryFile(string fileName, string fileExtension)
        {
            string folderName = @"c:\temp\root\";
            string downloadFilePath = Path.Combine(folderName, fileName + "." + fileExtension);

            Debug.WriteLine("Path to my file: {0}\n", downloadFilePath);
            //Write logic to create the file
            using (FileStream fs = File.Create(downloadFilePath))
            {
                byte[] info = new UTF8Encoding(true).GetBytes("There is some object file attributes in the file.");
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }

            using (StreamReader sr = File.OpenText(downloadFilePath))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
            return File.OpenRead(downloadFilePath);
        }

        public string GetData(int value)
        {
            Console.WriteLine("The client is calling the GetData Function!");
            return string.Format("You entered: {0}", value);
        }

        public async Task<string> GetData_Async(int value)
        {
            Console.WriteLine("The client is calling the GetData_Async Function!");
            return await Task.Factory.StartNew(() => "You Entered " + value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            Console.WriteLine("The client is calling the GetDataUsingDataContract Function!");
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public async Task<CompositeType> GetDataUsingDataContract_Async(CompositeType composite)
        {
            Console.WriteLine("The client is calling the GetDataUsingDataContract_Async Function!");
            return await Task.Factory.StartNew(() => {
                                                        if (composite == null)
                                                        {
                                                            throw new ArgumentNullException("composite");
                                                        }
                                                        if (composite.BoolValue)
                                                        {
                                                            composite.StringValue += "Suffix";
                                                        }
                                                        return composite;
                                                    }
                                               );
        }

        public List<Product> GetProductList()
        {
            Console.WriteLine("The client is calling the GetProductList Function!");
            return Products.Instance.ProductList;
        }

        public Dictionary<string, string[]> GetGeometryObjectByName(string componentName)
        {
            Console.WriteLine("The client is calling the GetGeometryObject_byComponentName Function!");
            Dictionary<string, string[]> component = new Dictionary<string, string[]>();
            //ObjectBuilder objClass = ObjectBuilder.DefaultInstance;
            //component = objClass.GenerateObjectDictionary(componentName);
            //string folderName = @"c:\temp\root\";
            //string FilePath = Path.Combine(folderName, fileName);
            //return File.ReadAllBytes(FilePath);
            return component;
        }

        public Dictionary<string, string[]> GetGeometryObject()
        {
            Console.WriteLine("The client is calling the GetGeometryObject Function!");
            Dictionary<string, string[]> components = new Dictionary<string, string[]>();
            //ObjectBuilder objClass = ObjectBuilder.DefaultInstance;
            //components = objClass.GenerateObjectDictionary();
            return components;
        }

        public string[] GetGeometryComponentNames()
        {
            //ObjectBuilder objClass = ObjectBuilder.DefaultInstance;
            //return objClass.GetComponentNames();
            return null;
        }

        public void Clear()
        {
            Console.WriteLine("The Client is calling Clear Function!");
            callback.Equation(equation + " = " + result.ToString());
            result = 0.0D;
            equation = result.ToString();
        }

        public void AddTo(double n)
        {
            Console.WriteLine("The Client is calling AddTo Function!");
            result += n;
            equation += " + " + n.ToString();
            callback.Equals(result);
        }
    }
}
