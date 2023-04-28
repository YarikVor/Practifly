using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbContextUtility.Context.PractiflyDb;
using PractiFly.DbEntities.Materials;
using PractiFly.WebApi.Dto.MaterialBlocks;

namespace PractiFly.WebApi.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetMaterialsFromHeading(int materialId)
        {
            var result = await _context
                .HeadingMaterials
                .AsNoTracking()
                .Where(e => e.Id == materialId)
                .Select(e => new MaterialsHeadingItemDto()
                {
                    Id = e.Id,
                    Name = e.Material.Name,
                    IsIncluded = _context.HeadingMaterials.Any(e => e.Id == materialId),
                    IsPractical = e.Material.IsPractical
                })
                .ToListAsync();

            return Json(result);
        }

        [HttpGet]
        [Route("[action]")]
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
