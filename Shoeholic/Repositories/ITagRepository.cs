using Shoeholic.Models;
using System.Collections.Generic;

namespace Shoeholic.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAll();
    }
}