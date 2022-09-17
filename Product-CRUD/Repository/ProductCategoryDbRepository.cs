using Microsoft.EntityFrameworkCore;
using Product_CRUD;
using ProductCRUD.Model;

namespace ProductCRUD.Repository;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly ProductContext _context;
    public ProductCategoryRepository(ProductContext context) => _context = context;


    public List<ProductCategory> GetAll()
    {
        try
        {
            return _context.ProductCategories.Include(p => p.Products).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception("Failed Get All Data");
        }
    }

    public void Add(ProductCategory category)
    {
        throw new NotImplementedException();
    }

    public ProductCategory? FindById(string id)
    {
        throw new NotImplementedException();
    }

    public List<ProductCategory> FindByNameLike(string name)
    {
        throw new NotImplementedException();
    }
}