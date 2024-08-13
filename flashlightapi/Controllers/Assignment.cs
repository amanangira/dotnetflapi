using AutoMapper;
using flashlightapi.Data;
using flashlightapi.DTOs.assignment;
using flashlightapi.Mappers;
using flashlightapi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace flashlightapi.Controllers;


[Route("/api/assignment")]
[ApiController]

public class Assignment(IAssignmentRepository assignmentRepository, IMapper mapper): ControllerBase
{
    private IAssignmentRepository _assignmentRepository = assignmentRepository;
    
    private IMapper _mapper = mapper;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(Guid.Parse(id));

        return Ok(mapper.Map<AssignmentDTO>(assignment));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AssignmentCreateDTO assignmentCreateDto)
    {
        var assignmentModel = assignmentCreateDto.ToAssignmentModel();
        await _assignmentRepository.CreateAsync(assignmentModel);

        return CreatedAtAction(nameof(Get), new { id = assignmentModel.Id }, _mapper.Map<AssignmentDTO>(assignmentModel));
    }
}