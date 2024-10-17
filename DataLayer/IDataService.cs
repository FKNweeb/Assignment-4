namespace DataLayer;
public interface IDataService
{
    IList<Category> GetCategories();

    Category CreateCategory(string name, string description);

    bool DeleteCategory(int id);
    bool UpdateCategory(int id, string name, string description);

    IList<Product> GetProducts();
}