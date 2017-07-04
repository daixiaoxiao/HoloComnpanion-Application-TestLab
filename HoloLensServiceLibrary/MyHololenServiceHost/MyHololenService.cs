using HoloLensServiceLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHololenServiceHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MyHololenService" in both code and config file together.
    public class MyHololenService : IMyHololenService
    {
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

        public List<Product> GetProductList()
        {
            Console.WriteLine("The client is calling the GetProductList Function!");
            return Products.Instance.ProductList;
        }

        public Dictionary<string, string[]> GetObjectFile(string fileName)
        {
//            ObjectBuilder objClass = ObjectBuilder.DefaultInstance;

            Console.WriteLine("The client is calling the GetObjectFile Function!");
            string folderName = @"c:\temp\root\";
            string FilePath = Path.Combine(folderName, fileName);
            return File.ReadAllBytes(FilePath);
        }
    }
}
