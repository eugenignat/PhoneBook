using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Domain.Repository;
using Domain.Interfaces;
using Domain;
using Moq;
using Domain.Service;
using FluentAssertions;
using System.Linq;

namespace UnitTest.ServiceTests
{
    /// <summary>
    /// Summary description for CustomerServiceTests
    /// </summary>
    [TestClass]
    public class CustomerServiceTests
    {
        //private ApiContext _context
        private ICustomerService _service;

        public CustomerServiceTests()
        {
            //var options = new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase().Options;
            //_context = new ApiContext(options);

            //SetUpData(_context);
            //_service = new CustomerService(new CustomerRepository(_context));
        }

        private void SetUpData(ApiContext context)
        {
            var customer1 = new CustomerModel
            {
                PhoneNumbers = new List<PhoneNumberModel>
                    {
                        new PhoneNumberModel
                        {
                            PhoneNumber = "07555555555",
                            Active = false
                        }
                    }
            };

            context.Customers.Add(customer1);

            var customer2 = new CustomerModel
            {
                PhoneNumbers = new List<PhoneNumberModel>
                    {
                        new PhoneNumberModel
                        {
                            PhoneNumber = "0711111111",
                            Active = true
                        },

                        new PhoneNumberModel
                        {
                            PhoneNumber = "0722222222",
                            Active = true
                        }
                    }
            };

            context.Customers.Add(customer2);

            context.SaveChanges();
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        //private ApiContext GetSetUpContext()
        //{
        //    var options = new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase().Options;
        //    ApiContext context = new ApiContext(options);

        //    SetUpData(context);
        //    return context;
        //}

        [TestMethod]
        public void Given_User_Wants_All_Numbers_Then_Return_All_Numbers()
        {
            using (var ctx = new ApiContext(new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase(databaseName: "all_numbers").Options))
            {
                //arrange
                SetUpData(ctx);
                var customers = CreateMultipleCustomerList();
                _service = new CustomerService(new CustomerRepository(ctx));

                //act
                var result = _service.GetAllPhoneNumbers();

                //assert
                result.Should().BeEquivalentTo(customers);
            }
        }

        [TestMethod]
        public void Given_User_Wants_The_phone_numbers_of_a_specific_person_Then_return_that_persons_numbers()
        {
            using (var ctx = new ApiContext(new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase(databaseName: "specific_numbers").Options))
            {
                //arrange
                SetUpData(ctx);
                _service = new CustomerService(new CustomerRepository(ctx));
                var customers = _service.GetAllPhoneNumbers();
                //act
                var result = _service.GetAllPhoneNumbersForCustomer(customers.Skip(1).First().CustomerId);

                //assert
                result.CustomerId.Should().Be(customers.Skip(1).First().CustomerId);
            }
        }

        [TestMethod]
        public void Given_User_Wants_The_phone_numbers_of_a_specific_person_Then_it_shouldnt_return_someone_elses_person_numbers()
        {
            using (var ctx = new ApiContext(new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase(databaseName: "specific_numbers_not").Options))
            {
                //arrange
                SetUpData(ctx);
                _service = new CustomerService(new CustomerRepository(ctx));
                var customers = _service.GetAllPhoneNumbers();
                //act
                var result = _service.GetAllPhoneNumbersForCustomer(customers.Skip(1).First().CustomerId);

                //assert
                result.CustomerId.Should().NotBe(customers.First().CustomerId);
            }
        }

        [TestMethod]
        public void Give_user_wants_to_Activate_a_phone_number_Then_the_phone_number_should_activate()
        {
            using (var ctx = new ApiContext(new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase(databaseName: "activate_number").Options))
            {
                //arrange
                SetUpData(ctx);
                _service = new CustomerService(new CustomerRepository(ctx));

                //act
                var result = _service.ActivatePhoneNumber("07555555555");

                //assert
                result.Should().BeTrue();
            }
        }

        [TestMethod]
        public void Give_user_wants_to_Activate_a_unexisting_phone_number_Then_the_phone_number_should_not_activate()
        {
            using (var ctx = new ApiContext(new DbContextOptionsBuilder<ApiContext>().UseInMemoryDatabase(databaseName: "activate_number_not").Options))
            {
                //arrange
                SetUpData(ctx);
                _service = new CustomerService(new CustomerRepository(ctx));

                //act
                var result = _service.ActivatePhoneNumber("07");

                //assert
                result.Should().BeFalse();
            }
        }

        private IList<ICustomer> CreateMultipleCustomerList()
        {
            IPhoneNumber number = new PhoneNumber
            {
                Number = "07555555555",
                IsActive = false
            };

            IList<IPhoneNumber> phoneNums = new List<IPhoneNumber>() { number };

            ICustomer customer = new Customer
            {
                CustomerId = 1,
                PhoneNumbers = phoneNums
            };

            IList<ICustomer> customers = new List<ICustomer>();
            customers.Add(customer);

            IPhoneNumber number1 = new PhoneNumber
            {
                Number = "0711111111",
                IsActive = true
            };

            IPhoneNumber number2 = new PhoneNumber
            {
                Number = "0722222222",
                IsActive = true
            };

            phoneNums = new List<IPhoneNumber>() { number1, number2 };

            customer = new Customer
            {
                CustomerId = 2,
                PhoneNumbers = phoneNums
            };
            customers.Add(customer);
            return customers;
        }
    }
}
