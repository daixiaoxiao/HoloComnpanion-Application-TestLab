using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;

namespace HoloLensServiceLibrary
{
    [ServiceContract(SessionMode = SessionMode.Required,
                     CallbackContract = typeof(IHololensServiceCallback))]
    public interface IHololensService
    {
        [OperationContract]
        Stream GetGeometryFile(string fileName, string fileExtension);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        Task<string> GetData_Async(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        Task<CompositeType> GetDataUsingDataContract_Async(CompositeType composite);

        [OperationContract]
        List<Product> GetProductList();

        [OperationContract]
        Dictionary<string, string[]> GetGeometryObject();

        [OperationContract]
        Dictionary<string, string[]> GetGeometryObjectByName(string componentName);

        [OperationContract]
        string[] GetGeometryComponentNames();

        [OperationContract(IsOneWay = true)]
        void Clear();

        [OperationContract(IsOneWay = true)]
        void AddTo(double n);
    }

    public interface IHololensServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void Equals(double result);

        [OperationContract(IsOneWay = true)]
        void Equation(string eqn);
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
