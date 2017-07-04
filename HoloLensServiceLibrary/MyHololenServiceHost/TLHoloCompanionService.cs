using HoloLensServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HololensServiceHost
{
    //The TLHoloCompanionService will connect to Test.Lab automation and provide the real life values.
    class TLHoloCompanionService : IHololensService
    {
        public string GetData(int value)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetData_Async(int value)
        {
            throw new NotImplementedException();
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            throw new NotImplementedException();
        }

        public Task<CompositeType> GetDataUsingDataContract_Async(CompositeType composite)
        {
            throw new NotImplementedException();
        }

        public Stream GetGeometryFile(string fileName, string fileExtension)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductList()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string[]> GetGeometryObjectByName(string componentName)
        {
            Console.WriteLine("The client is calling the GetGeometryObject_byComponentName Function!");
            Dictionary<string, string[]> component = new Dictionary<string, string[]>();
            //ObjectBuilder objClass = ObjectBuilder.DefaultInstance;
            //component = objClass.GenerateObjectDictionary(componentName);
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
            throw new NotImplementedException();
        }

        public void AddTo(double n)
        {
            throw new NotImplementedException();
        }
    }
}
