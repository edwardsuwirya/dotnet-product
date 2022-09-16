using ProductCRUD.Model;

namespace ProductCRUD.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetAll();
        public void Add(Product product);
        public Product? FindById(string id);
        public List<Product> FindByNameLike(string name);

        public void AddBulk(List<Product> newProducts);
    }

    public interface IProductFileRepository : IProductRepository
    {
    }

    public interface IProductArrayRepository : IProductRepository
    {
    }

    public interface IProductDbRepository : IProductRepository
    {
    }
}