using System.Net.Mime;
using API.Models;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class PopsicleController(IPopsicleService popsicleService) : ControllerBase
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PopsicleViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<PopsicleViewModel>> GetPopsicle(int id)
    {
        try
        {
            PopsicleViewModel popsicle = await popsicleService.GetPopsicleAsync(id);
            return Ok(popsicle);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Popsicle not found",
                Detail = e.Message,
                Instance = HttpContext.Request.Path
            });
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

    [HttpPost("create")]
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
            PopsicleViewModel newPopsicle = await popsicleService.CreatePopsicleAsync(model);
            return CreatedAtAction("CreatePopsicle", newPopsicle);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new ValidationProblemDetails(ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid Request",
                Detail = e.Message,
                Instance = HttpContext.Request.Path
            });
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

    [HttpPut("replace{id}")]
    [ProducesResponseType(typeof(PopsicleViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<PopsicleViewModel>> ReplacePopsicle(int id, [FromBody] PopsicleReplaceModel model)
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
            PopsicleViewModel updatedPopsicle = await popsicleService.ReplacePopsicle(id, model);
            return Ok(updatedPopsicle);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Popsicle not found",
                Detail = e.Message,
                Instance = HttpContext.Request.Path
            });
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

    [HttpDelete("delete{id}")]
    [ProducesResponseType( StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeletePopsicle(int id)
    {
        try
        {
            await popsicleService.DeletePopsicleAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Popsicle not found",
                Detail = e.Message,
                Instance = HttpContext.Request.Path
            });
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

    [HttpPatch("update/{id}")]
    [ProducesResponseType(typeof(PopsicleViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<PopsicleViewModel>> UpdatePopsicle(int id, [FromBody] PopsicleUpdateModel model)
    {
        try
        {
            var updated = await popsicleService.UpdatePopsicleAsync(id, model);
            return Ok(updated);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid Patch Request",
                Detail = e.Message,
                Instance = HttpContext.Request.Path
            });
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Popsicle not found",
                Detail = $"No popsicle with this Id: {id} exists.",
                Instance = HttpContext.Request.Path
            });
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

    [HttpGet("search")]
    [ProducesResponseType(typeof(List<PopsicleViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<List<Popsicle>>> SearchPopsicles([FromQuery] PopsicleSearchModel query)
    {
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
            List<PopsicleViewModel> result = await popsicleService.SearchPopsicleAsync(query);
            if (result.Count == 0)
            {
                return Ok("No popsicles found given search criteria.");
            }
            return Ok(result);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Invalid Search Request",
                Detail = e.Message,
                Instance = HttpContext.Request.Path
            });
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
