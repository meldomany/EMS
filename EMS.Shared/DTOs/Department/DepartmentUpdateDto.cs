﻿using System.ComponentModel.DataAnnotations;

namespace EMS.Shared.DTOs.Department
{
    public class DepartmentUpdateDto : BaseDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Department name can't exceed 100 characters.")]
        public string Name { get; set; }
    }
}