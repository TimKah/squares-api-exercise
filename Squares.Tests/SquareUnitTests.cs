using System;
using System.Collections.Generic;
using NUnit.Framework;
using Squares.DTO.Memory;
using Squares.Models;
using Squares.Services;

namespace Squares.Tests;

public class SquareUnitTests
{
    private PointsRepository _repository;
    private SquareIdentifierService _service;
    private readonly string _user = "user";
    
    [SetUp]
    public void Setup()
    {
        _repository = new PointsRepository();
        _service = new SquareIdentifierService(_repository);
        _service.DeleteAll(_user);
    }

    [Test]
    public void Test_AddAndDeletePoint()
    {
        var p = new Point() {X = 2, Y = 2};
        
        _service.Add(_user, p);
        Assert.True(_service.GetAll(_user).Contains(p));
        
        _service.Delete(_user, p);
        Assert.False(_service.GetAll(_user).Contains(p));
    }

    [Test]
    public void Test_CheckShape()
    {
        _service.AddAll(user: _user, points: new List<Point>()
        {
            new Point() {X = -1, Y = -1},
            new Point() {X = 1, Y = -1},
            new Point() {X = -1, Y = 1},
            new Point() {X = 1, Y = 1},
            new Point() {X = 0, Y = 1},
            new Point() {X = 0, Y = 0},
        });
        
        Assert.Throws<InvalidOperationException>(() => _service.IsHollowShape(_user));
        Assert.True(_service.IsSolidShape(_user));
        
        _service.DeleteAll(_user);
    }
}