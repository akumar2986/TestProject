using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace TEST.DAL.Entities
{
    [XmlInclude(typeof(ProductType))]
    [Serializable, XmlRoot(ElementName = "ProductType")]
    public class ProductType
    {
        [XmlAttribute(AttributeName = "ProductTypeName")]
        public string ProductTypeName { get; set; }
    }


    [Serializable, XmlRoot(ElementName = "Customer")]
    public class Customer
    {       

        [XmlAttribute(AttributeName = "CustomerId")]
        public int CustomerId { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        
        [XmlAttribute(AttributeName = "Email")]
        public string Email { get; set; }

        [XmlAttribute(AttributeName = "Phone")]
        public string Phone { get; set; }
        
        [XmlAttribute(AttributeName = "CustomerType")]
        public string CustomerType { get; set; }

        [XmlElement(ElementName = "ProductTypes")]
        public List<ProductType> ProductTypes { get; set; }
    }


    [Serializable, XmlRoot(ElementName = "root")]
    public class Root
    {
        [XmlElement(ElementName = "Customers")]
        public List<Customer> Customer { get; set; }
    }

}
