using AutoMapper;
using flashlightapi.Controllers;
using flashlightapi.Repository;
using Moq;
using Shouldly;

namespace flashlightapi.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // arrange
        var assignmentRepo = new Mock<IAssignmentRepository>();
        var mapper = new Mock<IMapper>();
        var assignmentController = new AssignmentController(assignmentRepo.Object, mapper.Object);
        
        // act
        var result = assignmentController.FooFuncForTesting(true);
    
        // validate 
        result.ShouldBe(false);
    }
}