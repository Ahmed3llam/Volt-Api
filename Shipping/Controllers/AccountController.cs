using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shipping.Models;
using Shipping.DTO.AccountDTOs;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Swashbuckle.AspNetCore.Annotations;
using AutoMapper;
using Shipping.DTO.Employee_DTOs;
using static Shipping.Constants.Permissions;

namespace Shipping.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        #region Login
        [HttpPost("login")]
        [AllowAnonymous]
        //[SwaggerOperation(Summary = "Authenticates a user and returns a JWT token.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully authenticated and returns a JWT token.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid email or password.")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(login.email);
                if (user == null)
                {
                    return BadRequest(new { message = "البريد الالكتروني او اسم المستخدم غير صحيح" });
                }

                var result = await _signInManager.PasswordSignInAsync(user, login.password, login.rememberMe, false);
                var userClaims = await _userManager.GetClaimsAsync(user);
                userClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

                string key = "Iti Pd And Bi 44 Menoufia Shipping System For GP";
                var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                var credential = new SigningCredentials(secertkey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: userClaims,
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: credential
                );

                var tokenstring = new JwtSecurityTokenHandler().WriteToken(token);
                var roles = await _userManager.GetRolesAsync(user);
                if (result.Succeeded)
                {
                    var UserData = _mapper.Map<UserDTO>(user);
                    UserData.role = roles.FirstOrDefault();
                    UserData.token = tokenstring;
                    return Ok(new { message = "تم تسجيل الدخول",User= UserData });
                }
                else
                {
                    return BadRequest(new { message = "كلمة المرور غير صحيحة" });
                }
            }
            return BadRequest(new { message = "البريد الالكتروني او كلمة المرور غير صحيحين" });
        }
        #endregion

        #region Logout
        [HttpPost("logout")]
        [Permission("anyUser")]
        [SwaggerOperation(Summary = "Logs out the current user.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successfully logged out.")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "تم تسجيل الخروج" });
        }
        #endregion

        #region Change Password
        [HttpPost("changePassword")]
        [Permission("anyUser")]
        [SwaggerOperation(Summary = "Changes the password for the logged-in user.")]
        [SwaggerResponse(StatusCodes.Status200OK, "Password changed successfully.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid password details.")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "User not found.")]
        public async Task<IActionResult> ChangePassword(PasswordDTO password)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized(new { message = "هذا المستخدم غير موجود" });
            }
            bool OldPassCheck = await _userManager.CheckPasswordAsync(user, password.oldPassword);
            if (!OldPassCheck)
            {
                return BadRequest(new { message = "كلمة المرور القديمة غير متطابقة" });
            }
            if (OldPassCheck && password.newPassword == password.confirmNewPassword)
            {
                var result = await _userManager.ChangePasswordAsync(user, password.oldPassword, password.newPassword);
                if (!result.Succeeded)
                {
                    return BadRequest(new { message = "خطأ في تغير كلمة المرور", errors = result.Errors });
                }

                //await _signInManager.SignOutAsync();
                return Ok(new { message = "تم تغير كلمة المرور بنجاح" });
            }
            else
            {
                return BadRequest(new { message = "كلمة المرور غير متطابقة" });
            }
        }
        #endregion
    }
}
