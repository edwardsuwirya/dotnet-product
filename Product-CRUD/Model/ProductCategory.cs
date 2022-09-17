using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCRUD.Model;

[Table("m_product_category")]
public class ProductCategory
{
    [Key] public string id { get; set; }
    [Column("category_name")] public string categoryName { get; set; }

    public List<Product> Products { get; set; }
    public override string ToString() => $"{id}-{categoryName}";
}