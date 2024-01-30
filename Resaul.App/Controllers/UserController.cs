using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Resaul.App.Domain.Models;
using Resaul.App.Domain.Requests;
using Resaul.App.Domain.Responses;

namespace Resaul.App.Controllers;

[ApiController]
[Route("user")]
public sealed class UserController : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("create")]
    [ProducesResponseType<string>(400)]
    public async Task<ActionResult> CreateAsync(UserRegisterRequest request, UserManager<UserModel> manager)
    {
        var model = new UserModel
        {
            UserName = request.Username,
            Region = request.Region,
            UserLanguage = request.UserLanguage,
            UserDateBirth = request.UserDateBirth,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Phone = request.Phone,
            UserGender = request.UserGender,
        };

        var result = await manager.CreateAsync(model, request.Password);
        var token = await manager.GenerateUserTokenAsync(model, "PasswordlessLoginProvider", "email-auth");

        if (result.Succeeded is true)
            return Ok(new UserCreateResponse(request.Username, token));

        return BadRequest(result.Errors.First().Description);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType<string>(400)]
    public async Task<ActionResult> LoginAsync(UserLoginRequest request, SignInManager<UserModel> manager)
    {
        var result = await manager.PasswordSignInAsync(request.Username, request.Password, false, false);

        if (result.IsNotAllowed is true)
            return BadRequest("Need emailToken confirmation");

        if (result.Succeeded is false)
            return BadRequest();

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("checkOtpcode")]
    [ProducesResponseType(404)]
    [ProducesResponseType<string>(400)]
    public async Task<ActionResult> CheckConfirmEmailAsync(string token, string username, UserManager<UserModel> users, SignInManager<UserModel> manager)
    {
        var model = await users.FindByNameAsync(username);
        if (model is null) return NotFound();

        if (await users.VerifyUserTokenAsync(model, "PasswordlessLoginProvider", "email-auth", token) is false)
            return BadRequest();

        var emailToken = await users.GenerateEmailConfirmationTokenAsync(model);
        var result = await users.ConfirmEmailAsync(model, emailToken);

        if (result.Succeeded is false) return BadRequest(result.Errors.First());

        await manager.SignInAsync(model, false);
        return Ok();
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> LogoutAsync(SignInManager<UserModel> manager)
    {
        await manager.SignOutAsync();
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("{username}")]
    [ProducesResponseType(404)]
    public async Task<ActionResult> GetUserAsync(string username, UserManager<UserModel> manager)
    {
        var model = await manager.FindByNameAsync(username);
        if (model is null) return NotFound();

        var response = new UserResponse
        {
            UserName = model.UserName ?? string.Empty,
            Region = model.Region,
            UserLanguage = model.UserLanguage,
            UserDateBirth = model.UserDateBirth,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Phone = model.Phone,
            UserGender = model.UserGender,
        };

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPut("{username}")]
    [ProducesResponseType(404)]
    [ProducesResponseType<string>(400)]
    public async Task<ActionResult> UpdateUserAsync(string username, [FromBody] UserUpdateRequest request, UserManager<UserModel> manager)
    {
        var model = await manager.FindByNameAsync(username);
        if (model is null) return NotFound();

        model.UserName = request.Username;
        model.Region = request.Region;
        model.UserLanguage = request.UserLanguage;
        model.UserDateBirth = request.UserDateBirth.ToShortDateString();
        model.FirstName = request.FirstName;
        model.LastName = request.LastName;
        model.Phone = request.Phone;
        model.UserGender = request.UserGender;

        var result = await manager.UpdateAsync(model);
        if (result.Succeeded is false) return BadRequest(result.Errors.First());

        var response = new UserResponse
        {
            UserName = model.UserName,
            Region = model.Region,
            UserLanguage = model.UserLanguage,
            UserDateBirth = model.UserDateBirth,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Phone = model.Phone,
            UserGender = model.UserGender,
        };

        return Ok(response);
    }
}