using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
namespace WCF_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private static void GetOrderDetails(string orderID)
        {
            WebClient proxy = new WebClient();
            string serviceURL = string.Format("http://localhost:61090/OrderService.svc/GetOrderDetails/{0}", orderID);
            byte[] data = proxy.DownloadData(serviceURL);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(OrderContract));
            OrderContract order = obj.ReadObject(stream) as OrderContract;
            Console.WriteLine("Order ID : " + order.OrderID);
            Console.WriteLine("Order Date : " + order.OrderDate);
            Console.WriteLine("Order Shipped Date : " + order.ShippedDate);
            Console.WriteLine("Order Ship Country : " + order.ShipCountry);
            Console.WriteLine("Order Total : " + order.OrderTotal);
        }
    }
}
