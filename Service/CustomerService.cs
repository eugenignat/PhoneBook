using Domain.Interfaces;
using System;
using System.Collections.Generic;


namespace Domain.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public bool ActivatePhoneNumber(string phoneNumber)
        {
            return _customerRepository.ActivatePhoneNumber(phoneNumber);
        }

        public IList<ICustomer> GetAllPhoneNumbers()
        {
            return _customerRepository.GetAllPhoneNumbers();
        }

        public ICustomer GetAllPhoneNumbersForCustomer(long customerId)
        {
            return _customerRepository.GetAllPhoneNumbersForCustomer(customerId);
        }
    }
}
