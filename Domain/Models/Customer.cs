using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Customer : ICustomer
    {
        public long CustomerId { get; set; }

        public IList<IPhoneNumber> PhoneNumbers { get; set; }
    }
}
