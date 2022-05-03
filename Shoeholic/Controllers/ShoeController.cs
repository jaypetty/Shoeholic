using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoeholic.Repositories;
using Shoeholic.Models;
using System.Collections.Generic;

namespace Shoeholic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoeController : ControllerBase
    {
        private readonly IShoeRepository _shoeRepository;

        public ShoeController(IShoeRepository shoeRepository)
        {
            _shoeRepository = shoeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Shoe> shoes = _shoeRepository.GetAllShoes();
            return Ok(shoes);
        }

        [HttpPost]
        public IActionResult Post(Shoe shoe)
        {
            _shoeRepository.Add(shoe);

            return CreatedAtAction("Get", new { id = shoe.Id }, shoe);
        }
    }
}
