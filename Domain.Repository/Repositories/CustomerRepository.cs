using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private ApiContext _context;

        public CustomerRepository(ApiContext context)
        {
            _context = context;
        }

        public bool ActivatePhoneNumber(string phoneNumber)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.PhoneNumbers.Any(n => n.PhoneNumber.Equals(phoneNumber)));

            if (customer != null && customer.PhoneNumbers != null )
            {
                customer.PhoneNumbers.First(n => n.PhoneNumber.Equals(phoneNumber)).Active = true;
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public IList<ICustomer> GetAllPhoneNumbers()
        {
            return (from c in _context.Customers
                    select new Customer
                    {
                        CustomerId = c.Id,
                        PhoneNumbers = (from n in c.PhoneNumbers select new PhoneNumber { IsActive = n.Active, Number = n.PhoneNumber }).ToList<IPhoneNumber>()
                    }).ToList<ICustomer>();
        }

        public ICustomer GetAllPhoneNumbersForCustomer(long customerId)
        {
            return (from c in _context.Customers
                    where c.Id == customerId
                    select new Customer
                    {
                        CustomerId = c.Id,
                        PhoneNumbers = (from n in c.PhoneNumbers select new PhoneNumber { IsActive = n.Active, Number = n.PhoneNumber }).ToList<IPhoneNumber>()
                    }).ToList<ICustomer>().FirstOrDefault();
        }
    }
}
