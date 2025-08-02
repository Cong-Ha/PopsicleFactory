using System.Net.Mime;
using API.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PopsicleController : ControllerBase
{
    private readonly ILogger<PopsicleController> _logger;
    private readonly IPopsicleService _popsicleService;

    public PopsicleController(ILogger<PopsicleController> logger,  IPopsicleService popsicleService)
    {
        _logger = logger;
        _popsicleService = popsicleService;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PopsicleViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<PopsicleViewModel>> GetPopsicle(int id)
    {
        try
        {
            PopsicleViewModel? popsicle = await _popsicleService.GetPopsicleAsync(id);
            
            //popsicle does not exist
            if (popsicle == null)
            {
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Popsicle not found",
                    Detail = $"No popsicle with this Id: {id} exists.",
                    Instance = HttpContext.Request.Path
                });
            }
            return Ok(popsicle);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = $"An error occurred while retrieving the popsicle: {e.Message}",
                Instance = HttpContext.Request.Path
            });
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(PopsicleViewModel), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<PopsicleViewModel>> CreatePopsicle([FromBody] PopsicleCreateModel model)
    {
        //if request is invalid
        if (!ModelState.IsValid)
        {
            return BadRequest(new ValidationProblemDetails(ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid Request",
                Detail = "The request contains validation errors.",
                Instance = HttpContext.Request.Path
            });
        }
        try
        {
            PopsicleViewModel newPopsicle = await _popsicleService.CreatePopsicleAsync(model);
            return CreatedAtAction("CreatePopsicle", newPopsicle);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = $"An error occurred while retrieving the popsicle: {e.Message}",
                Instance = HttpContext.Request.Path
            });
        }
    }
}
