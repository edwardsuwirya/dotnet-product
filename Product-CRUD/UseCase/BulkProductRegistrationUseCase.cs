using ProductCRUD.Model;
using ProductCRUD.Repository;

namespace ProductCRUD.UseCase
{
    public class BulkProductRegistrationUseCase
    {
        private readonly IProductRepository _productRepository;

        public BulkProductRegistrationUseCase(IProductRepository productRepository) => _productRepository =
            productRepository ?? throw new ArgumentException(nameof(productRepository));

        public void Handle(List<Product> newProducts)
        {
            _productRepository.AddBulk(newProducts);
        }
    }
}