﻿namespace TestiranjeAPI.Models;

public class UserRegister
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Avatar { get; set; }
}
