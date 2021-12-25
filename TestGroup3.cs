using System;
using System.Net.Http;

namespace ApiSample;
using Xunit;
public class TestGroup3:BaseTest
{
    [Fact(Skip = "Api allows negative id numbers")]
    public async void PostIsNotDeletedWhenInvalidIdIsUsedAsParameter()
    {
        int id = -2;
        Assert.Throws<Exception>(()=>_client.Delete(id)) ;
    }

    [Theory]
    [MemberData(nameof(BadPostData))]
    public async void DoesNotUpdatePostWhenIdIsNull(Post postData)
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () => await _client.UpdatePost(postData));
    }

    [Theory]
    [MemberData(nameof(BadCommentData))]
    public async void DoesNotCreateCommentWhenPostIdIsNull(Comment commentData)
    {
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
            await _client.CreateComment(commentData));
    }

    [Theory]
    [InlineData(5000)]
    [InlineData(677)]
    public async void DoesNotReturnCommentWhenPostIdIsInvalid(int postId)
    {
        var comments = await _client.GetComments(postId);
        Assert.True(comments.Count==0);
    }
    
}