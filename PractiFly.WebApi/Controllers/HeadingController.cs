using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Materials;
using PractiFly.WebApi.Dto.Heading;

namespace PractiFly.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeadingController : Controller
    {
        private readonly IPractiflyContext _context;

        public HeadingController(IPractiflyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int headingId)
        {
            var headingInfo = _context
                .Headings
                .Where(e => e.Id == headingId)
                .Select(e => new HeadingInfoDto()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Code = e.Code,
                    Description = e.Description,
                    Note = e.Note,
                    Udc = e.Udc
                })
                .FirstOrDefault();

            if (headingInfo == null)
                return NotFound();

            return Json(headingInfo);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string udc)
        {
            var headingInfo = _context
                .Headings
                .Where(e => e.Udc == udc)
                .Select(e => new HeadingInfoDto()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Code = e.Code,
                    Description = e.Description,
                    Note = e.Note,
                    Udc = e.Udc
                })
                .FirstOrDefault();

            if (headingInfo == null)
                return NotFound();

            return Json(headingInfo);
        }

        [HttpGet]
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

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> Delete(int headingId)
        {
            var isAvaibleHeading = await _context
                .Materials
                .AnyAsync(e => e.Id == headingId);

            if (!isAvaibleHeading)
                return NotFound();
            
            _context.Headings.Remove(new Heading() {Id = headingId});
            
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}