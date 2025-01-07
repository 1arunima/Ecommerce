using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using System.ComponentModel.Design;
using backend.Models;
using backend.Interfaces;

namespace dotnet_practice.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        // private readonly string _connectionString = "Host=192.168.1.10;Port=5432;Database=test;User Id=postgres;Password=PostgresDB@dm1n;"; 
        private readonly IProductService productService;

        public ProductController(IProductService _prdtService)
        {
            productService = _prdtService;

        }

        [HttpGet]
         [Route("list")]
       public IEnumerable<Product> Get()
        {
            return productService.GetList();
        }

        [HttpGet]
        [Route("getProductList")]
       public IEnumerable<AutocompleteModel> GetProductList()
   {
    var autocompleteModels = productService.GetAutocompleteModels();

    if (autocompleteModels != autocompleteModels)
      {
        return autocompleteModels; 
       }
        else
       {
        throw new Exception("Products not found.");     
       }
   }
        [HttpGet]
        [Route("get/id")]
       public IEnumerable<Product> GetById(int id)
        {
             var product = productService.GetById(id);
         return product != null ? new List<Product> { product } : Enumerable.Empty<Product>();
        }
        [HttpPost]
        [Route("add")]
        public IActionResult AddProduct ([FromBody] Product product){
            if(product == null){
                return BadRequest("Product data is required");
            }

            try
            {
               var isAdded= productService.AddProduct(product);
                {
            return Ok("Product successfully added");
             }

                return StatusCode(500, "Failed to add the product.");
            }
            catch (Exception ex)
            {
                
           return StatusCode(500, ex.Message);  

              }
        }


        [HttpPut]
        [Route("edit/{id}")]

      public IActionResult UpdateProduct (int id,[FromBody] Product product ){
        if(product ==null){
            return BadRequest("product data is required");
        }

        try
        {
            var isUpdated= productService.UpdateProduct(product);

            if (isUpdated)
            {
                 return Ok("Product successfully updated.");
            }else{
                return NotFound("Product not found.");
            }
        }
           catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
      }




      [HttpDelete]
      [Route("delete/{id}")]

      public IActionResult DeleteProduct(int id ){
        try
        {
         var isDeleted = productService.DeleteProduct(id) ;
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