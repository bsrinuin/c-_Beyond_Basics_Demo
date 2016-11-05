using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages the vendors from whom we purchase our inventory.
    /// </summary>
    public class Vendor 
    {
        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }

        public enum IncludeAddress { yes, no };
        public enum SendCopy { yes, no };



       /// <summary>
       /// 
       /// </summary>
       /// <param name="product">Product to Order</param>
       /// <param name="quantity">Quantity of the product</param>
       /// <param name="includeAddress">True to include shipping address</param>
       /// <param name="sendCopy">True to send copy of email</param>
       /// <returns>Success flag to order Text</returns>
       public OperationResult PlaceOrder (Product product,int quantity, IncludeAddress includeAddress,SendCopy sendCopy )
        {
            var orderText = "Test";
            if (includeAddress == IncludeAddress.yes) orderText += " with address";
            if (sendCopy == SendCopy.yes ) orderText += " with copy";

            var operationResult = new OperationResult(true, orderText);

            return operationResult;

        }

        //public OperationResult PlaceOrder(Product product, int quantity)
        //{
        //   return  PlaceOrder(product,quantity,null,null);
        //}

        //    public OperationResult PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy)
        //{
        //   return  PlaceOrder(product,quantity,deliverBy,null);
        //}

        /// <summary>
        /// Method overloading technique of PlaceOrder
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        /// <param name="deliverBy"></param>
        ///<param name="instructions"></param>
        /// <returns>Operation Result class </returns>
        public OperationResult PlaceOrder(Product product, int quantity, 
                                            DateTimeOffset? deliverBy = null,  //by defining default parameter, we can avoid overloads
                                            string instructions="default")
        {
            if (product == null)
                //throw new ArgumentNullException("Product");  //Old C#
                throw new ArgumentNullException(nameof(product)); //nameof() is C# 6.0

            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            if (deliverBy <= DateTimeOffset.Now)
                throw new ArgumentOutOfRangeException(nameof(deliverBy));

            var success = false;

            var orderTextBuilder = new StringBuilder("Order from Acme, Inc " + System.Environment.NewLine +
                            "Product: " + product.ProductName +
                                    System.Environment.NewLine +
                                    "Quantity: " + quantity);

            if (deliverBy.HasValue)
            {
                orderTextBuilder.Append( System.Environment.NewLine +
                            "Deliver By: " + deliverBy.Value.ToString("d"));
            }

            if(!String.IsNullOrWhiteSpace(instructions))
            {
                orderTextBuilder.Append(System.Environment.NewLine +
                    "Instruction: " + instructions); 
            }

            var orderText = orderTextBuilder.ToString();

            var emailService = new EmailService();
            var confirmation = emailService.SendMessage("new order", orderText, this.Email);

            if (confirmation.StartsWith("Message sent: "))
            {
                success = true;
            }

            var operationResult = new OperationResult(success, orderText);

            // operationResult.Success = true;

            return operationResult;
        }



        /// <summary>
        /// Sends an email to welcome a new vendor.
        /// </summary>
        /// <returns></returns>
        public string SendWelcomeEmail(string message)
        {
            var emailService = new EmailService();
            var subject = ("Hello " + this.CompanyName).Trim();
            var confirmation = emailService.SendMessage(subject,
                                                        message, 
                                                        this.Email);
            return confirmation;
        }

        public override string ToString()
        {
            string vendorInfo = "Vendor: " + this.CompanyName;
            //string vendorInfo = null;


            //if (!String.IsNullOrWhiteSpace(vendorInfo))
            //{
                string result;
                result = vendorInfo?.ToLower();
                result = vendorInfo?.ToUpper();
                result = vendorInfo?.Replace("Vendor", "Supplier");

                var length = result.Length;
                var index = vendorInfo?.IndexOf(":");
                var begin = vendorInfo?.StartsWith("Vendor");
            //}

            return vendorInfo;

        }

        public string PrepareDirections()
        {
            var directions = @"Insert \r\n to define a new line";  //verbatim string
            return directions; 
        }

        public string PrepareDirectionsOnTwoLines()
        {
            var directions = "First do this" + Environment.NewLine
                                + "then do that";
            return directions;
        }
    }
}
