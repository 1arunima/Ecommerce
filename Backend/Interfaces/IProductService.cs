using backend.Models;

namespace backend.Interfaces;


public interface IProductService
{
    IEnumerable<Product> GetList();
    Product GetById(int id);

    bool AddProduct(Product product);
    bool UpdateProduct(Product product);

    bool DeleteProduct(int id);
    IEnumerable<AutocompleteModel>GetAutocompleteModels();
}