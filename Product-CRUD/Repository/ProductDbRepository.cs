using Npgsql;
using ProductCRUD.Model;

namespace ProductCRUD.Repository
{
    public class ProductDbRepository : BaseDbRepository, IProductDbRepository
    {
        public ProductDbRepository(string connString)
        {
            GetConnection(connString);
        }

        public List<Product> GetAll()
        {
            var products = new List<Product>();
            try
            {
                QuerySql("SELECT * from m_product", null, (NpgsqlDataReader rdr) =>
                {
                    while (rdr.Read())
                    {
                        products.Add(new Product(rdr.GetString(0), rdr.GetString(1)));
                    }
                }).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Failed Get All Data");
            }

            return products;
        }

        public void Add(Product product)
        {
            try
            {
                ExecSql("INSERT INTO m_product VALUES($1, $2)", new List<string> { product.id, product.productName })
                    .Wait();
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
                var products = new List<Product>();
                QuerySql("SELECT * from m_product where id=$1", new List<string> { id }, (NpgsqlDataReader rdr) =>
                {
                    while (rdr.Read())
                    {
                        products.Add(new Product(rdr.GetString(0), rdr.GetString(1)));
                    }
                }).Wait();
                return products.Count == 0 ? null : products[0];
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
                var products = new List<Product>();
                QuerySql("SELECT * from m_product where name like $1", new List<string> { name },
                    (NpgsqlDataReader rdr) =>
                    {
                        while (rdr.Read())
                        {
                            products.Add(new Product(rdr.GetString(0), rdr.GetString(1)));
                        }
                    }).Wait();
                return products;
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
                var batchCommands = new List<NpgsqlBatchCommand>();
                foreach (var product in newProducts)
                {
                    var command = new NpgsqlBatchCommand("INSERT INTO m_product VALUES($1, $2)");
                    command.Parameters.AddWithValue(product.id);
                    command.Parameters.AddWithValue(product.productName);
                    batchCommands.Add(command);
                }

                ExecBatchInsertSql(batchCommands);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Failed Add Data");
            }
        }
    }
}