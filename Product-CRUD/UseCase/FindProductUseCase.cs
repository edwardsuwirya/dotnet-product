using System;
using ProductCRUD.Model;
using ProductCRUD.Repository;
using ProductCRUD.Utils;

namespace ProductCRUD.UseCase
{
    public class FindProductUseCase
    {
        private readonly IProductRepository _productRepository;
        public FindProductUseCase(IProductRepository productRepository) => _productRepository = productRepository ?? throw new ArgumentException(nameof(productRepository));
        public List<Product> Handle(ProductFindType type, Object param = null)
        {
            switch (type)
            {
                case ProductFindType.All:
                    {
                        var result = _productRepository.GetAll();
                        return result;
                    }
                case ProductFindType.ById:
                    {
                        var result = _productRepository.FindById(Convert.ToString(param));
                        if (result is not null)
                        {
                            return new List<Product> { result };
                        }
                        else
                        {
                            return new List<Product>();
                        }
                    }
                case ProductFindType.ByName:
                    {
                        return new List<Product>();
                    }
                default: return new List<Product>();
            }
        }
    }
}

