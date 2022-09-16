using System.Data;
using Dapper;
using Product_CRUD;
using ProductCRUD.Model;

namespace ProductCRUD.Repository
{
    public class ProductDbRepository : BaseDbRepository, IProductDbRepository
    {
        public ProductDbRepository(DapperContext context) => GetConnection(context);

        public List<Product> GetAll()
        {
            var products = new List<Product>();
            try
            {
                WithConn((con) =>
                {
                    var results = con.Query<Product>("SELECT id, product_name as productName from m_product");
                    products = results.ToList();
                });
            }
            catch (Exception e)
            {
                throw new Exception("Failed Get All Data");
            }

            return products;
        }

        public void Add(Product product)
        {
            try
            {
                WithConn((con) =>
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("id", product.id, DbType.String);
                    parameters.Add("name", product.productName, DbType.String);
                    con.Execute("INSERT INTO m_product VALUES(@id, @name)", parameters);
                });
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
                Product? product = null;
                WithConn((con) =>
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("id", id, DbType.String);
                    var results =
                        con.QuerySingleOrDefault<Product>(
                            "SELECT id,product_name as productName from m_product where id=@id",
                            parameters);
                    product = results;
                });
                return product;
            }
            catch (Exception e)
            {
                throw new Exception("Failed Find Data");
            }
        }

        public List<Product> FindByNameLike(string name)
        {
            try
            {
                var products = new List<Product>();
                WithConn(con =>
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("name", $"%{name}%", DbType.String);
                    var results =
                        con.Query<Product>("SELECT id,product_name as productName from m_product where name like @name",
                            parameters);
                    products = results.ToList();
                });
                return products;
            }
            catch (Exception e)
            {
                throw new Exception("Failed Find Data");
            }
        }

        public void AddBulk(List<Product> newProducts)
        {
            try
            {
                WithTrx((con, trx) =>
                {
                    foreach (var product in newProducts)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("id", product.id, DbType.String);
                        parameters.Add("name", product.productName, DbType.String);
                        con.Execute("INSERT INTO m_product VALUES(@id, @name)", parameters,
                            transaction: trx);
                    }
                });
            }
            catch (Exception e)
            {
                throw new Exception("Failed Insert Bulk Data");
            }
        }
    }
}