﻿using CostMSWebAPI.Models;
using CostMSWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CostMSWebAPI.Controllers;

[AllowAnonymous]
[ApiController]
[Route("")]
public class RegistrationController : ControllerBase
{
    private readonly IUserService _userService;

    public RegistrationController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("registration")]
    public ActionResult<User> Create(User user)
    {
        return StatusCode(StatusCodes.Status201Created, _userService.Create(user));
    }
}
