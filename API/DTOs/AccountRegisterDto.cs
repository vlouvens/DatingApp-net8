using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class AccountRegisterDto
{
    [Required]
    [MaxLength(15)]
    public required string Username { get; set; }
    
    [Required]
    public required string  Password { get; set; }

}
