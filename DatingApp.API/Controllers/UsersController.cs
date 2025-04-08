using DatingApp.API.Data;
using DatingApp.API.Models.Domain;
using DatingApp.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext dbContext = dbContext;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            List<AppUser> appUsers = await dbContext.Users.ToListAsync();
            List<GetAppUserDTO> appUserDTOs = appUsers.Select(user => new GetAppUserDTO
            {
                Id = user.Id,
                UserName = user.UserName
            }).ToList();
            return Ok(appUserDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(int id)
        {
            var appUsers = await dbContext.Users.FindAsync(id);
            if (appUsers == null)
            {
                return NotFound();
            }
            GetAppUserDTO appUserDTOs = new()
            {
                Id = appUsers.Id,
                UserName = appUsers.UserName
            };
            return Ok(appUserDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateAppUser request)
        {
            AppUser appUser = new()
            {
                UserName = request.UserName
            };

            await dbContext.Users.AddAsync(appUser);
            await dbContext.SaveChangesAsync();

            GetAppUserDTO response = new()
            {
                Id = appUser.Id,
                UserName = appUser.UserName
            };

            return Ok(response);
        }
    }
}
