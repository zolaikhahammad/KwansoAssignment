using KwansoWebApp.Contracts.Tasks.Request;
using KwansoWebApp.Contracts.Tasks.Response;
using KwansoWebApp.Contracts.User.Request;
using KwansoWebApp.Contracts.User.Response;
using KwansoWebApp.Contracts.ViewModels;
using KwansoWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace KwansoWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration iConfig)
        {
            _logger = logger;
            configuration = iConfig;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
       
        public IActionResult TasksList()
        {
            string baseUrl = configuration.GetSection("Api").GetSection("apiUrl").Value;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            client.DefaultRequestHeaders.Authorization =new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            HttpResponseMessage response = client.GetAsync("/list-tasks").Result;
            string stringTasks = response.Content.ReadAsStringAsync().Result;
            DataTasksResponse tasksList = JsonConvert.DeserializeObject<DataTasksResponse>(stringTasks);
            List<TaskViewModel> lst = new List<TaskViewModel>();
            if(tasksList!=null && tasksList.data.status_code == 200)
            {
                lst = tasksList.data.tasks;
            }
            return View(lst);
        }
        
        [HttpGet]
        public IActionResult CreateTasks()
        {            
            return View();
        }
        [HttpPost]
        public IActionResult CreateTasks(IFormCollection form)
        {
            string baseUrl = configuration.GetSection("Api").GetSection("apiUrl").Value;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CreateTaskRequest request = new CreateTaskRequest();
            request.Name = Convert.ToString(form["name"]);            
            string stringData = JsonConvert.SerializeObject(request);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/create-task", contentData).Result;
            string resposne = response.Content.ReadAsStringAsync().Result;
            DataTasksResponse jwt = JsonConvert.DeserializeObject<DataTasksResponse>(resposne);
            if (jwt != null && jwt.data.status_code == 200)
            {
                 return RedirectToAction("TasksList", "Home");


            }
            return View();
        }
        [HttpPost]
        public IActionResult DeleteTask(List<int> Ids)
        {
            string baseUrl = configuration.GetSection("Api").GetSection("apiUrl").Value;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
            client.DefaultRequestHeaders.Accept.Add(contentType);            
            string stringData = JsonConvert.SerializeObject(Ids);
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/delete-task", contentData).Result;
            string resposne = response.Content.ReadAsStringAsync().Result;
            DataTasksResponse jwt = JsonConvert.DeserializeObject<DataTasksResponse>(resposne);
            if (jwt != null && jwt.data.status_code == 200)
            {
                return RedirectToAction("TasksList", "Home");


            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(IFormCollection form)
        {
            string baseUrl = configuration.GetSection("Api").GetSection("apiUrl").Value;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            UserLoginRequest userModel = new UserLoginRequest();
            userModel.email = Convert.ToString(form["email"]);
            userModel.password = Convert.ToString(form["password"]);
            string stringData = JsonConvert.SerializeObject(userModel);
            var contentData = new StringContent(stringData,System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("/login", contentData).Result;
            string stringJWT = response.Content.ReadAsStringAsync().Result;
            DataUserLoginResponse jwt = JsonConvert.DeserializeObject<DataUserLoginResponse>(stringJWT);
            if(jwt != null && jwt.data.status_code==200)
            {
                HttpContext.Session.SetString("token", jwt.data.jwt.token);
                return RedirectToAction("TasksList","Home");
               

            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
