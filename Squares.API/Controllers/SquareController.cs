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
        [Route("/IsSolidShape")]
        public bool CheckIfSolidShape(string user)
        {
            return _shapeIdentifierService.IsSolidShape(user);
        }
        
        [HttpGet]
        [Route("/IsHollowShape")]
        public bool CheckIfHollowShape(string user)
        {
            return _shapeIdentifierService.IsHollowShape(user);
        }
    }
}