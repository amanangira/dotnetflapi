using AutoMapper;
using flashlightapi.DTOs.assignment;
using flashlightapi.Mappers;
using flashlightapi.Models;
using flashlightapi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Queryable = flashlightapi.DTOs.common.Queryable;

namespace flashlightapi.Controllers;


[Authorize]
[ApiController]
[Route("/api/assignment")]

public class AssignmentController: ControllerBase
{
    private readonly UserManager<AppUser> _manager;
    
    private readonly IAssignmentRepository _assignmentRepository;
    
    private readonly IMapper _mapper;

    public AssignmentController(UserManager<AppUser> manager,
        IAssignmentRepository assignmentRepository, 
        IMapper mapper)
    {
        _manager = manager;
        _assignmentRepository = assignmentRepository;
        _mapper = mapper;
        ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext()
        };
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var assignment = await _assignmentRepository.GetByIdAsync(Guid.Parse(id));

        return assignment != null ? Ok(_mapper.Map<AssignmentDTO>(assignment)) : NotFound();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAssignmentDTO createAssignmentDto)
    {
        var assignmentModel = createAssignmentDto.ToAssignmentModel();
        var user = await _manager.GetUserAsync(HttpContext.User);
        assignmentModel.CreatedById = user.Id;
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