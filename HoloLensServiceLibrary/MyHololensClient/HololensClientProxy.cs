using HoloLensServiceLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;

namespace MyHololensClient
{
    public interface IHololenServiceChannel : IHololensService, IClientChannel
    {
    }

    // Define class that implements the callback interface of duplex 
    // contract.
    public class HololensCallbackHandler : IHololensServiceCallback
    {
        public void Equation(string equation)
        {
            Console.WriteLine("Server is calling function Equation() at client side.");
            Console.WriteLine("Equation({0})", equation);
        }

        public void Equals(double result)
        {
            Console.WriteLine("Server is calling function Equals() at client side.");
            Console.WriteLine("Equals({0})", result);
        }
    }

    public partial class HololensClientProxy : DuplexClientBase<IHololensService>, IHololensService
    {
        public HololensClientProxy(InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }

        public HololensClientProxy(InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }

        public HololensClientProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }

        public HololensClientProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }

        public HololensClientProxy(InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }

        public void AddTo(double n)
        {
            base.Channel.AddTo(n);
        }

        public void Clear()
        {
            base.Channel.Clear();
        }

        public string GetData(int value)
        {
            return base.Channel.GetData(value);
        }

        public Task<string> GetData_Async(int value)
        {
            return base.Channel.GetData_Async(value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            return base.Channel.GetDataUsingDataContract(composite);
        }

        public Task<CompositeType> GetDataUsingDataContract_Async(CompositeType composite)
        {
            return base.Channel.GetDataUsingDataContract_Async(composite);
        }

        public string[] GetGeometryComponentNames()
        {
            return base.Channel.GetGeometryComponentNames();
        }

        public Stream GetGeometryFile(string fileName, string fileExtension)
        {
            return base.Channel.GetGeometryFile(fileName, fileExtension);
        }

        public Dictionary<string, string[]> GetGeometryObject()
        {
            return base.Channel.GetGeometryObject();
        }

        public Dictionary<string, string[]> GetGeometryObjectByName(string componentName)
        {
            return base.Channel.GetGeometryObjectByName(componentName);
        }

        public List<Product> GetProductList()
        {
            return base.Channel.GetProductList();
        }
    }
}
