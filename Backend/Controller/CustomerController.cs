using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using backend.Models;

namespace dotnet_practice.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("list")]
        public IEnumerable<Customer> Get()
        {
            using var connection = DbCnx.GetConnection();
            var customers = connection.Query<Customer>("SELECT * FROM customer order by id desc");
            return customers;
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

            using var connection = DbCnx.GetConnection();
            var sql = "INSERT INTO customer (name, phone, email, city) VALUES (@Name, @Phone, @Email, @City)";
            var result = connection.Execute(sql, new
            {
                
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                City = customer.City
            });

            if (result > 0)
            {
                return Ok("Customer added successfully.");
            }

            return StatusCode(500, "Failed to add customer.");
        }

        [HttpPut]
        [Route("edit/{id}")]
        public IActionResult EditCustomer(int id, [FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is required.");
            }

            using var connection = DbCnx.GetConnection();
            var sql = "UPDATE customer SET name = @Name, phone = @Phone, email = @Email, city = @City WHERE id = @Id";
            var result = connection.Execute(sql, new
            {
                Id = id,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                City = customer.City
            });

            if (result > 0)
            {
                return Ok("Customer successfully updated.");
            }

            return NotFound("Customer not found.");
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            using var connection = DbCnx.GetConnection();
            var deleteSalesSql = "DELETE FROM sales WHERE customer_id = @Id";
            connection.Execute(deleteSalesSql, new { Id = id });

            var sql = "DELETE FROM customer WHERE id = @Id";
            var result = connection.Execute(sql, new { Id = id });

            if (result > 0)
            {
                return Ok("Customer deleted successfully.");
            }

            return NotFound("Customer not found.");
        }
    }
}
