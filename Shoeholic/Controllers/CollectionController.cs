using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoeholic.Repositories;
using Shoeholic.Models;
using System.Collections.Generic;

namespace Shoeholic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public CollectionController(ICollectionRepository collectionRepository, IUserProfileRepository userProfileRepository)
        {
            _collectionRepository = collectionRepository;
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet("GetAllUserCollections/{id}")]
        public IActionResult GetAllUserCollections(int id)
        {
            return Ok(_collectionRepository.GetAllUserCollections(id));
        }
    }
}
