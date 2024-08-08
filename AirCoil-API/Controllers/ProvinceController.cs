using AirCoil_API.Data;
using AirCoil_API.Dto;
using AirCoil_API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirCoil_API.Controllers
{
    [Route("api/provinces")]
    [ApiController]
    public class ProvinceController : Controller
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IMapper _mapper;

        public ProvinceController(IProvinceRepository provinceRepository, IMapper mapper)
        {
            _provinceRepository = provinceRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProvinceDto>))]
        public IActionResult GetProvinces()
        {
            var provinces = _mapper.Map<List<ProvinceDto>>(_provinceRepository.GetProvices());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(provinces);
        }
    }
}
