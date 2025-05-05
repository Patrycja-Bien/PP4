﻿namespace User.Application;

public interface IJwtTokenService
{
    public string GenerateToken(int userId, List<string> roles);
}