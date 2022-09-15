using ProductCRUD.Model;
using ProductCRUD.Repository;

namespace ProductCrud;

public class Program
{

    public static void Main()
    {
        IProductRepository productRepo = new ProductArrayRepository();

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
