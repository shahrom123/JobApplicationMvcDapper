using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Dtos;
public class UserDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "FirstName is required")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "LastName is required")]
    public string LastName { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Required(ErrorMessage = "PhoneNumber is required")]
    public string PhoneNumber { get; set; }
    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }
    [Required(ErrorMessage = "JobId is required")]
    public int JobId { get; set; }
}