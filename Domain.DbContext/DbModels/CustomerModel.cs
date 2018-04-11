using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DbContext
{
    public class CustomerModel
    {
        public long Id { get; set; }

        public List<PhoneNumberModel> PhoneNumbers { get; set; }
    }
}
