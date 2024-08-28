using AutoMapper;
using flashlightapi.Controllers;
using flashlightapi.DTOs.assignment;
using flashlightapi.Models;
using flashlightapi.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using Queryable = flashlightapi.DTOs.common.Queryable;

namespace flashlightapi.Tests;

public class AssignmentControllerTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly AssignmentController _controller;
    private readonly Mock<UserManager<AppUser>> _manager;
    private readonly Mock<IAssignmentRepository> _assignmentRepositoryMock;

    public AssignmentControllerTests()
    {
        _mapperMock = new Mock<IMapper>();
        _manager = new Mock<UserManager<AppUser>>((Mock.Of<IUserStore<AppUser>>()), null, null, null, null, null, null, null, null);
        _assignmentRepositoryMock = new Mock<IAssignmentRepository>();
        _controller = new AssignmentController(_manager.Object, _assignmentRepositoryMock.Object, _mapperMock.Object);
    }
    
    [Fact]
    public async Task ListWithValidQueryReturnsOkResult()
    {
        // Arrange
        Guid id1, id2 ;
        id1 = System.Guid.NewGuid();
        id2 = System.Guid.NewGuid();
        var query = new Queryable { Page = 1, PageSize = 10 };
        var assignments = new List<Assignment>
        {
            new Assignment { Id = id1, Title = "Assignment 1"},
            new Assignment { Id = id2, Title = "Assignment 2" }
        };
        var assignmentDTOs = new List<AssignmentDTO>
        {
            new AssignmentDTO { Id = id1, Title = "Assignment 1" },
            new AssignmentDTO { Id = id2, Title = "Assignment 2" }
        };

        _assignmentRepositoryMock.Setup(repo => repo.ListAsync(query))
            .ReturnsAsync(assignments);

        _mapperMock.Setup(mapper => mapper.Map<List<AssignmentDTO>>(assignments))
            .Returns(assignmentDTOs);

        // Act
        var result = await _controller.List(query);

        // Assert
        result.ShouldBeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result;
        okResult.Value.ShouldBe(assignmentDTOs);
        
        // Verify that the repository method was called once with the correct parameters
        _assignmentRepositoryMock.Verify(repo => repo.ListAsync(query), Times.Once);
        
        // Verify that the mapper method was called once with the correct parameters
        _mapperMock.Verify(mapper => mapper.Map<List<AssignmentDTO>>(assignments), Times.Once);
    }

    [Fact]
    public async Task ListWithValidQueryReturnsNotFound()
    {
        // setup 
        Queryable query = new Queryable() { Page = 2, PageSize = 10};
        _assignmentRepositoryMock.Setup(repo => repo.ListAsync(query)).ReturnsAsync(value: null);
    
        
        // act
        var result = await _controller.List(query);
        
        // assert
        result.ShouldBeOfType<NotFoundResult>();
        _assignmentRepositoryMock.Verify(repo => repo.ListAsync(query), Times.Once);
    }
}