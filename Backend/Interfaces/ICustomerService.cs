using backend.Models;

namespace backend.Interfaces;

public interface ICustomerService{


    IEnumerable<Customer> GetAllCustomers();
     bool AddCustomer(Customer customer);

     bool  UpdateCustomer(Customer customer);
      bool DeleteCustomer( int id);
   
}