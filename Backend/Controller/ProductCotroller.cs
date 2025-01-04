using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using System.ComponentModel.Design;

namespace dotnet_practice.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        // private readonly string _connectionString = "Host=192.168.1.10;Port=5432;Database=test;User Id=postgres;Password=PostgresDB@dm1n;"; 
        [HttpGet]
         [Route("list")]
       public IEnumerable<Product> Get()
        {
            using var connection = DbCnx.GetConnection();

            var products = connection.Query<Product>("select * from product p");

            return products;
        }


        
        [HttpGet]
        [Route("get/id")]

        public IActionResult GetById(int id){
            using var connection =DbCnx.GetConnection();
            var sql ="SELECT * from product where id =@Id";
            var product = connection.QueryFirstOrDefault<Product>(sql, new {Id= id});

            if(product!= null){
                return Ok(product);
            }
            return NotFound("Product not Found ");
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddProduct([FromBody] Product product){
        
            if(product==null){
                return BadRequest("product data is required");
            }

            using var connection =DbCnx.GetConnection();
            var sql = "INSERT INTO product ( name, category, description, price, stock_quantity) " +
              "VALUES ( @Name, @category, @description, @price, @stock_quantity)";

             var result = connection.Execute(sql, new {
                        Name= product.Name,
                        category = product.category,
                        description = product.description,
                       price = product.price,
                       stock_quantity = product.stock_quantity
             });
                
             if(result>0){
                    
                return Ok("Product successfully added");
             }
             return StatusCode(500, "Failed to add the product.");
        }


        [HttpPut]
        [Route("edit/{id}")]

      public IActionResult EditProduct (int id,[FromBody] Product product ){
        if(product ==null){
            return BadRequest("product data is required");
        }

        using var connection =DbCnx.GetConnection();
        var sql = @"UPDATE product SET name=@Name, category=@Category,description=@Description, price=@price,stock_quantity=@Quantity  where id=@Id";

         var result = connection.Execute(sql,new {
                Id =id,
                Name=product.Name,
                Category= product.category,
                Description=product.description,
                Price=product.price,
                Quantity=product.stock_quantity,
               
         });

         if(result>0){
             
            return Ok ("Product sccessfully updated.");
         }
         return NotFound("product not found");
         
      }




      [HttpDelete]
      [Route("delete/{id}")]

      public IActionResult deleteProduct(int id ){
         using var connection=DbCnx.GetConnection();
         var sql ="DELETE from product where id =@Id";

         var result = connection.Execute(sql, new {Id=id});

         if(result>0){
            return Ok(" Product successfully deleted");

         }
         return NotFound(" Product Not found ");
      }

      

    }

}