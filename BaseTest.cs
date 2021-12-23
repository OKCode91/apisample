using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ApiSample;

public class BaseTest
{
    protected static ApiAccessor _client;

    public BaseTest()
    {
        _client = new ApiAccessor();
    }
}

