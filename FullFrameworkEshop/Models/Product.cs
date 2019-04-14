using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FullFrameworkEshop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProductTypeId { get; set; }

        [ForeignKey(nameof(Product.ProductTypeId))]
        public virtual ProductType ProductType { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}