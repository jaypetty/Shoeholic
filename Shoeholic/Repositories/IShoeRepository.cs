﻿using Shoeholic.Models;
using System.Collections.Generic;

namespace Shoeholic.Repositories
{
    public interface IShoeRepository
    {
        void Add(Shoe shoe);
        List<Shoe> GetAllShoes();
    }
}