using System;
using System.Collections.Generic;
using Domain;
using Domain.Interfaces;
using Domain.Repository;
using Domain.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace WebApplication1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase(databaseName: "customer_phones"));

            services.AddMvc(config =>
                {
                    config.RespectBrowserAcceptHeader = true;
                    config.ReturnHttpNotAcceptable = true; // for restricting
                    config.InputFormatters.Add(new XmlSerializerInputFormatter());
                    config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                });

            services.AddTransient<ICustomer, Customer>();
            services.AddTransient<IPhoneNumber, PhoneNumber>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApiContext>();

            AddTestData(context);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private void AddTestData(ApiContext context)
        {
            var customer = new CustomerModel
            {
                PhoneNumbers = new List<PhoneNumberModel>
                    {
                        new PhoneNumberModel
                        {
                            PhoneNumber = "07111111111",
                            Active = true
                        },
                        new PhoneNumberModel
                        {
                            PhoneNumber = "07222222222",
                            Active = true
                        }
                    }
            };

            context.Customers.Add(customer);

            customer = new CustomerModel
            {
                PhoneNumbers = new List<PhoneNumberModel>
                    {
                        new PhoneNumberModel
                        {
                            PhoneNumber = "07333333333",
                            Active = true
                        },
                        new PhoneNumberModel
                        {
                            PhoneNumber = "07444444444",
                            Active = false
                        }
                    }
            };

            context.Customers.Add(customer);

            customer = new CustomerModel
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

            context.Customers.Add(customer);

            context.SaveChanges();
        }
    }
}
