using Microsoft.EntityFrameworkCore;
using Product_CRUD;
using ProductCRUD.Model;

namespace ProductCRUD.Repository
{
    public class ProductDbRepository : IProductDbRepository
    {
        private readonly ProductContext _context;
        public ProductDbRepository(ProductContext context) => _context = context;

        public List<Product> GetAll()
        {
            try
            {
                return _context.Products.Include(category => category.ProductCategory).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Failed Get All Data");
            }
        }

        public void Add(Product product)
        {
            try
            {
                _context.Products.Add(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Failed Insert Data");
            }
        }

        public Product? FindById(string id)
        {
            try
            {
                return _context.Products.SingleOrDefault(p => p.id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Failed Find Data");
            }
        }

        public List<Product> FindByNameLike(string name)
        {
            try
            {
                return _context.Products.Where(p => p.productName.Contains(name)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Failed Find Data");
            }
        }

        public void AddBulk(List<Product> newProducts)
        {
            try
            {
                foreach (var product in newProducts)
                {
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Failed Add Data");
            }
        }
    }
}