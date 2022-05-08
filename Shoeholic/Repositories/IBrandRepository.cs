using Shoeholic.Models;
using System.Collections.Generic;

namespace Shoeholic.Repositories
{
    public interface IBrandRepository
    {
        List<Brand> GetAllBrands();
    }
}