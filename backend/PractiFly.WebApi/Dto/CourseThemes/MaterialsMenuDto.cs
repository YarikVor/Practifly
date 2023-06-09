﻿namespace PractiFly.WebApi.Dto.CourseThemes;

public class MaterialsMenuDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int PriorityLevel { get; set; }
    public bool IsIncluded { get; set; }
}