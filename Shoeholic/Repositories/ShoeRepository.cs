using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Shoeholic.Models;
using Shoeholic.Utils;
using Microsoft.Data.SqlClient;

namespace Shoeholic.Repositories
{
    public class ShoeRepository : BaseRepository, IShoeRepository
    {
        public ShoeRepository(IConfiguration configuration) : base(configuration) { }

        public List<Shoe> GetAllShoes()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT s.Id, s.Name, s.BrandId, s.ReleaseDate, s.Title, s.ColorWay, b.Name AS BrandName
                        FROM Shoe s
                        JOIN Brand b ON b.Id = s.BrandId";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Shoe> shoes = new List<Shoe>();
                        while (reader.Read())
                        {
                            shoes.Add(new Shoe()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                BrandId = DbUtils.GetInt(reader, "BrandId"),
                                ReleaseDate = DbUtils.GetDateTime(reader, "ReleaseDate"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Colorway = DbUtils.GetString(reader, "ColorWay"),
                                Brand = new Brand()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Name = DbUtils.GetString(reader, "Name")
                                },
                            });
                        }
                        return shoes;
                    }
                }
            }
        }
        public void Add(Shoe shoe)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Shoe (Name, BrandId, ReleaseDate, RetailPrice, PurchaseDate, Title, ColorWay, CollectionId)
                        OUTPUT INSERTED.ID
                        VALUES (@name, @brandId, @releaseDate, @retailPrice, @purchaseDate, @title, @colorWay, @collectionId)";

                    DbUtils.AddParameter(cmd, "@name", shoe.Name);
                    DbUtils.AddParameter(cmd, "@brandId", shoe.BrandId);
                    DbUtils.AddParameter(cmd, "@releaseDate", shoe.ReleaseDate);
                    DbUtils.AddParameter(cmd, "@retailPrice", shoe.RetailPrice);
                    DbUtils.AddParameter(cmd, "@purchaseDate", shoe.PurchaseDate);
                    DbUtils.AddParameter(cmd, "@title", shoe.Title);
                    DbUtils.AddParameter(cmd, "@colorWay", shoe.Colorway);
                    DbUtils.AddParameter(cmd, "@collectionId", shoe.CollectionId);

                    shoe.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
