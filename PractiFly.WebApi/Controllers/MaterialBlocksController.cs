using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Materials;
using PractiFly.WebApi.Dto.MaterialBlocks;

namespace PractiFly.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class MaterialBlocksController : Controller
    {
        public IPractiflyContext _context;
        public IMapper _mapper;

        public MaterialBlocksController(IPractiflyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        //метод для відображення ієрархії рубрик дублюється з HeadingController

        /*
         Місце для методу
         */

        //метод для відображення матеріалів, що належать рубриці 
        /// <summary>
        /// Returns a list of materials associated with a heading identified by the specified Id.
        /// </summary>
        /// <param name="headingId">Id of the heading</param>
        /// <response code="200">Getting materials from heading was successful.</response>
        /// <response code="400">Operation was failed.</response>
        /// <response code="404">No heading found.</response>
        /// <returns>A JSON-encoded representation of the list of materials.</returns>
        //TODO: Тут матеріал лише отримується. ПЕРЕДИВИТИСЯ
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetMaterialsFromHeading(int headingId)
        {
            var result = await _context
                .HeadingMaterials
                .AsNoTracking()
                .Where(e => e.HeadingId == headingId)
                .Select(e => new MaterialsHeadingItemDto()
                {
                    Id = e.Id,
                    Name = e.Material.Name,
                    IsIncluded = _context.HeadingMaterials.Any(e => e.HeadingId == headingId),
                    IsPractical = e.Material.IsPractical
                })
                .ToListAsync();

            return Json(result);
        }


        /// <summary>
        /// Creates a new material block with the specified properties.
        /// </summary>
        /// <param name="blockDto">A data transfer object containing the properties of the new material block.</param>
        /// <response code="200">Create material block was successful.</response>
        /// <response code="400">Create block was failed.</response>
        /// <returns>A JSON-encoded representation of the new material block.</returns>
        [HttpPost]
        [Route("material")]
        //TODO: має бути створення блоку, але тут створюється матеріал
        public async Task<IActionResult> CreateMaterialBlock(CreateBlockDto blockDto)
        {
            var material = new Material()
            {
                Name = blockDto.Name,
                Note = blockDto.Note,
                Url = blockDto.Url,
                IsPractical = blockDto.IsPractical
            };
            await _context.Materials.AddAsync(material);

            //TODO: має бути мапінг
            return Json(material);
        }

        /// <summary>
        /// Returns a list of material blocks associated with a block identified by the specified Id. 
        /// </summary>
        /// <remarks>
        /// When a block is clicked, the method displays the materials that are included in the block, as well as those that are not included in any block.
        /// </remarks>
        /// <param name="blockId">Id of the material block.</param>
        /// <response code="200">Getting material blocks was successful.</response>
        /// <response code="400">Operation was failed.</response>
        /// <response code="404">No blocks found.</response>
        /// <returns>A JSON-encoded representation of the list of material blocks.</returns>
        // TODO: ПЕРЕДИВИТИСЯ
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetBlocksWithMaterials(int blockId)
        {
            var result = await _context
                .MaterialBlocks
                .AsNoTracking()
                //TODO:
                //При натисканні на блок відображаються ті матеріали,
                //що включені  в цей блок, а також ті,
                //що не включені взагалі ні в один блок
                .Where(e => e.Id == blockId)
                .Select(e => new MaterialBlockItemDto(){
                    Id = e.Id,
                    Name = e.Parent.Name,
                    IsIncluded = _context.MaterialBlocks.Any(e => e.Id == blockId),
                    IsPractical = e.Parent.IsPractical
                }
            ).ToListAsync();

            return Json(result);
        }
    }
}
