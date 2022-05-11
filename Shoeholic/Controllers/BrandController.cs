using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shoeholic.Repositories;
using Shoeholic.Models;
using System.Collections.Generic;

namespace Shoeholic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepository;

        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Brand> brands = _brandRepository.GetAllBrands();
            return Ok(brands);
        }
    }
}
