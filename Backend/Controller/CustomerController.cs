using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using backend.Models;
using backend.Interfaces;

namespace dotnet_practice.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService _custService){
            customerService = _custService;
        }

        [HttpGet]
        [Route("list")]
        public IEnumerable<Customer> Get()  
        {
            return customerService.GetAllCustomers();
        }

        [HttpGet]
        [Route("getCustomerList")]
        public IEnumerable<AutocompleteModel> GetCustomerList()
        {
            using var connection = DbCnx.GetConnection();
            var customers = connection.Query<AutocompleteModel>("SELECT id, name as Label FROM customer order by id desc");
            return customers;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is required.");
            }
        try
        {
            var AddedCust = customerService.AddCustomer(customer);
                  {
                   return Ok("Customer successfully added");
                  }
                 return StatusCode(500, "Failed to add the Customer.");
        }
        catch (Exception ex)
        {
            
           return StatusCode(500, ex.Message);
        }
                
        }

        [HttpPut]
        [Route("edit/{id}")]
        public IActionResult EditCustomer(int id, [FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is required.");
            }

           
         try
           {
            var UpdateCust = customerService.UpdateCustomer(customer);
                  {
                   return Ok("Customer Updated  Succesfully ");
                  }
                 return StatusCode(500, "Failed to add the Customer.");
        }
        catch (Exception ex)
        {
            
           return StatusCode(500, ex.Message);
        }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
           try
        {
         var isDeleted = customerService.DeleteCustomer(id) ;
         if(isDeleted)
        {
        return Ok("product Successfully deleted");
        }       
        else{
        return NotFound("Product Not Found");
         }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Something went wrong!");
        }
        }
    }
}
