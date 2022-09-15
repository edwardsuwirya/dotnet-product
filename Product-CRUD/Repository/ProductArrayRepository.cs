using System;
using ProductCRUD.Model;

namespace ProductCRUD.Repository
{
    public class ProductArrayRepository : IProductRepository
    {
        private readonly List<Product> _products = new List<Product>();

        public List<Product> GetAll()
        {
            return _products;
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public Product? FindById(string id)
        {
            Product? product = null;
            for (var i = 0; i < _products.Count; i++)
            {
                if (_products[i].id == id)
                {
                    product = _products[i];
                    break;
                }
            }
            return product;
        }

        public List<Product> FindByNameLike(string name)
        {
            List<Product> productRes = new List<Product>();
            for (var i = 0; i < _products.Count; i++)
            {
                if (_products[i].productName.Contains(name))
                {
                    productRes.Add(_products[i]);
                }
            }
            return productRes;
        }
    }
}

