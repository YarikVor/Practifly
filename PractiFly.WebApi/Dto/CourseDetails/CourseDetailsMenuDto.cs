using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.CourseDetails
{
    //тема + матеріали
    public class CourseDetailsMenuDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        //масив
        public MaterialItemDto[] MaterialItemDto { get; set; } = null!;
    }
}
