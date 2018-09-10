using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml.Linq;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProductInformationService" in code, svc and config file together.
public class ProductInformationService : IProductInformationService
{
    public void DoWork()
    {
    }

    public Product GetProducts(string ProductID)
    {
        Product _Product = new Product();
        ProductInfo _PInfo = new ProductInfo();
        try
        {
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "\\Reference\\ProductInfo.Xml");

            IEnumerable<XElement> pdocut = (
                from result in doc.Root.Elements("ProductsInfo")
                where result.Element("ID").Value == ProductID.ToString()
                select result);
            
            _Product.id = pdocut.ElementAt(0).Element("ID").Value;
            _Product.timestamp = pdocut.ElementAt(0).Element("TimeStamp").Value;
            _Product.PInfo = new List<ProductInfo>();
            foreach (var item in pdocut)
            {
                _PInfo._id = Convert.ToInt64(item.Element("Product").Element("Pid").Value);
                _PInfo.name = item.Element("Product").Element("PName").Value;
                _PInfo.quantity = Convert.ToInt16(item.Element("Product").Element("quantity").Value);
                _PInfo.Sale_Amount = Convert.ToDouble(item.Element("Product").Element("sale_amount").Value);
                _Product.PInfo.Add(_PInfo);
            }
        }
        catch (Exception ex)
        {
            throw new FaultException<string>
                 (ex.Message);
        }
        return _Product;
    }

    public bool PutProductInformation(Product PInfo)
    {
        try
        {
            XDocument doc = XDocument.Load(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "\\Reference\\ProductInfo.Xml");

            var PID = doc.Root.Elements("ProductsInfo")
                .Where(a=> (string)a.Element("ID").Value==PInfo.id)
                .Select(a=>(string)a.Element("ID").Value).FirstOrDefault();

            if (!string.IsNullOrEmpty(PID))
                throw new Exception("Duplicate Product ID");

            doc.Element("DocumentElement").Add(
                    new XElement("ProductsInfo",
                    new XElement("ID", PInfo.id ),
                    new XElement("TimeStamp", PInfo.timestamp),
                    from PdInfo in PInfo.PInfo
                    select
                        new XElement("Product",
                        new XElement("Pid", PdInfo._id),
                        new XElement("PName", PdInfo.name),
                        new XElement("quantity", PdInfo.quantity),
                        new XElement("sale_amount", PdInfo.Sale_Amount))
                    ));

            doc.Save(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "\\Reference\\ProductInfo.Xml");
            return true;
        }
        catch
        {
            return false;
        }
    }
}
