using ProductCRUD.Model;
using ProductCRUD.Repository;

namespace ProductCRUD.UseCase
{
    public class ProductRegistrationUseCase
    {
        private readonly IProductRepository _productRepository;
        public ProductRegistrationUseCase(IProductRepository productRepository) => _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));

        public void Handle(Product newProduct)
        {
            _productRepository.Add(newProduct);
        }
    }
}

