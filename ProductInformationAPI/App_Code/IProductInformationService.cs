using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProductInformationService" in both code and config file together.
[ServiceContract]
public interface IProductInformationService
{
    [OperationContract]
    void DoWork();

    [OperationContract]
    [WebInvoke(UriTemplate = "/PutProductInformation",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json, Method = "POST")]
    bool PutProductInformation(Product pdoct);

    [OperationContract]
    [WebGet(UriTemplate = "/GetProducts/{ProductID}",
            ResponseFormat = WebMessageFormat.Json)]
    Product GetProducts(string ProductID);
}
[DataContract]
public class Product
{
    [DataMember]
    public string id { get; set; }
    [DataMember]
    public string timestamp { get; set; }
    [DataMember]
    public List<ProductInfo> PInfo { get; set; }
}
[DataContract]
public class ProductInfo
{
    [DataMember]
    public Int64 _id { get; set; }
    [DataMember]
    public string name { get; set; }
    [DataMember]
    public int quantity { get; set; }
    [DataMember]
    public double Sale_Amount { get; set; }
}
