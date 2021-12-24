using Xunit;

namespace ApiSample;

public class TestGroup2:BaseTest
{
    [Theory]
    [MemberData(nameof(PostData))]
    public async void CanUpdatePost(Post postDatum)
    {
        var updatedPost = await _client.UpdatePost(postDatum);
        Assert.True(updatedPost.id.Equals(postDatum.id) && updatedPost.userId.Equals(postDatum.userId));
        
    }

    [Fact]
    public async void CanDeletePost()
    {
        _client.Delete(1);
    }

    [Theory]
    [MemberData(nameof(NewCommentData))]
    public async void CanAddComment(Comment newComment)
    { 
        var result = await _client.CreateComment(newComment);
        Assert.Equal(newComment.body,result.body);
    }
}