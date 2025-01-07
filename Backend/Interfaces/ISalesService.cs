using backend.Models;

namespace backend.Interfaces;
public interface ISalesService{
    
    IEnumerable<SalesViewModel> GetList();

   bool  AddSales(Sales sales);
   bool EditSales(Sales sales);

   bool DeleteSales(int  salesId);
}