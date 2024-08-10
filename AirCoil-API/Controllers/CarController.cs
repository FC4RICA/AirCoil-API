using AirCoil_API.Dto;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirCoil_API.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public CarController(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CarDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetCars()
        {
            var cars = _mapper.Map<List<CarDto>>(_carRepository.GetCars());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(cars);
        }

        [HttpGet("{carId}")]
        [ProducesResponseType(200, Type = typeof(CarDto))]
        [ProducesResponseType(400)]
        public IActionResult GetCar(int carId)
        {
            if (!_carRepository.CarExists(carId))
            {
                return NotFound();
            }

            var car = _mapper.Map<CarDto>(_carRepository.GetCar(carId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(car);
        }


    }
}
