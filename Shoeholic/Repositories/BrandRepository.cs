using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Shoeholic.Models;
using Shoeholic.Utils;
using Microsoft.Data.SqlClient;

namespace Shoeholic.Repositories
{
    public class BrandRepository : BaseRepository, IBrandRepository
    {
        public BrandRepository(IConfiguration configuration) : base(configuration) { }

        public List<Brand> GetAllBrands()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT Id, [Name]
                                        FROM Brand";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Brand> brands = new List<Brand>();
                        while (reader.Read())
                        {
                            Brand brand = new Brand
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name")
                            };
                            brands.Add(brand);
                        }
                        return brands;
                    }
                }
            }
        }
    }
}
