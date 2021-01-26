using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD_Project.Models.DTOs
{
    public class BoxerDto
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Status { get; set; }
        public string Division { get; set; }
    }
}
