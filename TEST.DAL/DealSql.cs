using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using TEST.DAL.Entities;
using TEST.DAL.Serializers;

namespace TEST.DAL
{
    public class TESTSql
    {
        string xmlInputData = "";
        public TESTSql(string filename)
        {
            this.xmlInputData = filename;
        }
//        string xmlInputData = HttpContext.Current.Server.MapPath("~/App_Data/data.xml");//Path of the xml script 
 

        public List<Customer> GetCustomerList()
        {
            XmlSerializer serializer = new XmlSerializer();            
            DataSet ds = new DataSet();//Using dataset to read xml file  
            ds.ReadXml(xmlInputData);

            //this deserialization getting error that why access through dataset
            // Root data = serializer.Deserialize<Root>(xmlInputData);
            // return data.Customer;

            List<Customer> customers = new List<Customer>();

            customers = (from DataRow dr in ds.Tables[0].Rows
                         select new Customer()
                           {
                               CustomerId = Convert.ToInt32(dr["CustomerId"]),
                               Name = dr["Name"].ToString(),
                               Email = dr["Email"].ToString(),
                               Phone = dr["Phone"].ToString()
                           }).ToList();  
            //IList<CustomerModel> customers = DataTableHelper.ConvertToList<CustomerModel>(ds.Tables[0]); 
            return customers;
        }

        public Customer GetCustomerById(int Id)
        {
            var list = GetCustomerList();
            Customer customer = list.FirstOrDefault(i => i.CustomerId == Id);
            return customer;

            //XElement root = XElement.Load(xmlInputData);
            //IEnumerable<XElement> customerlist =
            //    from el in root.Elements("Customer")
            //    where (string)el.Attribute("CustomerId") == Convert.ToString(Id)
            //    select el;

            
        
            //XmlSerializer serializer = new XmlSerializer();
            //List<Customer> data = serializer.Deserialize<Customer>(customerlist.FirstOrDefault().ToString());
            //return data.FirstOrDefault();
        }

        public Customer saveCustomer(Customer customer)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlInputData);
                XmlElement custelement = xmlDoc.CreateElement("Customer");
                Random random = new Random();
                customer.CustomerId = random.Next();
                custelement.SetAttribute("CustomerId", customer.CustomerId.ToString());
                custelement.SetAttribute("Name", customer.Name);
                custelement.SetAttribute("Email", customer.Email);
                custelement.SetAttribute("Phone", customer.Phone);
                custelement.SetAttribute("CustomerType", customer.CustomerType);

                
                foreach (var prodtype in customer.ProductTypes)
                {
                    XmlElement ptelement = xmlDoc.CreateElement("ProductType");
                    ptelement.SetAttribute("ProductTypeName", prodtype.ProductTypeName);
                    custelement.AppendChild(ptelement);
                }
                
                xmlDoc.DocumentElement.AppendChild(custelement.Clone());
                xmlDoc.Save(xmlInputData);
                return customer;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }
}

