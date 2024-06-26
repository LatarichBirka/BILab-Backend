﻿using BILab.Domain;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace BILab.WebAPI.Controllers.Base {
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase {
        protected string? GetAuthUserId() => User.Claims
            .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)?.Value;
        protected bool IsAdministratorRequest() => User.IsInRole(Constants.NameRoleAdmin);
        protected IActionResult GetResult(ServiceResult result, int statusCode) => result.Success ? StatusCode(statusCode, result) : BadRequest(result);
    }
}
