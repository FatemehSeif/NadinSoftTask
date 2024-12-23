using Application.DTOs.AccountDtos;
using Application.Services.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly EmailService _emailService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public AccountController(UserManager<IdentityUser> userManager, EmailService emailService, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
        }






        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new IdentityUser
            {
                UserName = register.Email,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber, 
                
            };

            var result = await _userManager.CreateAsync(newUser, register.Password);
            if (result.Succeeded)
            {
              
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

                var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                {
                    userId = newUser.Id,
                    token = token
                }, protocol: Request.Scheme);



               
                string body = $"برای تأیید حساب کاربری خود لطفاً بر روی لینک زیر کلیک کنید: <br/> <a href=\"{callbackUrl}\">تأیید حساب</a>";
                string subject = "تایید حساب";
                await _emailService.ExecuteAsync(new EmailDto  ( newUser.Email, subject , body ));

                return Ok("ثبت‌نام انجام شد. لطفاً ایمیل خود را برای تأیید بررسی کنید.");
            }

            return BadRequest(result.Errors);
        }





        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest("Invalid request.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok("Account confirmed successfully.");
            }
            else
            {
                return BadRequest("Error confirming account.");
            }
        }

      

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityUser user = null;

           
            if (loginModel.EmailOrPhone.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(loginModel.EmailOrPhone);
            }
            else
            {
               
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == loginModel.EmailOrPhone);
            }

            if (user == null)
            {
                return Unauthorized("کاربر پیدا نشد.");
            }

          
            var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

            if (result.Succeeded)
            {
                return Ok("ورود با موفقیت انجام شد.");
            }

            return Unauthorized("نام کاربری یا رمز عبور اشتباه است.");
        }





    }




}

