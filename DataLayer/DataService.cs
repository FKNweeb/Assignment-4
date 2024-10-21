using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace DataLayer;
public class DataService : IDataService
{
    public Category CreateCategory(string name, string description)
    {
        var db = new NorthwindContext();

        int id = db.Categories.Max(x => x.Id) + 1;
        var category = new Category
        {
            Id = id,
            Name = name,
            Description = description
        };

        db.Categories.Add(category);

        db.SaveChanges();

        return category;

    }

    public bool DeleteCategory(int id)
    {
        var db = new NorthwindContext();

        var category = db.Categories.Find(id);

        if (category == null)
        {
            return false;
        }

        db.Categories.Remove(category);

        return db.SaveChanges() > 0;

    }

    public bool UpdateCategory(int id, string name, string description){
        var db = new NorthwindContext();

        var category = db.Categories.Find(id);

        if (category == null)
        {
            return false;
        }

        category.Name = name;
        category.Description = description;
        
        return db.SaveChanges() > 0;
    }

    public bool UpdateCategory(Category category){
        var db = new NorthwindContext();

        var newCategory = db.Categories.Find(category.Id);

        if (newCategory == null) { return false; }

        newCategory.Name = category.Name;
        newCategory.Description = category.Description;

        return db.SaveChanges() > 0;
    }

    public IList<Category> GetCategories()
    {
        var db = new NorthwindContext();
        return db.Categories.ToList();
    }

    public Category? GetCategory(int index){
        var db = new NorthwindContext();
        return db.Categories.Find(index);
    }

    public IList<Product> GetProducts()
    {
        var db = new NorthwindContext();
        return db.Products.Include(x => x.Category).ToList();
    }
    public Product? GetProduct(int index) {
        var db = new NorthwindContext();
        return db.Products.Include(p => p.Category)
                          .FirstOrDefault(p => p.CategoryId == index);
    }
    public IList<Product> GetProductByCategory(int index){
        var db = new NorthwindContext();
        return db.Products.Where(p => p.CategoryId == index)
                                  .Include(p => p.Category).ToList();
        
    }
    public IList<DTOName> GetProductByName(string name){
        var db = new NorthwindContext();
        var product = db.Products.Where(p => p.Name.Contains(name))
                                 .Include(p => p.Category)
                                 .Select(p => new DTOName 
                                 {
                                    ProductName = p.Name,
                                    CategoryName = p.Category.Name
                                 }).ToList();
        return product;
    }
    public IList<Order> GetOrders()
    {
        var context = new NorthwindContext();
        var orders = context.Orders.ToList();
        return orders;
    }
    public Order GetOrder(int orderId)
    {
        var context = new NorthwindContext();
        var order = context.Orders.Include(o => o.OrderDetails)
                                  .ThenInclude(p => p.Product)
                                  .ThenInclude(c => c.Category)
                                  .First(c => c.Id == orderId);
        return order;
    }

    public IList<Order> GetOrdersByShippingName(string shippingname)
    {
        var context = new NorthwindContext();
        var orders = context.Orders.Where(o => o.ShipName == shippingname).ToList();
        return orders;
    }

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
        var orderDetails = context.OrderDetails.Where(p => p.ProductId == productid)
                                               .Include(p => p.Product)
                                               .Include(o => o.Order)
                                               .OrderBy(o => o.OrderId)
                                               .ToList();
        return orderDetails;
                                   
    }
}