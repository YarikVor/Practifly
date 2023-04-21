namespace PractiFly.WebApi.Dto.CourseDetails
{
    //матеріали
    public class MaterialItemDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = null!;
        
        public int Grade { get; set; }

        public bool IsSelected { get; set; }
    }
}
