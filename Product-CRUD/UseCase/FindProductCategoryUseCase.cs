using ProductCRUD.Model;
using ProductCRUD.Repository;

namespace ProductCRUD.UseCase
{
    public class FindProductCategoryUseCase
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public FindProductCategoryUseCase(IProductCategoryRepository productCategoryRepository) =>
            _productCategoryRepository =
                productCategoryRepository ?? throw new ArgumentException(nameof(productCategoryRepository));

        public List<ProductCategory> Handle()
        {
            return _productCategoryRepository.GetAll();
        }
    }
}