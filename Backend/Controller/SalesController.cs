using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System;
using Dapper;
using System.Security.AccessControl;
using backend.Interfaces;


namespace backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService salesService;

         public SalesController(ISalesService _saleService){
            salesService = _saleService;
         }


        [HttpGet]
        [Route("GetSales")]
       public IEnumerable<SalesViewModel> GetList()
        {
           return salesService.GetList();
        }

       

       
        [HttpPost]
        [Route("AddSales")]
        public IActionResult AddSales([FromBody] Sales sales)
        {

            if (sales == null)
            {
                return BadRequest("Sales data is required");
            }
         
             if(salesService.AddSales(sales))
               {
            return Ok("Sales successfully added");
             }

           else{
            return StatusCode(500, "failed to add sales");
           }
                
            

        }


        [HttpPut]
        [Route("Edit")]
        public IActionResult EditProduct([FromBody]Sales sales)
        {
            if(sales==null){
                return BadRequest("Sales Data Required");
            }
            try{
               var isUpdated= salesService.EditSales(sales);
                if(isUpdated)
                {
                    return Ok("sales details updated");
                }
                 return StatusCode(500, "Failed to update product");
            }
            
            catch(Exception ex)
            {
                return StatusCode(500,$"Internal server error : {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("Delete/{salesId}")]
        public IActionResult DeleteProduct(int salesId){
           var delete =salesService.DeleteSales(salesId);
           if(delete){
            return Ok("sale is deleted");
           }
           return StatusCode(500, "Internal Server error");
        }
    }
}


