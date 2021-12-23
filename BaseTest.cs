using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiSample;

public class BaseTest
{
    protected static HttpClient _client;

    public BaseTest()
    {
        _client = new HttpClient();
    }

    //todo Put json serialization into private method
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

    private T createObject<T>(string data)
    {
        return JsonConvert.DeserializeObject<T>(data);
    }
}

