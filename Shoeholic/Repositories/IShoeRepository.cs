using Shoeholic.Models;
using System.Collections.Generic;

namespace Shoeholic.Repositories
{
    public interface IShoeRepository
    {
        void Add(Shoe shoe);
        List<Shoe> GetAllShoes();
        Shoe GetById(int id);
        void Update(Shoe shoe);
        List<Shoe> GetAllShoesByCollectionId(int collectionId);
        public List<Tag> GetTagsByShoeId(int shoeId);
        void Delete(int id);
        void AddShoeTag(int tagId, int shoeId);
    }
}