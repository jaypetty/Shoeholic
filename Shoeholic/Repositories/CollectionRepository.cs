using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Shoeholic.Models;
using Shoeholic.Utils;
using Microsoft.Data.SqlClient;


namespace Shoeholic.Repositories
{
    public class CollectionRepository : BaseRepository, ICollectionRepository
    {
        public CollectionRepository(IConfiguration configuration) : base(configuration) { }

        public List<Collection> GetUserCollectionByUserId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.Id, c.[Name], c.UserProfileId,
                                               u.FirstName
                                       FROM Collection c
                                       LEFT JOIN UserProfile u ON c.UserProfileId = u.Id
                                       WHERE c.UserProfileId = @userProfileId";

                    DbUtils.AddParameter(cmd, "@userProfileId", id);

                   using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Collection> collections = new List<Collection>();
                        while (reader.Read())
                        {
                            collections.Add(new Collection()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "UserProfileId"),
                                    FirstName = DbUtils.GetString(reader, "FirstName")
                                }
                                
                            });
                        }
                        reader.Close();
                        return collections;
                    }

                }
            }

        }

        
    }
}
