using Mapster;
using Microsoft.AspNetCore.Mvc;
using Squares.API.Filters;
using Squares.DTO;
using Squares.Models;
using Squares.Services.Abstractions;

namespace Squares.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [CustomExceptionFilter]
    public class PointsController
    {
        private readonly ILogger<PointsController> _logger;
        private readonly IShapeIdentifierService _shapeIdentifierService;

        public PointsController(IShapeIdentifierService shapeIdentifierService, ILogger<PointsController> logger)
        {
            _logger = logger;
            _shapeIdentifierService = shapeIdentifierService;
        }
        
        [HttpGet]
        public ICollection<PointDTO> GetDots(string user)
        {
            return _shapeIdentifierService.GetAll(user).Adapt<ICollection<PointDTO>>();
        }

        [HttpPut]
        public void AddAll(string user, IList<PointDTO> points)
        {
            _shapeIdentifierService.AddAll(user, points.Adapt<IList<Point>>());
        }

        [HttpDelete]
        public void Delete(string user)
        {
            _shapeIdentifierService.DeleteAll(user);
        }
    }
}