using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class AccountRegisterDto
{
    [Required(ErrorMessage = "Username is required")]
    [MaxLength(15)]
    public  string Username { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")] 
    public  string  Password { get; set; }= string.Empty;

}
