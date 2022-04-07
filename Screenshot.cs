using System;
using RestSharp;
using Restpack;
using Newtonsoft.Json;

namespace Restpack.Screenshot
{
  public class Screenshot
  {
    private RestClient client;

    public struct RequestOptions
    {
      [JsonProperty("mode")]
      public string Mode;

      [JsonProperty("format")]
      public string Format;

      [JsonProperty("json")]
      public bool Json;

      [JsonProperty("url")]
      public string Url;

      [JsonProperty("html")]
      public string HTML;

      [JsonProperty("height")]
      public int Height;

      [JsonProperty("width")]
      public int Width;

      [JsonProperty("thumbnail_height")]
      public int ThumbnailHeight;

      [JsonProperty("thumbnail_width")]
      public int ThumbnailWidth;

      [JsonProperty("css")]
      public string CSS;

      [JsonProperty("js")]
      public string JS;

      [JsonProperty("delay")]
      public int Delay;

      [JsonProperty("cache_ttl")]
      public int CacheTTL;

      [JsonProperty("user_agent")]
      public string UserAgent;

      [JsonProperty("accept_language")]
      public string AcceptLanguage;

      [JsonProperty("headers")]
      public string Headers;

      [JsonProperty("element_selector")]
      public string ElementSelector;

      [JsonProperty("retina")]
      public bool Retina;

      [JsonProperty("emulate_media")]
      public string EmulateMedia;

      [JsonProperty("allow_failed")]
      public bool AllowFailed;

      [JsonProperty("wait")]
      public string Wait;

      [JsonProperty("shutter")]
      public string Shutter;

      [JsonProperty("privacy")]
      public bool Privacy;

      [JsonProperty("filename")]
      public string Filename;

      [JsonProperty("block_ads")]
      public bool BlockAds;

      [JsonProperty("block_cookie_warnings")]
      public bool BlockCookieWarnings;

      [JsonProperty("omit_background")]
      public bool OmitBackground;
    }

    public Screenshot(string accessToken)
    {
      client = new Client("https://restpack.io/api/screenshot/v7/capture", accessToken).GetClient();

      JsonConvert.DefaultSettings = () => new JsonSerializerSettings
      {
        DefaultValueHandling = DefaultValueHandling.Ignore,
        NullValueHandling = NullValueHandling.Ignore
      };
    }

    private ResultJson Execute(RequestOptions options)
    {
      RestRequest request = new RestRequest();

      var jsonBody = JsonConvert.SerializeObject(options, Formatting.Indented);

      request.AddJsonBody(jsonBody);

      var response = client.Post(request);

      if (!response.IsSuccessful)
      {
        var resultError = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultError>(response.Content);
        throw new Exception(resultError.Error);
      }

      return Newtonsoft.Json.JsonConvert.DeserializeObject<ResultJson>(response.Content);
    }

    private byte[] ExecuteBytes(RequestOptions options)
    {
      RestRequest request = new RestRequest();

      var jsonBody = JsonConvert.SerializeObject(options, Formatting.Indented);

      request.AddJsonBody(jsonBody);

      var response = client.Post(request);

      if (!response.IsSuccessful)
      {
        var resultError = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultError>(response.Content);
        throw new Exception(resultError.Error);
      }

      return response.RawBytes;
    }


    public ResultJson Capture(string url, RequestOptions options = new RequestOptions())
    {
      options.Json = true;
      options.Url = url;

      return this.Execute(options);
    }

    public byte[] CaptureBytes(string url, RequestOptions options = new RequestOptions())
    {
      options.Json = false;
      options.Url = url;

      return this.ExecuteBytes(options);
    }

    public ResultJson CaptureHTML(string html, RequestOptions options = new RequestOptions())
    {
      options.Json = true;
      options.HTML = html;

      return this.Execute(options);
    }

    public byte[] CaptureHTMLBytes(string html, RequestOptions options = new RequestOptions())
    {
      options.Json = false;
      options.HTML = html;

      return this.ExecuteBytes(options);
    }
  }
}
