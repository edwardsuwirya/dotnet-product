namespace ProductCrud;

public class Program
{
    public static void Main()
    {
        IProductRepository productRepo = new ProductRepository();
        productRepo.Add(new Product("1", "Nasi Goreng"));
        productRepo.Add(new Product("2", "Es teh tawar"));

        var products = productRepo.GetAll();
        foreach (var p in products)
        {
            Console.WriteLine(p.ToString());
        }

        var productResId = productRepo.FindById("2");
        Console.WriteLine(productResId?.ToString() ?? "No Product Found");
    }
}

public interface IProductRepository
{
    public List<Product> GetAll();
    public void Add(Product product);
    public Product? FindById(string id);
    public List<Product> FindByNameLike(string name);
}

public class ProductRepository : IProductRepository
{
    List<Product> products = new List<Product>();

    public List<Product> GetAll()
    {
        return products;
    }

    public void Add(Product product)
    {
        products.Add(product);
    }

    public Product? FindById(string id)
    {
        Product? product = null;
        for (var i = 0; i < products.Count; i++)
        {
            if (products[i].id == id)
            {
                product = products[i];
                break;
            }
        }
        return product;
    }

    public List<Product> FindByNameLike(string name)
    {
        List<Product> productRes = new List<Product>();
        for (var i = 0; i < products.Count; i++)
        {
            if (products[i].productName.Contains(name))
            {
                productRes.Add(products[i]);
            }
        }
        return productRes;
    }
}

public class Product
{
    public string id { get; set; }
    public string productName { get; set; }

    public Product(string id, string productName)
    {
        this.id = id;
        this.productName = productName;
    }
    public override string ToString() => $"{id}-{productName}";
}