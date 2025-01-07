using backend.Interfaces;
using backend.Models;
using Dapper;
using Microsoft.Identity.Client;


namespace backend.Services;
    public class SalesService : ISalesService
    {
        public IEnumerable<SalesViewModel> GetList()
        {
            try
            {
               using var connection=DbCnx.GetConnection();
                 var sql =@"SELECT s.*, p.""name"" as productName, c.name as customerName
                    FROM sales s
                    inner join product p on s.product_id = p.id
                    inner join customer c  on c.id = s.customer_id 
                    ORDER BY id";
               return connection.Query<SalesViewModel>(sql);
            }
            catch (Exception ex){
                return Enumerable.Empty<SalesViewModel>();
            }
        }

        public bool AddSales(Sales sales){
            try
            {
                using var connection =DbCnx.GetConnection();
                var sql="INSERT INTO Sales (customer_id,product_id,quantity,total_amount,sale_date,payment_method)" +
                "VALUES(@Customer_Id,@Product_Id,@Quantity,@Total_Price,@Sales_Date,@Payment_Method)";

                var result =connection.Execute(sql,new{
                      customer_id=sales.Customer_id,
                    product_id = sales.Product_id,
                    quantity= sales.Quantity,
                    total_price=sales.Total_amount,
                    sales_date=sales.Sale_date,
                    payment_method=sales.Payment_method
                });

                  return result>0; 
            }
            catch (Exception ex)
            {
                return false;
          //      throw new Exception("Error in Adding Sales", ex);
            }
        }

        public bool EditSales(Sales sales){
            try{
                using var connection =DbCnx.GetConnection();
                var sql=@"UPDATE SALES 
                        SET customer_id=@Customer_Id,product_id=@Product_Id,quantity=@Quantity,total_amount=@total_amount,sale_date=@Sale_Date,payment_method=@Payment_Method 
                        where id=@id";
                var result=connection.Execute(sql,new{
                    id=sales.Id,
                    product_id=sales.Product_id,
                    customer_id=sales.Customer_id,
                    quantity=sales.Quantity,
                    total_amount=sales.Total_amount,
                    sale_date=sales.Sale_date,
                    payment_method=sales.Payment_method
                });

                return result>0;
            }
            catch (Exception ex){
                  throw new Exception("Error in Editing" + ex.Message);
            }
        }


        public bool DeleteSales(int salesId){
            try{
                using var connection =DbCnx.GetConnection();
                var sql="DELETE from Sales Where id=@Id";
                var result=connection.Execute(sql,new {Id = salesId});

                return result>0;
            }
            catch(Exception ex){
                throw new Exception("Error in Deleting a sale" + ex.Message);
            }
        }
    }
