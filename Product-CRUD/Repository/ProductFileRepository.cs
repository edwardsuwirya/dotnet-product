using ProductCRUD.Model;

namespace ProductCRUD.Repository
{
    public class ProductFileRepository : IProductFileRepository
    {
        private readonly string _filePath;

        public ProductFileRepository(string filePath) => _filePath = filePath;

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            using (StreamReader sr = new StreamReader(_filePath))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] productStr = line.Split(",");
                    if (productStr.Length == 2)
                    {
                        products.Add(new Product(productStr[0], productStr[1]));
                    }
                }
            }
            return products;
        }

        public void Add(Product product)
        {
            using (StreamWriter sw = new StreamWriter(_filePath, true))
            {
                sw.WriteLine($"{product.id},{product.productName}");
            }
        }

        public Product? FindById(string id)
        {
            using (StreamReader sr = new StreamReader(_filePath))
            {
                string line;
                Product product = null;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] productStr = line.Split(",");
                    if (productStr[0] == id)
                    {
                        product = new Product(productStr[0], productStr[1]);
                        break;
                    }
                }
                return product;
            }
        }

        public List<Product> FindByNameLike(string name)
        {
            List<Product> productRes = new List<Product>();
            using (StreamReader sr = new StreamReader(_filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] productStr = line.Split(",");
                    if (productStr[1].Contains(name))
                    {
                        var product = new Product(productStr[0], productStr[1]);
                        productRes.Add(product);
                    }
                }
            }
            return productRes;
        }

        public void AddBulk(List<Product> newProducts)
        {
            throw new NotImplementedException();
        }
    }
}

