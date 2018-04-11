using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{

    [Route("api/Customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetPhoneNumbers")]
        public IActionResult GetPhoneNumbers()
        {
            try
            {
                var customerCollection = _customerService.GetAllPhoneNumbers();
                return Ok(customerCollection);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpGet("GetCustomerPhoneNumbers/{customerId}")]
        public IActionResult GetCustomerPhoneNumbers(long customerId)
        {
            try
            {
                var customerPhoneNumberCollection = _customerService.GetAllPhoneNumbersForCustomer(customerId);
                return Ok(customerPhoneNumberCollection);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost("ActivatePhoneNumber/{phoneNumber}")]
        public IActionResult ActivatePhoneNumber(string phoneNumber)
        {
            try
            {
                var customerCollection = _customerService.ActivatePhoneNumber(phoneNumber);
                return Ok(customerCollection);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }
    }
}