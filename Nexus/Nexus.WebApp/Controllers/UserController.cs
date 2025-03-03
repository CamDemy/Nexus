using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Nexus.Application.Dtos;
using Nexus.WebApp.Models;

namespace Nexus.WebApp.Controllers;

public class UserController : Controller
{
    private readonly HttpClient _httpClient;
    private const string ApiUrl = "https://localhost:5001/api/products"; // Thay đổi URL API nếu cần

    public UserController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    // 1. Lấy danh sách sản phẩm từ API
    public async Task<IActionResult> Index()
    {
        var products = await _httpClient.GetFromJsonAsync<List<UserDto>>(ApiUrl);
        return View(products);
    }

    // 2. Hiển thị form thêm sản phẩm
    public IActionResult Create()
    {
        return View();
    }

    // 3. Gửi yêu cầu thêm sản phẩm lên API
    [HttpPost]
    public async Task<IActionResult> Create(UserDto product)
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiUrl, product);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
        }
        return View(product);
    }

    // 4. Hiển thị form chỉnh sửa sản phẩm
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _httpClient.GetFromJsonAsync<UserDto>($"{ApiUrl}/{id}");
        if (product == null)
            return NotFound();

        return View(product);
    }

    // 5. Gửi yêu cầu cập nhật sản phẩm lên API
    [HttpPost]
    public async Task<IActionResult> Edit(UserDto product)
    {
        if (ModelState.IsValid)
        {
            var response = await _httpClient.PutAsJsonAsync($"{ApiUrl}/{product.Id}", product);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");
        }
        return View(product);
    }

    // 6. Hiển thị form xác nhận xoá
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _httpClient.GetFromJsonAsync<UserDto>($"{ApiUrl}/{id}");
        if (product == null)
            return NotFound();

        return View(product);
    }

    // 7. Gửi yêu cầu xoá sản phẩm lên API
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var response = await _httpClient.DeleteAsync($"{ApiUrl}/{id}");
        return RedirectToAction("Index");
    }
}
