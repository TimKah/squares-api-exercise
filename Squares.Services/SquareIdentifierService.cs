using Squares.Services.Abstractions;
using Squares.Database;
using Squares.Models;

namespace Squares.Services;

public class SquareIdentifierService : IShapeIdentifierService
{
    private readonly IPointsRepository _repository;
    
    public SquareIdentifierService(IPointsRepository repository)
    {
        _repository = repository;
    }

    public void Add(string user, Point point)
    {
        _repository.Add(user, point);
    }

    public void AddAll(string user, ICollection<Point> points)
    {
        _repository.AddAll(user, points);
    }

    public void Delete(string user, Point point)
    {
        _repository.Delete(user, point);
    }
    
    public void DeleteAll(string user)
    {
        _repository.DeleteAll(user);
    }

    public ICollection<Point> GetAll(string user)
    {
        return _repository.GetAll(user).Result;
    }

    public bool IsSolidShape(string user)
    {
        var points = _repository.GetAll(user).Result;

        var corners = GetCorners(points);
        
        foreach (var p in points)
        {
            if (IsToLeft(corners[0], corners[1], p)) throw new InvalidOperationException();
            if (IsToLeft(corners[1], corners[3], p)) throw new InvalidOperationException();
            if (IsToLeft(corners[2], corners[0], p)) throw new InvalidOperationException();
            if (IsToLeft(corners[3], corners[2], p)) throw new InvalidOperationException();
        }
        
        var sides = new long[3];

        sides[0] = (long) DistancePower2(corners[0], corners[1]);
        sides[1] = (long) DistancePower2(corners[0], corners[2]);

        if (sides[0] == sides[1])
        {
            sides[2] = (long) DistancePower2(corners[3], corners[1]);
        }
        else
        {
            sides[2] = (long) DistancePower2(corners[0], corners[3]);
            if (sides[0] == sides[2])
            {
                sides[1] = (long) DistancePower2(corners[2], corners[1]);
            }
            else
            {
                sides[0] = (long) DistancePower2(corners[1], corners[2]);
            }
        }

        return sides[0] == sides[1] && sides[1] == sides[2];
    }

    public bool IsHollowShape(string user)
    {
        var points = _repository.GetAll(user).Result;

        var corners = GetCorners(points);

        foreach (var p in points)
        {
            if (IsOnLine(corners[0], corners[1], p)) continue;
            if (IsOnLine(corners[1], corners[3], p)) continue;
            if (IsOnLine(corners[2], corners[0], p)) continue;
            if (IsOnLine(corners[3], corners[2], p)) continue;
            throw new InvalidOperationException();
        }

        return true;
    }

    private Point[] GetCorners(List<Point> points)
    {

        if (points.Count < 4)
        {
            throw new InvalidOperationException();
        }

        var nCorner = new Point();
        var eCorner = new Point();
        var wCorner = new Point();
        var sCorner = new Point();
        
        foreach (var p in points)
        {
            if (p.Y > nCorner.Y) nCorner = p;
            if (p.X > eCorner.X) eCorner = p;
            if (p.X < wCorner.X) wCorner = p;
            if (p.Y < sCorner.Y) sCorner = p;
        }

        var nPoints = points.Count(p => p.Y == nCorner.Y);
        var ePoints = points.Count(p => p.X == eCorner.X);
        var wPoints = points.Count(p => p.X == wCorner.X);
        var sPoints = points.Count(p => p.Y == sCorner.Y);

        // Double check to be sure that those points exist
        if (nPoints == 0 || sPoints == 0 || ePoints == 0 || wPoints == 0) 
        {
            throw new InvalidOperationException();
        }
        
        // Check if opposite sides have 1 or more points at the end
        if ((nPoints == 1 && nPoints != sPoints) ||
            (ePoints == 1 && ePoints != wPoints) ||
            (wPoints == 1 && wPoints != ePoints) ||
            (sPoints == 1 && sPoints != nPoints))
        {
            throw new InvalidOperationException();
        }

        // Correct points
        if (nPoints >= 2)
        {
            nCorner.X = eCorner.X;
            eCorner.Y = sCorner.Y;
            wCorner.Y = nCorner.Y;
            sCorner.X = wCorner.X;

            if (!points.Contains(nCorner)) throw new InvalidOperationException();
            if (!points.Contains(eCorner)) throw new InvalidOperationException();
            if (!points.Contains(wCorner)) throw new InvalidOperationException();
            if (!points.Contains(sCorner)) throw new InvalidOperationException();
        }

        return new[] {nCorner, eCorner, wCorner, sCorner};
    }

    private bool IsToLeft(Point a, Point b, Point c)
    {
        var d = (c.X - a.X) * (b.Y - a.Y) - (c.Y - a.Y) * (b.X - a.X);
        return d < 0;
    }
    
    private bool IsOnLine(Point a, Point b, Point c)
    {
        var d = (c.X - a.X) * (b.Y - a.Y) - (c.Y - a.Y) * (b.X - a.X);
        return d == 0;
    }
    
    private double DistancePower2(Point from, Point to)
    {
        return Math.Pow(from.X - to.X, 2) + Math.Pow(from.Y - to.Y, 2);
    }
}