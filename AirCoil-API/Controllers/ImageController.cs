using AirCoil_API.Dto;
using AirCoil_API.Dto.Image;
using AirCoil_API.Interface;
using AirCoil_API.Models;
using AirCoil_API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AirCoil_API.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ImageController(IImageService imageService, IMapper mapper)
        {
            _imageService = imageService;
            _mapper = mapper;
        }

        [HttpGet("{imageId}")]
        [ProducesResponseType(200, Type = typeof(ImageUrlDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetImageUrl(int imageId)
        {
            var imageUrl = await _imageService.GetImageUrlAsync(imageId, Request);
            if (imageUrl == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new ImageUrlDto { Url = imageUrl });
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _imageService.CreateImageAsync(file) == null)
            {
                ModelState.AddModelError("", "Error occur while saving");
                return StatusCode(500, ModelState);
            }

            return Created();
        }

        [HttpDelete("{imageId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(405)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var imageToDelete = await _imageService.GetImageAsync(imageId);
            if (imageToDelete == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _imageService.DeleteImageAsync(imageToDelete))
            {
                ModelState.AddModelError("", "Error occur while deleting");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
