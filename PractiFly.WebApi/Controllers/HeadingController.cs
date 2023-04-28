using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Materials;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Dto.Heading;

namespace PractiFly.WebApi.Controllers;

/// <summary>
/// Heading controller
/// </summary>
[Route("api/heading")]
[ApiController]
public class HeadingController : Controller
{
    private readonly IPractiflyContext _context;

    public HeadingController(IPractiflyContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get heading by heading udc or heading id
    /// </summary>
    /// <param name="headingId">heading id</param>
    /// <param name="code">Heading code (ex: 01, 01.01, 01.01.01, 01.01.01.01)</param>
    /// <returns></returns>
    /// <response code="200">Return heading info</response>
    /// <response code="404">Heading not found</response>
    /// <response code="400">Bad request (udc or headingId must have value)</response>
    [HttpGet]
    public async Task<IActionResult> Get(
        [RegularExpression(@"^\d{2}(?:\.\d{2}){0,3}$")]
        string? code = null,
        int? headingId = null
    )
    {
        var request = _context.Headings.AsNoTracking();

        if (code != null)
            request = request.Where(e => e.Code == code);
        else if (headingId != null)
            request = request.Where(e => e.Id == headingId);
        else
        {
            return BadRequest();
        }

        var headingInfo = await request
            .Select(e => new HeadingInfoDto()
            {
                Id = e.Id,
                Name = e.Name,
                Code = e.Code,
                Description = e.Description,
                Note = e.Note,
                Udc = e.Udc
            })
            .FirstOrDefaultAsync();

        if (headingInfo == null)
            return NotFound();

        return Json(headingInfo);
    }

    /// <summary>
    /// Create new heading
    /// </summary>
    /// <param name="headingDto"> A parameter containing fields for creating a heading </param>
    /// <returns></returns>
    /// <response code="200">Heading created and returned id</response>
    /// <response code="400">Bad request (error save)</response>
    [HttpPost]
    [Authorize(UserRoles.Admin)]
    public async Task<IActionResult> Create(HeadingEditDto headingDto)
    {
        var heading = new Heading()
        {
            Name = headingDto.Name,
            Code = headingDto.Code,
            Description = headingDto.Description,
            Note = headingDto.Note,
            Udc = headingDto.Udc
        };

        var result = await _context.Headings.AddAsync(heading);

        return heading.Id != 0 ? Ok(heading.Id) : BadRequest();
    }

    /// <summary>
    /// Edit heading
    /// </summary>
    /// <param name="dto"> Containing fields for edit a heading</param>
    /// <returns></returns>
    /// <response code="200">Heading edited</response>
    /// <response code="404">Heading not found</response>
    [HttpPost]
    [Route("edit")]
    [Authorize(UserRoles.Admin)]
    public async Task<IActionResult> Edit(HeadingEditDto dto)
    {
        var heading = await _context
            .Headings
            .FirstOrDefaultAsync(e => e.Id == dto.Id);

        if (heading == null)
            return NotFound();

        heading.Code = dto.Code;
        heading.Name = dto.Name;
        heading.Description = dto.Description;
        heading.Note = dto.Note;
        heading.Udc = dto.Udc;

        _context.Headings.Update(heading);

        await _context.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    /// Delete heading
    /// </summary>
    /// <param name="headingId"> Id of the rubric to delete </param>
    /// <response code="200">Heading deleted</response>
    /// <response code="404">Heading not found</response>
    [HttpDelete]
    [Authorize(UserRoles.Admin)]
    public async Task<IActionResult> Delete(int headingId)
    {
        var isAvaibleHeading = await _context
            .Materials
            .AnyAsync(e => e.Id == headingId);

        if (!isAvaibleHeading)
            return NotFound();

        _context.Headings.Remove(new Heading() { Id = headingId });

        await _context.SaveChangesAsync();

        return Ok();
    }
}