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

        public List<Collection> GetAllUserCollections(int UPID)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.Id, c.Name, c.UserProfileId, up.FirstName, up.LastName
                                       FROM Collection c
                                       LEFT JOIN UserProfile up ON c.UserProfileId = up.Id
                                       WHERE c.UserProfileId = UPID";

                    DbUtils.AddParameter(cmd, "@UPID", UPID);

                    var reader = cmd.ExecuteReader();

                    var collections = new List<Collection>();
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
                                FirstName = DbUtils.GetString(reader, "FirstName"),
                                LastName = DbUtils.GetString(reader, "LastName")
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
