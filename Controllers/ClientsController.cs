using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Moot.Data;
using Moot.Models;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Moot.Controllers
{
    [Authorize(Policy = "SalesManager")]
    public class ClientsController : Controller
    {
        private readonly LibraryContext _context;
        // https://localhost:7248;http://localhost:5275
        private string _baseUrl = "https://localhost:7248/api/Clients";

        public ClientsController(LibraryContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> Index()
        {
            var clients = new HttpClient();
            var response = await clients.GetAsync(_baseUrl);

            if (response.IsSuccessStatusCode)
            {
                var client = JsonConvert.DeserializeObject<List<Client>>(await
               response.Content.
                ReadAsStringAsync());
                return View(client);
            }
            return NotFound();

        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var clients = new HttpClient();
            var response = await clients.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var client= JsonConvert.DeserializeObject<Client>(
 await response.Content.ReadAsStringAsync());
                return View(client);
            }
            return NotFound();
        }
    
        public IActionResult Create()
        {
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("ClientID,Name,Adress,BirthDate")]
Client client)
        {
            if (!ModelState.IsValid) return View(client);
            try
            {
                var clients = new HttpClient();
                string json = JsonConvert.SerializeObject(client);
                var response = await clients.PostAsync(_baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to create record: {ex.Message}");
            }
            return View(client);
        }

      

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var clients = new HttpClient();
            var response = await clients.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var client = JsonConvert.DeserializeObject<Client>(
                await response.Content.ReadAsStringAsync());
                return View(client);
            }
            return new NotFoundResult();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("ClientID,Name,Adress,BirthDate")]
Client client)
        {
            if (!ModelState.IsValid) return View(client);
            var clients = new HttpClient();
            string json = JsonConvert.SerializeObject(client);
            var response = await clients.PutAsync($"{_baseUrl}/{client.ClientID}",
            new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(client);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new BadRequestResult();
            }
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id.Value}");
            if (response.IsSuccessStatusCode)
            {
                var clients= JsonConvert.DeserializeObject<Client>(await
               response.Content.ReadAsStringAsync());
                return View(client);
            }
            return new NotFoundResult();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind("CientID")] Client client)
        {
            try
            {
                var clients = new HttpClient();
                HttpRequestMessage request =
                new HttpRequestMessage(HttpMethod.Delete,
               $"{_baseUrl}/{client.ClientID}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(client),
               Encoding.UTF8, "application/json")
                };
                var response = await clients.SendAsync(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete record:{ex.Message} ");
            }
            return View(client);
        }
    }

}
