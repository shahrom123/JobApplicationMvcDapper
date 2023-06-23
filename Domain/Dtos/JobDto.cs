using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos;

public class JobDto
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public string Location { get; set; }
    [Required]
    public string Description { get; set; } 
}