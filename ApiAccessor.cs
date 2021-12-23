using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiSample;

public class ApiAccessor
{
    protected static HttpClient _client;

    public ApiAccessor()
    {
        _client = new HttpClient();
    }
    protected async Task<Post> GetById(int id)
    {
        var result = await _client.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
        result.EnsureSuccessStatusCode();
        var jsonString = await result.Content.ReadAsStringAsync();
        return createObject<Post>(jsonString);

    }

    protected async Task<List<Post>> AllPosts()
    {
        var result = await _client.GetAsync("https://jsonplaceholder.typicode.com/posts");
        result.EnsureSuccessStatusCode();
        var jsonString = await result.Content.ReadAsStringAsync();
        return createObject<List<Post>>(jsonString);
    }

    protected async Task<Post> CreatePost(Post data)
    {
        string postData = JsonConvert.SerializeObject(data);
        StringContent httpContent = new StringContent(postData, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("https://jsonplaceholder.typicode.com/posts", httpContent);
        response.EnsureSuccessStatusCode();
        var jsonResult = await response.Content.ReadAsStringAsync();
        return createObject<Post>(jsonResult);
    }

    protected async Task<Post> UpdatePost(Post newData)
    {
        string newPostData = JsonConvert.SerializeObject(newData);
        StringContent httpContent = new StringContent(newPostData, System.Text.Encoding.UTF8, "application/json");

        var response = await _client.PutAsync($"https://jsonplaceholder.typicode.com/posts{newData.id}", httpContent);
        response.EnsureSuccessStatusCode();
        var jsonResult = await response.Content.ReadAsStringAsync();
        return createObject<Post>(jsonResult);
    }

    protected async void Delete(int id)
    {
        var response = await _client.DeleteAsync($"https://jsonplaceholder.typicode.com/posts{id}");
        response.EnsureSuccessStatusCode();
    }
    private T createObject<T>(string data)
    {
        return JsonConvert.DeserializeObject<T>(data);
    }
}