using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 10/27 : completed creating good properties
/// 
/// </summary>
namespace Acme.Biz.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void SayHelloTest()
        {
            //Arrange
            Product p = new Product();
            string expected = "This is a test for PropertyID 1Product Avaialble on";
            p.PropertyId = 1;
            p.ProductName = "Apple";
            p.Description = "This is a test desc";
            p.ProductVendor.CompanyName = "ABC";

            //Act

            var actual = p.SayHelloTest();

            //Assert
            Assert.AreEqual(actual, expected);


            //Assert.Fail();
        }

        [TestMethod()]
        public void SayHello_ParametorizedCtorTest()
        {
            //Arrange
            Product p = new Product(1, "Apple", "This is a test desc");
            //string expected = "This is a test for PropertyID 1 ";
            string expected = "This is a test for PropertyID 1Product Avaialble on";

            //p.PropertyId = 1;
            //p.ProductName = "Apple";
            //p.Description = "This is a test desc";


            //Act

            var actual = p.SayHelloTest();

            //Assert
            Assert.AreEqual(actual, expected);


            //Assert.Fail();
        }


        [TestMethod()]
        public void SayHello_ObjectInitializerTest()
        {
            //Arrange
            Product currentProduct = new Product()
            {
                PropertyId = 1,
                ProductName = "Apple",
                Description = "This is a test desc"
            };

            // string expected = "This is a test for PropertyID 1";
            string expected = "This is a test for PropertyID 1Product Avaialble on";

            //Act

            var actual = currentProduct.SayHelloTest();

            //Assert
            Assert.AreEqual(actual, expected);


            //Assert.Fail();
        }


        [TestMethod()]
        public void Product_NullTest()
        {
            //Arrange
            Product currentProduct = null;
            //C# 6 null checking
            var companyName = currentProduct?.ProductVendor?.CompanyName;
            string expected = null;
            //Act

            string actual = companyName;
            // var actual = currentProduct.SayHelloTest();


            //Assert
            Assert.AreEqual(actual, expected);

            //Assert.Fail();
        }

        [TestMethod()]
        public void ConverMeterstoInches_Test()
        {
            //Arrange
            double expected = Product.InchesPerMeter * 2;

            //Act
            double actual = 78.74;

            //Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void ReadonlyParameters_Test()
        {
            //Arrange
            var product = new Product();
            decimal expected = .99m;

            //Act
            decimal actual = product.minimumPrice;

            //Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void ReadonlyParametersBULK_Test()
        {
            //Arrange
            var product = new Product(1, "Apple", "Apple Iphone");
            decimal expected = 9.99m;

            //Act
            decimal actual = product.minimumPrice;

            //Assert
            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void ProductNameformat_Test()
        {
            //Arrange
            var product = new Product();
            product.ProductName = "Apple  ";

            string actual = product.ProductName;

            //act
            string expected = "Apple";

            //Assert

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void ProductName_Toolong_Test()
        {
            //Arrange
            var product = new Product();
            product.ProductName = "Apple united states of America, hawai, maui and Guam ";

            string expected = null;
            string expectedmessage = "Product name must be below 25 characters";


            //act
            string actual = product.ProductName;
            string actualmessage = product.validationmessage;


            //Assert

            Assert.AreEqual(actual, expected);
            Assert.AreEqual(actualmessage, expectedmessage);
        }


        [TestMethod]
        public void ProductName_Short_Test()
        {
            //Arrange
            var product = new Product();
            product.ProductName = "Ap";

            string expected = null;
            string expectedmessage = "Product name must be greaterthen 3 characters";


            //act
            string actual = product.ProductName;
            string actualmessage = product.validationmessage;

            //Assert
            Assert.AreEqual(actual, expected);
            Assert.AreEqual(actualmessage, expectedmessage);
        }

        [TestMethod]
        public void ProductName_CorrectLength_Test()
        {
            //Arrange
            var product = new Product();
            product.ProductName = "Apple ";

            string expected = "Apple";
            string expectedmessage = null;

            //act
            string actual = product.ProductName;
            string actualmessage = product.validationmessage;

            //Assert

            Assert.AreEqual(actual, expected);
            Assert.AreEqual(actualmessage, expectedmessage);
        }


        [TestMethod]
        public void Sequencenumber_default_Test()
        {
            //Arrange
            var product = new Product();
            // product.sequencenumber =1;

            int expected = 1;

            //act
            int actual = product.sequencenumber;

            //Assert

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Sequencenumber_anyvalue_Test()
        {
            //Arrange
            var product = new Product();
            product.sequencenumber = 5;

            int expected = 5;

            //act
            int actual = product.sequencenumber;

            //Assert

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void category_default_Test()
        {
            //Arrange
            var product = new Product();
            // product.Category = "hardware";

            string expected = "Tools";

            //act
            string actual = product.Category;

            //Assert

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void category_anyvalue_Test()
        {
            //Arrange
            var product = new Product();
            product.Category = "hardware";

            string expected = "hardware";

            //act
            string actual = product.Category;

            //Assert

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void ProductCode_Defalult_Test()
        {
            //Arrange
            var product = new Product();
            // product.Category = "hardware";

            string expected = "Tools-0001";

            //act
            string actual = product.productCode;

            //Assert

            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void CalculateSuggestedPriceSayHelloTest()
        {
            //Arrange
            var currentProduct = new Product(1, "Apple", "Apple Inc.");
            currentProduct.cost = 50m;
            var expected = 55m;

            //Act
            var actual = currentProduct.CalculateSuggestedPrice(10m);

            //Assert
            Assert.AreEqual(actual, expected); 


        }
    }
}