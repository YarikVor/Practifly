﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Materials;
using PractiFly.WebApi.Context;
using PractiFly.WebApi.Dto.MaterialBlocks;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace PractiFly.WebApi.Controllers;

[Route("api")]
[ApiController]
public class MaterialBlocksController : Controller
{
    private readonly IConfigurationProvider _configurationProvider;
    private readonly IPractiflyContext _context;
    private readonly IMapper _mapper;

    public MaterialBlocksController(
        IPractiflyContext context,
        IMapper mapper,
        IConfigurationProvider configurationProvider)
    {
        _context = context;
        _mapper = mapper;
        _configurationProvider = configurationProvider;
    }

    //метод для відображення ієрархії рубрик дублюється з HeadingController

    /*
     Місце для методу
     */

    //метод для відображення матеріалів, що належать рубриці 
    /// <summary>
    ///     Returns a list of materials associated with a heading identified by the specified Id.
    /// </summary>
    /// <param name="headingId">Id of the heading</param>
    /// <response code="200">Getting materials from heading was successful.</response>
    /// <response code="400">Operation was failed.</response>
    /// <response code="404">No heading found.</response>
    /// <returns>A JSON-encoded representation of the list of materials.</returns>
    [HttpGet]
    [Route("heading/materials")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetMaterialsFromHeading(int headingId)
    {
        var result = await _context
            .HeadingMaterials
            .AsNoTracking()
            .Where(e => e.HeadingId == headingId)
            .ProjectTo<MaterialsHeadingItemDto>(_configurationProvider)
            .ToListAsync();

        return Json(result);
    }


    /// <summary>
    ///     Creates a new material block with the specified properties.
    /// </summary>
    /// <param name="blockDto">A data transfer object containing the properties of the new material block.</param>
    /// <response code="200">Create material block was successful.</response>
    /// <response code="400">Create block was failed.</response>
    /// <returns>A JSON-encoded representation of the new material block.</returns>
    [HttpPost]
    [Route("material")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Teacher},{UserRoles.Manager}",
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateMaterial(CreateMaterialDto blockDto)
    {
        var material = _mapper.Map<CreateMaterialDto, Material>(blockDto);
        await _context.Materials.AddAsync(material);
        await _context.SaveChangesAsync();

        if (material.Id == 0)
            //return Problem(statusCode: 999);
            return BadRequest();

        var materialDto = _mapper.Map<Material, MaterialDto>(material);
        return Json(materialDto);
    }

    /// <summary>
    ///     Edit a material with the specified properties.
    /// </summary>
    /// <param name="blockDto">A data transfer object containing the properties of the changed material.</param>
    /// <response code="200">Edit material was successful.</response>
    /// <response code="400">Edit material was failed.</response>
    /// <response code="404">No material found.</response>
    /// <returns>A JSON-encoded representation of the new material block.</returns>
    [HttpPost]
    [Route("material/edit")]
    [Authorize(Roles = $"{UserRoles.Admin},{UserRoles.Teacher},{UserRoles.Manager}",
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> EditMaterial(EditMaterialDto blockDto)
    {
        if (!await _context.Materials.AnyAsync(e => e.Id == blockDto.Id))
            return NotFound();

        var material = _mapper.Map<EditMaterialDto, Material>(blockDto);

        _context.Materials.Update(material);
        await _context.SaveChangesAsync();

        return Ok();
    }

    /// <summary>
    ///     Gets all materials.
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Getting materials was successful.</response>
    /// <response code="400">Getting materials was failed.</response>
    /// <response code="404">Materials not found.</response>
    [HttpGet]
    [Route("material/all")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetMaterials()
    {
        var materials = await _context.Materials
            .AsNoTracking()
            .OrderBy(e => e.Id)
            .ProjectTo<MaterialItemDto>(_configurationProvider)
            .ToListAsync();

        return Json(materials);
    }
}