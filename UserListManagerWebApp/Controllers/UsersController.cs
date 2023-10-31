using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using UserListManagerWebApp.Models;
using static UserListManagerWebApp.Shared;

namespace UserListManagerWebApp.Controllers;

public class UsersController : Controller
{
    private readonly HttpClient _client;

    public UsersController(IHttpClientFactory httpClientFactory)
    {
		_client = httpClientFactory.CreateClient(ApiClientName);
	}

	[HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var response = await _client.GetAsync("api/users");

        if (!response.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = response.Content.ToString();
            return View();
        }

        var content = await response.Content.ReadAsStringAsync();
        var users = JsonConvert.DeserializeObject<IEnumerable<User>>(content);

        return View(users);
    }

    [HttpGet("/Add")]
    public IActionResult AddUser()
    {
        return View();
    }
    [HttpPost("/Add")]
    public async Task<IActionResult> AddUser(User user)
    {
        if (!ModelState.IsValid)
            return View(user);

        var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("api/users", content);

        if (!response.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = response.Content.ToString();
            return View(user);
        }

        return RedirectToAction(nameof(GetUsers));
    }

    [HttpGet("{id}/Update")]
    public async Task<IActionResult> UpdateUser(int id)
    {
        var response = await _client.GetAsync($"api/users/{id}");

        if (!response.IsSuccessStatusCode)
        {
            TempData["ErrorMessage"] = response.Content.ToString();
            return RedirectToAction(nameof(GetUsers));
        }

        var content = await response.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<User>(content);

        return View(user); // Передаем данные в представление
    }
    [HttpPost("{id}/Update")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
		if (!ModelState.IsValid)
			return View(user);

		var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
		var response = await _client.PutAsync($"api/users/{id}", content);

		if (!response.IsSuccessStatusCode)
		{
			TempData["ErrorMessage"] = response.Content.ToString();
			return View(user);
		}

		return RedirectToAction(nameof(GetUsers));
	}

	[HttpGet("{id}/Delete")]
	public async Task<IActionResult> DeleteUser(int id)
	{
		var response = await _client.GetAsync($"api/users/{id}");

		if (!response.IsSuccessStatusCode)
		{
			TempData["ErrorMessage"] = response.Content.ToString();
			return RedirectToAction(nameof(GetUsers));
		}

		var content = await response.Content.ReadAsStringAsync();
		var user = JsonConvert.DeserializeObject<User>(content);

		return View(user);
	}
	[HttpPost("{id}/Delete")]
	public async Task<IActionResult> DeleteUser(int id, User user)
	{
		var response = await _client.DeleteAsync($"api/users/{id}");

		if (!response.IsSuccessStatusCode)
		{
			TempData["ErrorMessage"] = response.Content.ToString();
			return View(user);
		}

		return RedirectToAction(nameof(GetUsers));
	}
}
