using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit;
using Xunit.Sdk;

namespace ApiSample;

public class BaseTest
{
    protected static ApiAccessor _client;

    public BaseTest()
    {
        _client = new ApiAccessor();
    }

    public static TheoryData<Post> PostData =>
        new TheoryData<Post>
        {
             new Post(1,5,String.Empty,  LoremNET.Lorem.Words(3) ),
            new Post(2,  20,LoremNET.Lorem.Sentence(3, 20), 
                LoremNET.Lorem.Words(3) )
        };

    public static TheoryData<Post> NewPostData =>
        new TheoryData<Post>
        {
            new Post(1, null, LoremNET.Lorem.Sentence(3, 20),
                LoremNET.Lorem.Paragraph(10, 4)),
            new Post(2, null, LoremNET.Lorem.Sentence(3, 20),
                LoremNET.Lorem.Paragraph(10, 4))
        };

    public static TheoryData<Comment> NewCommentData =>
        new TheoryData<Comment>
        {
            new Comment(2, null, LoremNET.Lorem.Words(3), LoremNET.Lorem.Email(),
                LoremNET.Lorem.Sentence(3, 30))
        };
}

