using System;
using RestSharp;
using Newtonsoft.Json;

namespace Restpack
{
  public class ResultJson
  {
    [JsonProperty("remote_status")]
    public int RemoteStatus;

    [JsonProperty("image")]
    public string Image;

    [JsonProperty("width")]
    public int Width;

    [JsonProperty("height")]
    public int Height;

    [JsonProperty("cached")]
    public Boolean Cached;

    [JsonProperty("url")]
    public string Url;
  }

  public class ResultError
  {
    [JsonProperty("error")]
    public string Error;
  }

  public class Client
  {
    public string BasePath, AccessToken;

    public RestClient client;

    public Client(string basePath, string accessToken)
    {
      BasePath = basePath;
      AccessToken = accessToken;

      client = new RestClient(basePath);
      client.AddDefaultHeader("x-access-token", accessToken);
    }

    public RestClient GetClient()
    {
      return client;
    }
  }
}
