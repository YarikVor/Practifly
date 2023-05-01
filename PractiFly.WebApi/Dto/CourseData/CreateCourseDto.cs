﻿using PractiFly.DbEntities;
using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.CourseData
{
    public class CreateCourseDto
    {

        public int CourseId { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Language { get; set; } = null!;

        [Required]
        [MaxLength(EntitiesConstantLengths.Name)]
        public string CourseName { get; set; } = null!;

        [Required]
        public int OwnerId { get; set; }

        [MaxLength(EntitiesConstantLengths.Note)]
        public string? Note { get; set; }

        [MaxLength(EntitiesConstantLengths.Description)]
        public string? Description { get; set; }
    }
}