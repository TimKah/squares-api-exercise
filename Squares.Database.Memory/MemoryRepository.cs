using System.Collections.Concurrent;
using Squares.Database.Memory.Exceptions;
using Squares.Models;

namespace Squares.Database.Memory;

public class MemoryRepository : IPointsRepository
{
    private readonly ConcurrentDictionary<string, List<Point>> _points;

    public MemoryRepository()
    {
        _points = new ConcurrentDictionary<string, List<Point>>();
    }

    public Task Add(string user, Point point)
    {
        if (!_points.ContainsKey(user))
        {
            _points[user] = new List<Point>() {point};
        }
        else
        {
            if (!_points[user].Contains(point))
            {
                _points[user].Add(point);
            }
        }
        
        return Task.CompletedTask;
    }

    public Task AddAll(string user, ICollection<Point> points)
    {
        _points[user] = new List<Point>(points);
        
        return Task.CompletedTask;
    }

    public Task Delete(string user, Point point)
    {
        if (!_points.ContainsKey(user) && !_points[user].Contains(point))
        {
            throw new NoSuchUserException();
        }
        else
        {
            _points[user].Remove(point);
        }
        
        return Task.CompletedTask;
    }

    public Task DeleteAll(string user)
    {
        _points.TryRemove(user, out var ignore);

        return Task.CompletedTask;
    }

    public Task<List<Point>> GetAll(string user)
    {
        if (!_points.ContainsKey(user))
        {
            throw new NoSuchUserException();
        }
        else
        {
            return Task.FromResult(_points[user]);
        }
    }
}