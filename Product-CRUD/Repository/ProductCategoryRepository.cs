using ProductCRUD.Model;

namespace ProductCRUD.Repository
{
    public interface IProductCategoryRepository
    {
        public List<ProductCategory> GetAll();
        public void Add(ProductCategory category);
        public ProductCategory? FindById(string id);
        public List<ProductCategory> FindByNameLike(string name);
    }
}