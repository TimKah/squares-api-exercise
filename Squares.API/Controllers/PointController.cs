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
    public class PointController
    {
        private readonly ILogger<PointController> _logger;
        private readonly IShapeIdentifierService _shapeIdentifierService;

        public PointController(IShapeIdentifierService shapeIdentifierService, ILogger<PointController> logger)
        {
            _logger = logger;
            _shapeIdentifierService = shapeIdentifierService;
        }

        [HttpPut]
        public void Add(string user, PointDTO point)
        {
            _shapeIdentifierService.Add(user, point.Adapt<Point>());
        }

        [HttpDelete]
        public void Delete(string user, PointDTO point)
        {
            _shapeIdentifierService.Delete(user, point.Adapt<Point>());
        }
    }
}