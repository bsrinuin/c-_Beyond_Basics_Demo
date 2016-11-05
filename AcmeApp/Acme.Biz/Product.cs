using Acme.Common;
using System;
using static Acme.Common.LoggingService;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// This is to crate a public class
    /// </summary>
    public class Product
    {

        public const double InchesPerMeter = 39.37;
        public readonly decimal minimumPrice;

        public decimal cost { get; set; }

        //public decimal CalculateSuggestedPrice(decimal markupPercent)
        //{
        //    return this.cost + (this.cost * markupPercent / 100);
        //}
        public decimal CalculateSuggestedPrice(decimal markupPercent)=>
        this.cost + (this.cost * markupPercent / 100);
        
        public Product()
        {
            Console.WriteLine("This is construstor1");

            //Initialize the vendor

            //this.productVendor = new Vendor();

            minimumPrice = .99m;

            this.Category = "Tools";
        }

        private DateTime? availabilityDate;

        public DateTime? AvailabilityDate
        {
            get { return availabilityDate; }
            set { availabilityDate = value; }
        }


        public Product(int propertyid, String productname, string description ):this()  //this() will Invoke the default constructor
        {
            this.PropertyId = propertyid;
            this.ProductName = productname;
            this.Description = description;

            this.minimumPrice = 9.99m;
            Console.WriteLine("Product Instance has a name:" + ProductName);
        }
        private String productName; //Backing Field names should be camel case-(Propful tab tab to get the fields and properties)
       

        public String ProductName //Property names should be Pascal case
        {
            get
            {
                var formattedValue = productName?.Trim();
                return formattedValue;
            }
            set
            {
                //public string validationmessage;
                if (value.Length < 3)
                {
                    validationmessage = "Product name must be greaterthen 3 characters";
                }
                else
                     if (value.Length > 25)
                {
                    validationmessage = "Product name must be below 25 characters";
                }
                else
                {
                    productName = value;
                }
            }
        }


        private String description;

        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        private int propertyId;

        public int PropertyId
        {
            get { return propertyId; }
            set { propertyId = value; }
        }


        private Vendor productVendor;

        public Vendor ProductVendor
        {
            get {

                //LAZY loading , need based 
                if(productVendor == null)
                {
                    productVendor = new Vendor();
                }

                return productVendor; }
            set { productVendor = value; }
        }

        public string validationmessage { get; private set; }

        internal string Category { get; set; }
        public int sequencenumber { get; set; } = 1; //C# 6 

        //public string productCode => this.Category + "-" + this.sequencenumber;

       // public string productCode => string.Format("{0}-{1:0000}", this.Category, this.sequencenumber);
        public string productCode => $"{this.Category}-{this.sequencenumber:0000}";  //String interpolation replaces above code -C# 6

        public string SayHelloTest()
        {
            var vendor = new Vendor();
            vendor.SendWelcomeEmail("welcome to the email");

            var emailservice = new EmailService();

            
            emailservice.SendMessage("test message",this.productName,"test@email.com");

            //c# 6 , we can import statis classes, using static Acme.Common.LoggingService;
            var result = LoggingService.LogAction("Say hello");

            return "This is a test for PropertyID " + PropertyId.ToString()+"Product Avaialble on" +AvailabilityDate?.ToShortDateString();
        }

        public override string ToString()
        {
            return this.ProductName + "(" + this.PropertyId + ")";
        }
    }
}
