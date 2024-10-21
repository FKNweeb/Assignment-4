namespace DataLayer;
public interface IDataService
{
    IList<Category> GetCategories();
    Category? GetCategory(int index);
    Category CreateCategory(string name, string description);

    bool DeleteCategory(int id);
    bool UpdateCategory(int id, string name, string description);
    bool UpdateCategory(Category category);

    IList<Product> GetProducts();
    Product? GetProduct(int index);
    IList<Product> GetProductByCategory(int index);
    IList<DTOName> GetProductByName(string name);
    IList<Order> GetOrders();
    Order GetOrder(int orderId);
    IList<Order> GetOrdersByShippingName(string shippingname);
    IList<OrderDetails> GetOrderDetailsByOrderId(int orderid);
    IList<OrderDetails> GetOrderDetailsByProductId(int productid);
}