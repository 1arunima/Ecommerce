using backend.Interfaces;
using Dapper;

namespace backend.Services;

public class CustomerService :ICustomerService{



public IEnumerable<Customer> GetAllCustomers()
{
    try
    {
        using var connection = DbCnx.GetConnection();
        var sql = "SELECT * FROM customer ORDER BY id DESC";
        var customers = connection.Query<Customer>(sql);
        return customers;
    }
    catch (Exception ex)
    {
        throw new Exception("An error occurred while fetching customers.", ex);
    }
}


   public bool AddCustomer(Customer customer)
{
    try
    {
        using var connection = DbCnx.GetConnection();
        var sql = "INSERT INTO customer (name, phone, email, city) VALUES (@Name, @Phone, @Email, @City)";

        var result = connection.Execute(sql, new
        {
            customer.Name,
            customer.Phone,
            customer.Email,
            customer.City
        });
        return result > 0;
    }
    catch (Exception ex)
    {
       
        throw new Exception("An error occurred while adding the customer.", ex);
    }
}


public bool UpdateCustomer(Customer customer){
    try{
        using var connection = DbCnx.GetConnection();
     var sql = "UPDATE customer SET name = @Name, phone = @Phone, email = @Email, city = @City WHERE id = @Id";
     var result =connection.Execute(sql, new{
        
        Name =customer.Name,
        Phone = customer.Phone,
        Email = customer.Email,
        City = customer.City,
     });
    return result > 0;
    }
    catch(Exception ex){
        throw new Exception("Error Occured While Updating the Customer", ex);
    }
}

public bool DeleteCustomer(int id){
    try{
        using var connection = DbCnx.GetConnection();
        var deleteSalesSql ="DELETE FROM sales WHERE customer_id = @Id";

        connection.Execute(deleteSalesSql,new {Id=id});

        var sql = "DELETE FROM customer WHERE id = @Id";
        var result =connection.Execute(sql, new{Id=id});
                   return result > 0;

    }
    catch(Exception ex){
        throw new Exception("An error Occured while deleteing a customer", ex);
    }
}
    
}