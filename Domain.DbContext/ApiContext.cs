using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<CustomerModel> Customers { get; set; }
    }
}
