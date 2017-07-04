using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace HoloLensServiceLibrary
{
    [ServiceContract]
    public interface IMyHololenService
    {
        [OperationContract]
        Stream GetGeometryFile(string fileName, string fileExtension);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        List<Product> GetProductList();

        [OperationContract]
        Dictionary<string, string[]> GetObjectFile(string fileName);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "HoloLensServiceLibrary.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        public CompositeType(bool v1, string v2)
        {
            BoolValue = v1;
            StringValue = v2;
        }

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
