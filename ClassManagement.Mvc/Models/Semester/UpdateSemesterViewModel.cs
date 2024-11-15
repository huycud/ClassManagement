﻿using System.ComponentModel.DataAnnotations;

namespace ClassManagement.Mvc.Models.Semester
{
    public class UpdateSemesterViewModel
    {
        [Display(Name = "Mã học kỳ")]
        public string Id { get; set; }

        [Display(Name = "Tên học kỳ")]
        public string Name { get; set; }
    }
}
