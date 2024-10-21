using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer;


public class Category
{
    public int Id { get; set; }


    public string Name { get; set; } = null;

    public string Description { get; set; } = null;

    public ICollection<Product> Products { get; }
}
