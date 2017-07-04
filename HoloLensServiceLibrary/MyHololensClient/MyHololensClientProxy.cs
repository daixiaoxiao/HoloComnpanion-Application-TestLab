using HoloLensServiceLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel;

namespace MyHololensClient
{
    public interface IMyHololenServiceChannel : IMyHololenService, IClientChannel
    {
    }

    public partial class MyHololensClientProxy : ClientBase<IMyHololenService>, IMyHololenService
    {
        public MyHololensClientProxy()
        {
        }

        public MyHololensClientProxy(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }

        public MyHololensClientProxy(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }

        public MyHololensClientProxy(string endpointConfigurationName, EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }

        public MyHololensClientProxy(System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }

        public string GetData(int value)
        {
            return base.Channel.GetData(value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            return base.Channel.GetDataUsingDataContract(composite);
        }

        public Stream GetGeometryFile(string fileName, string fileExtension)
        {
            return base.Channel.GetGeometryFile(fileName, fileExtension);
        }

        public Dictionary<string, string[]> GetObjectFile(string fileName)
        {
            return base.Channel.GetObjectFile(fileName);
        }

        public List<Product> GetProductList()
        {
            return base.Channel.GetProductList();
        }
    }
}
