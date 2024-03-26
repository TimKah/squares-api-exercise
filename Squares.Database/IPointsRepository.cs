using Squares.Models;

namespace Squares.Database;

public interface IPointsRepository
{
    Task Add(string user, Point point);
    Task AddAll(string user, ICollection<Point> points);
    Task Delete(string user, Point point);
    Task DeleteAll(string user);
    Task<List<Point>> GetAll(string user);
}