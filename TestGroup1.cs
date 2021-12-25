using System;
using System.Runtime.CompilerServices;
using Xunit.Abstractions;

namespace ApiSample;
using Xunit;

public class TestGroup1:BaseTest
{
   
    [Fact]
    public async void CanGetPostsFromService()
    {
        var result = await _client.AllPosts();
        Assert.NotEmpty(result);
    }

    [Theory]
    [MemberData(nameof(NewPostData))]
    public async void CanCreateNewPost(Post newPost)
    {
        var result = await _client.CreatePost(newPost);
        Assert.NotNull(result.id);
    }
[Theory]
[InlineData(2)]
[InlineData(1)]
    public async void CanGetCommentByPostId(int postId)
    {
        var result = await _client.GetComments(postId);
        Assert.True(result.Count>0);
    }
}