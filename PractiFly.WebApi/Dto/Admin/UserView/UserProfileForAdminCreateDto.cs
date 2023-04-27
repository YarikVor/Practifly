﻿using System.ComponentModel.DataAnnotations;
using PractiFly.WebApi.Attributes;

namespace PractiFly.WebApi.Dto.Admin.UserView;

public class UserProfileForAdminCreateDto
{

    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(128)]
    public string Surname { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(128)]
    public string Email { get; set; } = null!;
    
    [Phone]
    [MaxLength(32)]
    public string Phone { get; set; } = null!;
    
    [Url]
    [MaxLength(2048)]
    public string? FilePhoto { get; set; }
    
    [RoleString]
    public string Role { get; set; } = null!;
}