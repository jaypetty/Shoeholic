using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoeholic.Repositories;
using Shoeholic.Models;
using System.Collections.Generic;
using System.Security.Claims;

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

        [HttpGet("UserCollectionByUser")]
        public IActionResult UserCollectionByUser(int id)
        {
            UserProfile user = GetCurrentUserProfile();
            id = user.Id;
            return Ok(_collectionRepository.GetUserCollectionByUserId(id));
        }

        [HttpPost]
        public IActionResult Post(Collection collection)
        {
            collection.UserProfileId = GetCurrentUserProfile().Id;

            _collectionRepository.AddCollection(collection);

            return CreatedAtAction("Get", new {id = collection.Id}, collection);
        }

        private UserProfile GetCurrentUserProfile()
        {
            var firebaseUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userProfileRepository.GetByFirebaseUserId(firebaseUserId);
        }
    }
}
