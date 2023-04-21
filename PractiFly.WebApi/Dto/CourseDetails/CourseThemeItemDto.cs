namespace PractiFly.WebApi.Dto.CourseDetails
{
    //теми курсу
    public class CourseThemeItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
       
        //TODO: IsCompleted?
        // public bool IsCompleted { get; set; }
    }
}
