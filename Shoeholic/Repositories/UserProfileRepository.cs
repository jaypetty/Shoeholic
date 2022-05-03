using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Shoeholic.Models;
using Shoeholic.Utils;

namespace Shoeholic.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public UserProfile GetByFirebaseUserId(string firebaseUserProfileId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id,FirebaseUserProfileId, FirstName, LastName, Email 
                          FROM UserProfile 
                         WHERE FirebaseUserProfileId = @FirebaseuserprofileId";

                    DbUtils.AddParameter(cmd,"@FirebaseUserProfileId", firebaseUserProfileId);

                    UserProfile userProfile = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        userProfile = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseUserProfileId = DbUtils.GetString(reader, "FirebaseUserProfileId"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            Email = DbUtils.GetString(reader, "Email")
                            
                        };
                    }
                    reader.Close();

                    return userProfile;
                }
            }
        }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT up.Id AS upId,
                                               FirstName,
                                               LastName,
                                               Email,
                                               Name
                                          FROM UserProfile up
                                               JOIN UserType ut ON ut.Id = UserTypeId
                                         WHERE IsDeactivated = 0
                                         ORDER BY DisplayName
                                         ";
                    var reader = cmd.ExecuteReader();

                    var users = new List<UserProfile>();

                    while (reader.Read())
                    {
                        var user = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "upId"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            
                        };

                        users.Add(user);
                    }
                    return users;
                }
            }
        }

       
        public void Add(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO UserProfile (FirebaseUserId, FirstName, LastName, DisplayName, 
                                                                 Email, CreateDateTime, ImageLocation, UserTypeId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@FirebaseUserId, @FirstName, @LastName, @DisplayName, 
                                                @Email, @CreateDateTime, @ImageLocation, @UserTypeId)";
                    DbUtils.AddParameter(cmd, "@FirebaseUserId", userProfile.FirebaseUserProfileId);
                    DbUtils.AddParameter(cmd, "@FirstName", userProfile.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", userProfile.LastName);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                 

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        
        
    }
}
