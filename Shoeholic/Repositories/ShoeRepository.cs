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
                        SELECT s.Id, s.[Name], s.BrandId, s.ReleaseDate, s.Title, s.ColorWay,
                               b.[Name] AS BrandName, t.Id AS TagId, t.[Name] AS TagName
                        FROM Shoe s
                            JOIN Brand b ON b.Id = s.BrandId
                            LEFT JOIN ShoeTag st ON st.ShoeId = s.Id
                            LEFT JOIN Tag t ON st.TagId = t.Id";

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
                                ColorWay = DbUtils.GetString(reader, "ColorWay"),
                                Brand = new Brand()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Name = DbUtils.GetString(reader, "BrandName")
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
                    DbUtils.AddParameter(cmd, "@colorWay", shoe.ColorWay);
                    DbUtils.AddParameter(cmd, "@collectionId", shoe.CollectionId);

                    shoe.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public Shoe GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT s.Id, s.Name, s.BrandId, s.ReleaseDate, s.RetailPrice, s.PurchaseDate, s.Title, s.ColorWay, s.CollectionId,
                        b.[Name] AS BrandName, c.[Name] AS CollectionName, t.Id AS TagId, t.[Name] AS TagName
                        FROM Shoe s
                            JOIN Collection c ON s.CollectionId = c.id
                        JOIN Brand b ON s.BrandId = b.id
                            LEFT JOIN ShoeTag st ON st.ShoeId = s.Id
                            LEFT JOIN Tag t ON st.TagId = t.Id
                        WHERE s.Id = @id";

                    DbUtils.AddParameter(cmd, "@id", id);

                    var reader = cmd.ExecuteReader();

                    Shoe shoe = null;
                    while (reader.Read())
                    {
                        shoe = NewShoeFromReader(reader);
                        while (reader.Read())
                        {
                            shoe.Tags.Add(new Tag
                            {
                                Id = DbUtils.GetInt(reader, "TagId"),
                                Name = DbUtils.GetString(reader, "TagName")
                            });
                        };
                    }
                    reader.Close();
                    return shoe;

                }
            }
        }

        public void Update(Shoe shoe)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Shoe
                                        SET Name = @name,
                                            BrandId = @brandId,
                                            ReleaseDate = @releaseDate,
                                            RetailPrice = @retailPrice,
                                            PurchaseDate = @purchaseDate,
                                            Title = @title,
                                            ColorWay = @colorWay,
                                            CollectionId = @collectionId
                                        WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", shoe.Id);
                    DbUtils.AddParameter(cmd, "@name", shoe.Name);
                    DbUtils.AddParameter(cmd, "@brandId", shoe.BrandId);
                    DbUtils.AddParameter(cmd, "@releaseDate", shoe.ReleaseDate);
                    DbUtils.AddParameter(cmd, "@retailPrice", shoe.RetailPrice);
                    DbUtils.AddParameter(cmd, "@purchaseDate", shoe.PurchaseDate);
                    DbUtils.AddParameter(cmd, "@title", shoe.Title);
                    DbUtils.AddParameter(cmd, "@colorWay", shoe.ColorWay);
                    DbUtils.AddParameter(cmd, "@collectionId", shoe.CollectionId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Shoe> GetAllShoesByCollectionId(int collectionId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT s.Id, s.[Name], s.BrandId, s.Title, s.CollectionId,
                                               b.[Name] AS BrandName,
                                               c.[Name] AS CollectionName
                                        FROM Shoe s
                                            LEFT JOIN Brand b ON s.BrandId = b.Id
                                            LEFT JOIN Collection c ON s.CollectionId = c.Id

                                        WHERE s.CollectionId = @collectionId";

                    DbUtils.AddParameter(cmd, "@collectionid", collectionId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Shoe> shoes = new List<Shoe>();
                        while (reader.Read())
                        {
                            Shoe shoe = new Shoe
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                Title = DbUtils.GetString(reader, "Title"),
                                BrandId = DbUtils.GetInt(reader, "BrandId"),
                                CollectionId = DbUtils.GetInt(reader, "CollectionId"),
                                Brand = new Brand
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Name = DbUtils.GetString(reader, "BrandName"),
                                },
                                Collection = new Collection
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Name = DbUtils.GetString(reader, "CollectionName"),
                                }
                            };
                            shoes.Add(shoe);
                        }
                        return shoes;
                    }
                }
            }
        }
        public List<Tag> GetTagsByShoeId(int shoeId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT t.Id, t.[Name]
                                       FROM ShoeTag
                                         LEFT JOIN Tag t ON t.Id = ShoeTag.TagId
                                       WHERE ShoeTag.Shoe.Id = @shoeId";

                    DbUtils.AddParameter(cmd, "@shoeId", shoeId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Tag> tags = new List<Tag>();
                        while(reader.Read())
                        {
                            Tag tag = new Tag
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                            };
                            tags.Add(tag);
                        }
                        reader.Close();
                        return tags;
                    }
                }
            }
        }

        private Shoe NewShoeFromReader(SqlDataReader reader)
        {
            Shoe shoe = new Shoe()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Title = DbUtils.GetString(reader, "Title"),
                Name = DbUtils.GetString(reader, "Name"),
                ReleaseDate = DbUtils.GetDateTime(reader, "ReleaseDate"),
                PurchaseDate = DbUtils.GetDateTime(reader, "PurchaseDate"),
                RetailPrice = DbUtils.GetInt(reader, "RetailPrice"),
                ColorWay = DbUtils.GetString(reader, "ColorWay"),
                BrandId = DbUtils.GetInt(reader, "BrandId"),
                Brand = new Brand()
                {
                    Id = DbUtils.GetInt(reader, "Id"),
                    Name = DbUtils.GetString(reader, "BrandName")
                },
                CollectionId = DbUtils.GetInt(reader, "Id"),
                Collection = new Collection()
                {
                    Id = DbUtils.GetInt(reader, "Id"),
                    Name = DbUtils.GetString(reader, "CollectionName"),

                },
                Tags = new List<Tag>()
            };
            if (DbUtils.IsNotDbNull(reader, "TagId"))
            {
                shoe.Tags.Add(new Tag()
                {
                    Id = DbUtils.GetInt(reader, "TagId"),
                    Name = DbUtils.GetString(reader, "TagName")
                });
            }
            return shoe;
        }

        public void AddShoeTag(int tagId, int shoeId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [ShoeTag] (ShoeId, TagId)
                                        VALUES (@shoeId, @tagId)";

                    
                       

                        DbUtils.AddParameter(cmd, "@shoeId", shoeId);
                        DbUtils.AddParameter(cmd, "@tagId", tagId);

                    
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
    

