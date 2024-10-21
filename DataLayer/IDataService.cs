namespace DataLayer
{
    public interface IDataService
    {
        //Category
        public Category GetCategory(int id);
        public IList<Category> GetCategories();

        public Category CreateCategory(string name, string description);

        public bool DeleteCategory(int id);

        public bool UpdateCategory(int id, string name, string description);


        //Product
        public Product? GetProduct(int id);

        public IList<DTONames> GetProductByName(string matchingSubstring);

        public IList<Product> GetProductByCategory(int id);


        //Orders
        public Order GetOrder(int orderId);


        public IList<Order> GetOrdersByShippingName(string shippingname);

        public IList<Order> GetOrders();

        //OrderDetails
        public IList<OrderDetails> GetOrderDetailsByOrderId(int orderid);
        public IList<OrderDetails> GetOrderDetailsByProductId(int productid);
    }
}