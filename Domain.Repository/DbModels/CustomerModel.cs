using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Repository
{
    public class CustomerModel
    {
        public long Id { get; set; }

        public List<PhoneNumberModel> PhoneNumbers { get; set; }
    }
}
