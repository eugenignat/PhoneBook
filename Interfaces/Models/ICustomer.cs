using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface ICustomer
    {
        long CustomerId { get; set; }

        IList<IPhoneNumber> PhoneNumbers { get; set; }
    }
}
