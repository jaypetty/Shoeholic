using Shoeholic.Models;
using System.Collections.Generic;

namespace Shoeholic.Repositories
{
    public interface ICollectionRepository
    {
        List<Collection> GetUserCollectionByUserId(int id);
    }
}