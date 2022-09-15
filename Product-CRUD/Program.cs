using ProductCRUD.Model;
using ProductCRUD.Repository;

namespace ProductCrud;

public class Program
{

    public static void Main()
    {
        String filePath = "/Users/edwardsuwirya/Downloads/product.txt";
        IProductRepository productRepo = new ProductFileRepository(filePath);

        productRepo.Add(new Product("3", "Mie Ayam"));
        productRepo.Add(new Product("4", "Mie Baso"));

        var products = productRepo.GetAll();
        foreach (var p in products)
        {
            Console.WriteLine(p.ToString());
        }

        var productResId = productRepo.FindById("3");
        Console.WriteLine(productResId?.ToString() ?? "No Product Found");

    }
}
