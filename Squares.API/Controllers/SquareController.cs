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
    public class SquareController
    {
        private readonly ILogger<SquareController> _logger;
        private readonly IShapeIdentifierService _shapeIdentifierService;

        public SquareController(IShapeIdentifierService shapeIdentifierService, ILogger<SquareController> logger)
        {
            _logger = logger;
            _shapeIdentifierService = shapeIdentifierService;
        }
        
        [HttpGet]
        [Route("/checkIfDotsMakeSolidShape")]
        public bool CheckIfSolidShape(string user)
        {
            return _shapeIdentifierService.IsSolidShape(user);
        }
        
        [HttpGet]
        [Route("/checkIfDotsMakeHollowShape")]
        public bool CheckIfHollowShape(string user)
        {
            return _shapeIdentifierService.IsHollowShape(user);
        }
        
        [HttpGet]
        [Route("/getDots")]
        public ICollection<PointDTO> GetDots(string user)
        {
            return _shapeIdentifierService.GetAll(user).Adapt<ICollection<PointDTO>>();
        }

        [HttpPut]
        [Route("/addPoint")]
        public void Add(string user, PointDTO point)
        {
            _shapeIdentifierService.Add(user, point.Adapt<Point>());
        }

        [HttpPut]
        [Route("/addAllPoints")]
        public void AddAll(string user, IList<PointDTO> points)
        {
            _shapeIdentifierService.AddAll(user, points.Adapt<IList<Point>>());
        }

        [HttpDelete]
        [Route("/deletePoint")]
        public void Delete(string user, PointDTO point)
        {
            _shapeIdentifierService.Delete(user, point.Adapt<Point>());
        }

        [HttpDelete]
        [Route("/deleteAllPoints")]
        public void Delete(string user)
        {
            _shapeIdentifierService.DeleteAll(user);
        }
    }
}