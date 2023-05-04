using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities;
using PractiFly.DbEntities.Materials;
using PractiFly.WebApi.Dto.Heading;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.Controllers;

[Route("api/heading")]
[ApiController]
public class HeadingController : Controller
{
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IPractiflyContext _context;
    private readonly IMapper _mapper;

    public HeadingController(
        IPractiflyContext context,
        IConfigurationProvider configurationProvider,
        IMapper mapper
    )
    {
        _configurationProvider = configurationProvider;
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    ///     Get heading by heading udc or heading id.
    /// </summary>
    /// <param name="headingId">Id of the heading.</param>
    /// <param name="code">Heading code (ex: 01, 01.01, 01.01.01, 01.01.01.01)</param>
    /// <returns></returns>
    /// <response code="200">Return heading info</response>
    /// <response code="400">Bad request (udc or headingId must have value)</response>
    /// <response code="404">Heading not found</response>
    [HttpGet]
    public async Task<IActionResult> Get(
        [RegularExpression(EntitiesConstants.HeadingPattern)]
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
            return BadRequest();

        var headingInfo = await request
            .ProjectTo<HeadingInfoDto>(_configurationProvider)
            .FirstOrDefaultAsync();

        if (headingInfo == null)
            return NotFound();

        return Json(headingInfo);
    }

    /// <summary>
    ///     Creates a new heading using the provided heading data.
    /// </summary>
    /// <param name="headingDto"> A parameter containing fields for creating a heading </param>
    /// <returns></returns>
    /// <response code="200">Heading created and returned id</response>
    /// <response code="400">Bad request (error save)</response>
    [HttpPost]
    //[Authorize(UserRoles.Admin)]
    public async Task<IActionResult> Create(HeadingCreateDto headingDto)
    {
        if (await _context.Headings.AnyAsync(e => e.Code == headingDto.Code))
            return BadRequest(new { message = "Heading with this code already exists" });

        var heading = _mapper.Map<HeadingCreateDto, Heading>(headingDto);

        await _context.Headings.AddAsync(heading);
        await _context.SaveChangesAsync();

        if (heading.Id == 0) return BadRequest();

        var resultHeading = _mapper.Map<Heading, HeadingInfoDto>(heading);
        return Json(resultHeading);
    }

    /// <summary>
    ///     Method for edit heading data.
    /// </summary>
    /// <param name="dto">Containing fields for edit a heading.</param>
    /// <returns></returns>
    /// <response code="200">Heading edited successfully.</response>
    /// <response code="404">Heading not found.</response>
    [HttpPost]
    [Route("edit")]
    //[Authorize(UserRoles.Admin)]
    public async Task<IActionResult> Edit(HeadingEditDto dto)
    {
        if (!await _context.Headings.AnyAsync(e => e.Id == dto.Id))
            return NotFound();

        if (_context.Headings.Any(e => e.Code == dto.Code && e.Id != dto.Id))
            return BadRequest(new { message = "Heading with this code already exists" });

        var changedHeading = _mapper.Map<HeadingEditDto, Heading>(dto);

        _context.Headings.Update(changedHeading);

        await _context.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    ///     Method for delete heading.
    /// </summary>
    /// <param name="headingId">Id of the heading to delete.</param>
    /// <response code="200">Heading deleted successfully.</response>
    /// <response code="404">Heading not found.</response>
    [HttpDelete]
    //[Authorize(UserRoles.Admin)]
    public async Task<IActionResult> Delete(int headingId)
    {
        var isAvaibleHeading = await _context
            .Headings
            .AnyAsync(e => e.Id == headingId);

        if (!isAvaibleHeading)
            return NotFound();

        _context.Headings.Remove(new Heading { Id = headingId });

        await _context.SaveChangesAsync();

        return Ok();
    }
}