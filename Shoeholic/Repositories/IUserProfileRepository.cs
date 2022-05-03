using System.Collections.Generic;
using Shoeholic.Models;

namespace Shoeholic.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAll();
        void Add(UserProfile userProfile);
        UserProfile GetByFirebaseUserId(string firebaseUserId);
    }
}
