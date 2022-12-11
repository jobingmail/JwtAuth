using AspNetCoreHero.ToastNotification.Abstractions;
using JwtAuthAPI.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace JwtAuthAPI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly INotyfService _notyf;

        public AccountController
            (
             IConfiguration configuration,
              INotyfService notyfService
            )
        {
            _configuration = configuration;
            _notyf = notyfService;
        }

        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Register(UserDTO userDto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["Settings:BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.PostAsJsonAsync("api/Account/Register", userDto);

                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    _notyf.Success("User Saved Successfully");
                }
                else
                {
                    _notyf.Error("Failed to Register");
                }
                return View(new UserDTO());
            }
        }
        // POST: AccountController/Create
                 

        public IActionResult Login()
        {
            return View(new LoginDTO());
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO userDto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["Settings:BaseUrl"]);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                userDto.Token = "";
                HttpResponseMessage Res = await client.PostAsJsonAsync("api/Authenticate/Login", userDto);

                var token = new TokenDTO();
                var reslogin = new LoginDTO();
                if (Res.IsSuccessStatusCode)
                {
                    var response = Res.Content.ReadAsStringAsync().Result;
                    _notyf.Success("Logged in SuccessFully");
                    token = JsonConvert.DeserializeObject<TokenDTO>(response);
                }
                else
                {
                    _notyf.Error("Failed to Login");
                }
                reslogin.Token = token.Token;
                return View(reslogin);
            }
        }
    }
}
