using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCRUD.Model
{
    [Table("m_product")]
    public class Product
    {
        [Key] public string id { get; set; }
        [Column("product_name")] public string productName { get; set; }

        [Column("category_id")] public string ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public Product(string id, string productName)
        {
            this.id = id;
            this.productName = productName;
        }

        public override string ToString() => $"{id}-{productName}-{ProductCategory.ToString()}";
    }
}