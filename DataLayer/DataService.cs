using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class DataService : IDataService
    {
        //Category Functions
        public Category GetCategory(int id)
        {
            var context = new NorthwindContext();
            var category = context.Categories.Find(id);
            return category;

        }
        public IList<Category> GetCategories()
        {
            var context = new NorthwindContext();
            var categories = context.Categories.ToList();
            return categories;
        }

        public Category CreateCategory(string name, string description)
        {
            var context = new NorthwindContext();
            var id = context.Categories.Max(c => c.Id) + 1;
            var newCategory = new Category
            {
                Id = id,
                Name = name,
                Description = description,
            };
            context.Categories.Add(newCategory);
            context.SaveChanges();
            return context.Categories.Find(newCategory.Id);
        }

        public bool DeleteCategory(int id)
        {
            var context = new NorthwindContext();
            var category = context.Categories.Find(id);
            if (category != null)
            {
                context.Categories.Remove(category);
                return context.SaveChanges() > 0;
            }
            return false;
        }

        public bool UpdateCategory(int id, string name, string description)
        {
            var context = new NorthwindContext();
            var category = context.Categories.Find(id);
            if (category != null)
            {
                category.Name = name;
                category.Description = description;
                return context.SaveChanges() > 0;
            }
            return false;
        }

        //Products Functions
        public Product? GetProduct(int id)
        {
            var context = new NorthwindContext();
            var product = context.Products.Include(e => e.Category).FirstOrDefault(e => e.Id == id);
            return product;
        }

        public IList<DTONames> GetProductByName(string matchingSubstring)
        {
            var context = new NorthwindContext();
            var dto = context.Products
                    .Where(e => e.Name.Contains(matchingSubstring))
                    .Include(e => e.Category)
                    .Select(e => new DTONames
                    {
                        CategoryName = e.Category.Name,
                        ProductName = e.Name,
                    }).ToList();
            
            return dto;
        }

        public IList<Product> GetProductByCategory(int categoryId)
        {
            var context = new NorthwindContext();
            var products = context.Products.
                            Where(e => e.CategoryId == categoryId).
                            Include(e => e.Category).ToList();
            if (!products.Any()) {return new List<Product>(); }

            return products;
        }


        //Orders Functions
        public Order GetOrder(int orderId)
        {
            var context = new NorthwindContext();
            var order = context.Orders
                                .Include(e => e.OrderDetails)
                                .ThenInclude(p => p.Product)
                                .ThenInclude(c => c.Category)
                                .First(e => e.Id == orderId);
            return order;

        }

        public IList<Order> GetOrders()
        {
            var context = new NorthwindContext();
            var orders = context.Orders.ToList();
            return orders;
        }

        public IList<Order> GetOrdersByShippingName(string shippingname)
        {
            var context = new NorthwindContext();
            var orders = context.Orders.Where(s => s.ShipName == shippingname).ToList();
            return orders;
        }

        //OrderDetails Functions
        public IList<OrderDetails> GetOrderDetailsByOrderId(int orderid)
        {
            var context = new NorthwindContext();
            var orderDetails = context.OrderDetails
                                .Where(o => o.OrderId == orderid)
                                .Include(p => p.Product)
                                .ToList();
            return orderDetails;
        }

        public IList<OrderDetails> GetOrderDetailsByProductId(int productid)
        {
            var context = new NorthwindContext();
            var orderDetails = context.OrderDetails
                                       .Where(p => p.ProductId == productid)
                                       .Include(p => p.Product)
                                       .Include(o => o.Order)
                                       .OrderBy(o => o.OrderId)
                                       .ToList();
            return orderDetails;

        }
    }
}
