using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Common;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class VendorTests
    {
        [TestMethod()]
        public void SendWelcomeEmail_ValidCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "ABC Corp";
            var expected = "Message sent: Hello ABC Corp";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_EmptyCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = "";
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SendWelcomeEmail_NullCompany_Success()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.CompanyName = null;
            var expected = "Message sent: Hello";

            // Act
            var actual = vendor.SendWelcomeEmail("Test Message");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PlaceOrderTest()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Apple", "Apple Inc");

            var expected = new OperationResult(true, "Order from Acme, Inc \r\nProduct: Apple\r\nQuantity: 3\r\nInstruction: default");

            //Act 
            var actual = vendor.PlaceOrder(product, 3);

            //Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

        }

        [TestMethod()]
        public void PlaceOrder_3parameter_Test()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Apple", "Apple Inc");

            var expected = new OperationResult(true, "Order from Acme, Inc \r\nProduct: Apple\r\nQuantity: 3" + "\r\nDeliver By: 11/25/2016\r\nInstruction: default");

            //Console.WriteLine(new DateTimeOffset(2016, 11, 25, 0, 0, 0, new TimeSpan(-7, 0, 0)));
            //Act 
            var actual = vendor.PlaceOrder(product, 3, new DateTimeOffset(2016, 11, 25, 0, 0, 0, new TimeSpan(-7, 0, 0)));


            // var actual = vendor.PlaceOrder(product, 3, null);


            //Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

        }


        [TestMethod()]
        public void PlaceOrder_NoDeliverydate_Test()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Apple", "Apple Inc");

            var expected = new OperationResult(true, "Order from Acme, Inc \r\nProduct: Apple\r\nQuantity: 3" + "\r\nInstruction: Test Instructions");

            //Act 
            var actual = vendor.PlaceOrder(product, 3, instructions: "Test Instructions");

            //Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

        }



        [TestMethod()]
        public void PlaceOrder_3parameterNULL_Test()
        {
            //Arrange
            var vendor = new Vendor();
            var product = new Product(1, "Apple", "Apple Inc");

            var expected = new OperationResult(true, "Order from Acme, Inc \r\nProduct: Apple\r\nQuantity: 3\r\nInstruction: default");

            //Console.WriteLine(new DateTimeOffset(2016, 11, 25, 0, 0, 0, new TimeSpan(-7, 0, 0)));
            //Act 
            var actual = vendor.PlaceOrder(product, 3, null);

            // var actual = vendor.PlaceOrder(product, 3, null);

            //Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PlaceOrderProductnullExceptionTest()
        {
            //Arrange
            var vendor = new Vendor();

            //Act 
            var actual = vendor.PlaceOrder(null, 3);

            //Assert
            //  Assert.AreEqual(expected, actual);
        }

        [TestMethod()]

        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void PlaceOrderOrderquantityExceptionTest()
        {
            //Arrange 
            var vendor = new Vendor();
            var product = new Product(1, "Apple", "");

            //Act 
            var actual = vendor.PlaceOrder(product, 0);

            //Assert
            //  Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PlaceOrder_WithAddress_Test()
        {
            //Arrange 
            var vendor = new Vendor();
            var product = new Product(1, "Apple", "");

            var expected = new OperationResult(true, "Test with address");

            //Act - named argumets  -Emum Parameter
            var actual = vendor.PlaceOrder(product, 3,
                                               Vendor.IncludeAddress.yes, //Enumerated Parameter
                                               Vendor.SendCopy.no);  //Enumerated Parameter

            //Assert
            Assert.AreEqual(expected.Success, actual.Success);
            Assert.AreEqual(expected.Message, actual.Message);

        }

        [TestMethod()]
        public void ToString_Test()
        {
            // Arrange
            var vendor = new Vendor();
            vendor.VendorId = 1;
            vendor.CompanyName = "ABC Corp";
            var expected = "Vendor: ABC Corp";

            // Act
            var actual = vendor.ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void PrepareDirections_Test()
        {
            // Arrange
            var vendor = new Vendor();
            var expected = @"Insert \r\n to define a new line";

            // Act
            var actual = vendor.PrepareDirections();
            Console.WriteLine(actual);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        //[TestMethod()]
        //public void PlaceOrder_WithSendCopy_Test()
        //{
        //    //Arrange 
        //    var vendor = new Vendor();
        //    var product = new Product(1, "Apple", "");

        //    var expected = new OperationResult(true, "Test with copy");

        //    //Act 
        //    var actual = vendor.PlaceOrder(product, 3, includeAddress: false,sendCopy: true);

        //    //Assert
        //    Assert.AreEqual(expected.Success, actual.Success);
        //    Assert.AreEqual(expected.Message, actual.Message);

        //}

        //[TestMethod()]
        //public void PlaceOrder_WithAddressAndCopy_Test()
        //{
        //    //Arrange 
        //    var vendor = new Vendor();
        //    var product = new Product(1, "Apple", "");

        //    var expected = new OperationResult(true, "Test with address with copy");

        //    //Act 
        //    var actual = vendor.PlaceOrder(product, 3, includeAddress: true, sendCopy: true);

        //    //Assert
        //    Assert.AreEqual(expected.Success, actual.Success);
        //    Assert.AreEqual(expected.Message, actual.Message);

        //}
    }
}