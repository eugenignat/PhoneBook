﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DbContext
{
    public  class PhoneNumberModel
    {
        public long Id { get; set; }

        public bool Active { get; set; }

        public string PhoneNumber { get; set; }
    }
}
