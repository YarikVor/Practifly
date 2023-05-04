namespace PractiFly.WebApi.Dto.Heading;

public class HeadingInfoDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string Udc { get; set; } = null!;
    public string? Note { get; set; }
    public string? Description { get; set; }
}