using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface ICustomerService
    {
        IList<ICustomer> GetAllPhoneNumbers();

        ICustomer GetAllPhoneNumbersForCustomer(long customerId);

        bool ActivatePhoneNumber(string phoneNumber);
    }
}
