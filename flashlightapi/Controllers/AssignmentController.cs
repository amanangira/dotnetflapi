using AutoMapper;
using flashlightapi.Data;
using flashlightapi.DTOs.assignment;
using flashlightapi.DTOs.common;
using flashlightapi.Mappers;
using flashlightapi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queryable = flashlightapi.DTOs.common.Queryable;

namespace flashlightapi.Controllers;


[Authorize]
[ApiController]
[Route("/api/assignment")]

public class AssignmentController(IAssignmentRepository assignmentRepository, IMapper mapper): ControllerBase
{
    private readonly IAssignmentRepository _assignmentRepository = assignmentRepository;
    
    private readonly IMapper _mapper = mapper;

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(Guid.Parse(id));

        return Ok(_mapper.Map<AssignmentDTO>(assignment));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AssignmentCreateDTO assignmentCreateDto)
    {
        var assignmentModel = assignmentCreateDto.ToAssignmentModel();
        await _assignmentRepository.CreateAsync(assignmentModel);

        return CreatedAtAction(nameof(Get), new { id = assignmentModel.Id }, _mapper.Map<AssignmentDTO>(assignmentModel));
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] Queryable query)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var assignments = await _assignmentRepository.ListAsync(query);

        return (assignments == null)
            ? NotFound()
            : Ok(_mapper.Map<List<AssignmentDTO>>(assignments));
    }
}