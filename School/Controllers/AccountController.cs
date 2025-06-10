using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.DTOs.AccountDTOs;
using School.DTOs.RegisterAndLoginDTOs;
using School.Models;
using School.Services;
using School.UnitOfWorks;

namespace School.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly UnitOfWork unitOfWork;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            UnitOfWork _unitOfWork,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            unitOfWork = _unitOfWork;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("model not valid"); 

            if (dto.Role == Role.Student)
            {
                var student = unitOfWork.StudentRepo.getAll()
                    .FirstOrDefault(s => s.NationalId == dto.NationalId);

                if (student == null)
                    return BadRequest(new { message = "National ID is not registered as a student in the system. Registration denied." });

                return await RegisterUser(dto, student.Id);
            }
            else if (dto.Role == Role.Teacher)
            {
                var teacher = unitOfWork.TeacherRepo.getAll()
                    .FirstOrDefault(t => t.NationalId == dto.NationalId);

                if (teacher == null)
                    return BadRequest(new { message = "National ID is not registered as a teacher in the system. Registration denied." });

                return await RegisterUser(dto, teacher.Id);
            }
            else if (dto.Role == Role.Parent)
            {
                var parent = unitOfWork.ParentRepo.getAll()
                    .FirstOrDefault(t => t.NationalId == dto.NationalId);

                if (parent == null)
                    return BadRequest(new { message = "National ID is not registered as a parent in the system. Registration denied." });

                return await RegisterUser(dto, parent.Id);
            }
            else
            {
                return BadRequest(new { message = "Unsupported role type. Only Student, Teacher and Parent roles are allowed." });
            }
        }

        private async Task<IActionResult> RegisterUser(RegisterDTO dto, int relatedId)
        {
            var appUser = new ApplicationUser
            {
                UserName = dto.Username,
                Email = dto.Email,
                PhoneNumber = dto.Phone,
                Role = dto.Role,
                RelatedId = relatedId
            };

            var result = await _userManager.CreateAsync(appUser, dto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Failed to create user", errors = result.Errors });
            }

            return Ok(new
            {
                userId = appUser.Id,
                role = appUser.Role.ToString(),
                message = "Registration completed successfully."
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null)
                return Unauthorized("Invalid Username or Password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return Unauthorized("Invalid Username or Password");

            var token = _tokenService.CreateToken(user);

            return Ok(new
            {
                username = user.UserName,
                role = user.Role.ToString(),
                relatedId = user.RelatedId,
                token = token
            });
        }


        //[HttpPost("Login")]
        //public async Task<IActionResult> Login(LoginDTO dto)
        //{
        //    var user = await _userManager.FindByNameAsync(dto.UserName);
        //    if (user == null)
        //        return Unauthorized("Invalid Username");

        //    var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        //    if (!result.Succeeded)
        //        return Unauthorized("Invalid Password");

        //    var token = _tokenService.CreateToken(user);
        //    return Ok(new { token });
        //}
    }
}



