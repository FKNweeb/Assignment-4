using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;



public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string QuantityPerUnit { get; set; } = null;
    public int UnitPrice { get; set; }

    public int UnitsInStock { get; set; }

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null;

    public string CategoryName
    {
        get => Category?.Name ?? "";
    }


}
