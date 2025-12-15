using Application.DTOs;
using System.Net.Http.Json;

namespace Web.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Categories
    public async Task<List<CategoryDto>> GetCategoriesAsync(string? searchTerm = null)
    {
        var url = string.IsNullOrWhiteSpace(searchTerm) 
            ? "api/categories" 
            : $"api/categories?search={searchTerm}";
        
        return await _httpClient.GetFromJsonAsync<List<CategoryDto>>(url) ?? new List<CategoryDto>();
    }

    public async Task<CategoryDetailDto?> GetCategoryByIdAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<CategoryDetailDto>($"api/categories/{id}");
    }

    public async Task<CategoryDto?> CreateCategoryAsync(string name, string description)
    {
        var response = await _httpClient.PostAsJsonAsync("api/categories", new { Name = name, Description = description });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<CategoryDto>();
    }

    // Gifts
    public async Task<List<GiftDto>> GetGiftsAsync(string? searchTerm = null, Guid? categoryId = null)
    {
        var query = new List<string>();
        if (!string.IsNullOrWhiteSpace(searchTerm)) query.Add($"search={searchTerm}");
        if (categoryId.HasValue) query.Add($"categoryId={categoryId}");
        
        var url = query.Any() ? $"api/gifts?{string.Join("&", query)}" : "api/gifts";
        return await _httpClient.GetFromJsonAsync<List<GiftDto>>(url) ?? new List<GiftDto>();
    }

    public async Task<GiftDto?> CreateGiftAsync(string name, string description, DateTime dateGiven, string recipient, Guid categoryId, string? imageUrl = null)
    {
        var response = await _httpClient.PostAsJsonAsync("api/gifts", new 
        { 
            Name = name, 
            Description = description, 
            DateGiven = dateGiven,
            Recipient = recipient,
            CategoryId = categoryId,
            ImageUrl = imageUrl
        });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<GiftDto>();
    }
}
