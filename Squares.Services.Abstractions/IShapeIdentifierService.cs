using Squares.Models;

namespace Squares.Services.Abstractions;

public interface IShapeIdentifierService
{
    void Add(string user, Point point);
    void AddAll(string user, ICollection<Point> points);
    void Delete(string user, Point point);
    void DeleteAll(string user);
    ICollection<Point> GetAll(string user);
    bool IsSolidShape(string user);
    bool IsHollowShape(string user);
}