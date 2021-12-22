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
        var json = await result.Content.ReadAsStringAsync();
       return JsonConvert.DeserializeObject<Post>(json);
    }
}

