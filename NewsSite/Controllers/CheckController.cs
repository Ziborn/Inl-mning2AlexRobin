using NewsSite.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
namespace NewsSite.Controllers
{
    [Route("check")]
    public class CheckController : Controller
    {
        private ApplicationDbContext AppDb;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        
        public CheckController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext appDb)
        {
            AppDb = appDb;
            appDb.Database.EnsureCreated();
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet, Route("view/OpenNews")]
        public IActionResult ViewOpenNews()
        {
            return Ok();
        }

        [Authorize(Policy = "HiddenNews")]
        [HttpGet, Route("view/HiddenNews")]
        async public Task<IActionResult> ViewHiddenNews()
        {
            var user = await _userManager.FindByEmailAsync("adam@gmail.com");
            return Ok(user.PhoneNumber);
        }

        [HttpGet, Route("add")]
        async public Task<IActionResult> Add()
        {
            AppDb.EmptyDatabase();
            List<ApplicationUser> userList = new List<ApplicationUser>
           {
               new ApplicationUser { UserName = "Adam",   Email = "adam@gmail.com",   PhoneNumber = "055 20 04 40" },
               new ApplicationUser { UserName = "Peter",  Email = "peter@gmail.com",  PhoneNumber = "055 20 04 41" },
               new ApplicationUser { UserName = "Susan",  Email = "susan@gmail.com",  PhoneNumber = "055 20 04 42" },
               new ApplicationUser { UserName = "Viktor", Email = "viktor@gmail.com", PhoneNumber = "055 20 04 43" },
               new ApplicationUser { UserName = "Xerxes", Email = "xerxes@gmail.com", PhoneNumber = "055 20 04 44" }
           };
            foreach (var user in userList)
            {
                await _userManager.CreateAsync(user);
            }
            return Ok(userList);
        }

        [HttpGet, Route("addClaim")]
        async public Task<IActionResult> AddClaim()
        {
            var user = await _userManager.FindByEmailAsync("adam@gmail.com");
            var result = await _userManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
            var user2 = await _userManager.FindByEmailAsync("peter@gmail.com");
            var result2 = await _userManager.AddClaimAsync(user2, new Claim("UserRole", "Publisher"));
            var user3 = await _userManager.FindByEmailAsync("susan@gmail.com");
            var result3 = await _userManager.AddClaimAsync(user3, new Claim("UserRole", "Subscriber"));
            var user4 = await _userManager.FindByEmailAsync("viktor@gmail.com");
            var result4 = await _userManager.AddClaimAsync(user4, new Claim("UserRole", "Subscriber"));

            var user5 = await _userManager.FindByEmailAsync("susan@gmail.com");
            var result5 = await _userManager.AddClaimAsync(user5, new Claim("Age", "48"));
            var user6 = await _userManager.FindByEmailAsync("viktor@gmail.com");
            var result6 = await _userManager.AddClaimAsync(user6, new Claim("Age", "15"));
            return Ok(result);
        }

        [HttpGet, Route("info")]
        async public Task<IActionResult> Info()
        {
            var user = await _userManager.FindByEmailAsync("adam@gmail.com");
            return Ok(await _userManager.GetClaimsAsync(user));
        }

        [HttpPost, Route("login/{userEmail}")]
        async public Task<IActionResult> Login(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            await _signInManager.SignInAsync(user, false);
            await _signInManager.SignInAsync(user, true);
            return Ok(userEmail);
        }

        [Authorize(Policy = "HiddenNews")]
        [HttpGet, Route("check")]
        public IActionResult Check()
        {
            return Ok();
        }

        [HttpGet, Route("OpenNews")]
        public IActionResult OpenNewsCheck()
        {
            return Ok();
        }

        [Authorize(Policy = "MinAge20")]
        [HttpGet, Route("MinAgeNews")]
        public IActionResult OpenAgeNews()
        {
            return Ok("XXX news is allowed!");
        }
    }
}