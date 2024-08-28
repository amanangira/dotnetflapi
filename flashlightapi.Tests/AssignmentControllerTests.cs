using System.Security.Claims;
using AutoMapper;
using flashlightapi.Controllers;
using flashlightapi.DTOs.assignment;
using flashlightapi.Mappers;
using flashlightapi.Models;
using flashlightapi.Repository;
using Microsoft.AspNetCore.Http;
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


    [Fact]
    public async Task GetWithValidIdReturnsOk()
    {
        // setup 
        Guid id = System.Guid.NewGuid();
        var assignment = new Assignment()
        {
            Id = id,
            Title = "Assignment 1",
            CreatedById = System.Guid.NewGuid().ToString(),
        };
        var assignmentDTO = assignment.ToAssignmentDTO();

        _assignmentRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(assignment);
        _mapperMock.Setup(mapper => mapper.Map<AssignmentDTO>(assignment)).Returns(assignmentDTO);

        // act
        var result = await _controller.Get(id.ToString());

        // assert
        result.ShouldBeOfType<OkObjectResult>();
        var resultObj = (OkObjectResult)result;
        resultObj.Value.ShouldBe(assignmentDTO);
        
        _assignmentRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<AssignmentDTO>(assignment), Times.Once);
    }

    [Fact]
    public async Task GetWithInvalidIdReturnsNotFound()
    {
        // setup 
        Guid id = System.Guid.NewGuid();
        var assignment = new Assignment()
        {
            Id = id,
            Title = "Assignment 1",
            CreatedById = System.Guid.NewGuid().ToString(),
        };

        _assignmentRepositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(value: null);

        // act
        var result = await _controller.Get(id.ToString());

        // assert
        result.ShouldBeOfType<NotFoundResult>();
        
        _assignmentRepositoryMock.Verify(repo => repo.GetByIdAsync(id), Times.Once);
    }

    [Fact]
    public async Task CreateWithValidInputsReturnsOk()
    {
        // arrange
        var input = new CreateAssignmentDTO()
        {
            Title = "Assignment One",
            StartAt = DateTime.Today,
            CloseAt = DateTime.Today.AddDays(30),
        };

        var inputModel = input.ToAssignmentModel();
        var outputModel = inputModel;
        var userId = Guid.NewGuid();
        outputModel.Id = It.IsAny<Guid>();
        outputModel.CreatedById = userId.ToString();
        var expectedResultDto = outputModel.ToAssignmentDTO(); 
        
        _manager.Setup(manager => manager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(new AppUser(){Id = userId.ToString()});
        _assignmentRepositoryMock.Setup(repo => repo.CreateAsync(inputModel)).ReturnsAsync(outputModel);
        _mapperMock.Setup(mapper => mapper.Map<AssignmentDTO>(outputModel)).Returns(expectedResultDto);
        
        // act 
        var result = await _controller.Create(input);


        // assert
        result.ShouldBeOfType<CreatedAtActionResult>();
        var resultObj = (CreatedAtActionResult)result;
        resultObj.Value.ShouldBe(expectedResultDto);
        
        _manager.Verify( manager => manager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);
        _assignmentRepositoryMock.Verify( repo => repo.CreateAsync(inputModel), Times.Once);
        _mapperMock.Verify(mapper => mapper.Map<AssignmentDTO>(outputModel), Times.Once);
    }
}