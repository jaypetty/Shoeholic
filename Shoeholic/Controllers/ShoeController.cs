using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoeholic.Repositories;
using Shoeholic.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.Linq;

namespace Shoeholic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoeController : ControllerBase
    {
        private readonly IShoeRepository _shoeRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public ShoeController(IShoeRepository shoeRepository, IUserProfileRepository userProfileRepository)
        {
            _shoeRepository = shoeRepository;
            _userProfileRepository = userProfileRepository;
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
            

            return Ok(shoe);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var shoe = _shoeRepository.GetById(id);
            if (shoe == null)
            {
                return NotFound();
            }
            return Ok(shoe);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Shoe shoe)
        {
            _shoeRepository.Update(shoe);
            return Ok(shoe);
        }

        [HttpGet("GetWithCollections/{id}")]
        public IActionResult GetWithCollections(int id)
        {
            var shoes = _shoeRepository.GetAllShoesByCollectionId(id);
            return Ok(shoes);
        }

        [HttpGet("GetWithTags/{id}")]
        public IActionResult GetWithTags(int id)
        {
            var shoes = _shoeRepository.GetTagsByShoeId(id);
            return Ok(shoes);
        }

       

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }

    }
}
