using System.Diagnostics.CodeAnalysis;
using backend.Interfaces;
using backend.Models;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace backend.Services;

public class ProductService : IProductService
{
    public IEnumerable<Product> GetList()
    {
        try
        {
            using var connection = DbCnx.GetConnection();

            return connection.Query<Product>("select * from product p");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Enumerable.Empty<Product>();
        }
    }

    public Product GetById(int id)
{
    try
    {
        using var connection = DbCnx.GetConnection();
        var sql = "SELECT * FROM product WHERE id = @Id";
        var product = connection.QueryFirstOrDefault<Product>(sql, new { Id = id });

        return product; 
    }
    catch (Exception ex)
    {
        throw new Exception("An error occurred while fetching the product.", ex);
    }
}

       public bool AddProduct(Product product){
        try
        {
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

             return result>0; 
        }
        catch (Exception ex)
        {
            
            throw new Exception("An error occurred while adding the product.", ex);
        }
       }


       public bool UpdateProduct(Product product){

        try{
            using var connection = DbCnx.GetConnection();
            var sql = @"UPDATE product SET name=@Name, category=@Category,description=@Description, price=@price,stock_quantity=@Quantity  where id=@Id";
            var result = connection.Execute(sql,new {
                Name=product.Name,
                Category= product.category,
                Description=product.description,
                Price=product.price,
                Quantity=product.stock_quantity,
            });

       return result > 0;
        }
        catch(Exception ex){
            throw new Exception("An error occured while editin the Product", ex);
        }
       }

       public bool DeleteProduct(int id){
        try{
            using var connection =DbCnx.GetConnection();
            var sql = "DELETE from Product where id = @Id ";
            var result = connection.Execute(sql,new {Id=id});
            return result > 0;
            }
            catch(Exception ex){
                throw new Exception("An error occured while deleteing a product", ex);
            }
        }

       public IEnumerable<AutocompleteModel>GetAutocompleteModels(){
        try
        {
            using var connection = DbCnx.GetConnection();
            var Products =connection.Query<AutocompleteModel>("SELECT id, name as Label FROM product order by id desc");
            return Products;
        }
        catch (Exception ex)
        {
            
            throw new Exception("Something Went Wrong", ex);
        }
       }
}