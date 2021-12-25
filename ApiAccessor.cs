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
    public async Task<Post> GetPostById(int id)
    {
        var result = await _client.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");

        var jsonString = await ProcessHttpResponseMessage(result);
        return createObject<Post>(jsonString);

    }

    public async Task<List<Post>> AllPosts()
    {
        var result = await _client.GetAsync("https://jsonplaceholder.typicode.com/posts");
        var jsonString = await ProcessHttpResponseMessage(result);
        return createObject<List<Post>>(jsonString);
    }
    public async Task<List<Comment>> GetComments(int postId)
    {
        var result = await _client
            .GetAsync($"https://jsonplaceholder.typicode.com/comments?postId={postId}");
        var jsonString = await ProcessHttpResponseMessage(result);
        return createObject<List<Comment>>(jsonString);
    }

    public async Task<Post> CreatePost(Post data)
    {
        string postData = JsonConvert.SerializeObject(data);
        StringContent httpContent = new StringContent(postData, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("https://jsonplaceholder.typicode.com/posts", httpContent);
        var jsonResult = await ProcessHttpResponseMessage(response);
        return createObject<Post>(jsonResult);
    }

    public async Task<Comment> CreateComment(Comment data)
    {
        string commentData = JsonConvert.SerializeObject(data);
        StringContent httpContent = new StringContent(commentData, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PostAsync( $"https://jsonplaceholder.typicode.com/posts/{data.postId}/comments", httpContent);
        var jsonResult = await ProcessHttpResponseMessage(response);
        return createObject<Comment>(jsonResult);
    }
    public async Task<Post> UpdatePost(Post newData)
    {
        string newPostData = JsonConvert.SerializeObject(newData);
        StringContent httpContent = new StringContent(newPostData, System.Text.Encoding.UTF8, "application/json");

        var response = await _client.PutAsync($"https://jsonplaceholder.typicode.com/posts/{newData.id}", httpContent);
        var jsonResult = await ProcessHttpResponseMessage(response);
        return createObject<Post>(jsonResult);
    }

    public async void Delete(int id)
    {
        var response = await _client.DeleteAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
        response.EnsureSuccessStatusCode();
    }
    private T createObject<T>(string data)
    {
        return JsonConvert.DeserializeObject<T>(data);
    }

    private async Task<string> ProcessHttpResponseMessage(HttpResponseMessage message)
    {
        message.EnsureSuccessStatusCode();
        return await message.Content.ReadAsStringAsync();
    }
}