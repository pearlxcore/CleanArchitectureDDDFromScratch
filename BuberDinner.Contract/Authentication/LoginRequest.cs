﻿namespace BuberDinner.Contract.Authentication
{
    public record LoginRequest(
        string Email,
        string Password);
}
