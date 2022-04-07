using System;
using System.Threading.Tasks;
using RestSharp;
using Restpack;
using Newtonsoft.Json;

namespace Restpack.Html2Pdf
{
  public class Html2Pdf
  {
    private RestClient client;

    public struct RequestOptions
    {
      [JsonProperty("url")]
      public string Url;

      [JsonProperty("html")]
      public string HTML;

      [JsonProperty("json")]
      public bool Json;

      [JsonProperty("pdf_page")]
      public string PDFPage;

      [JsonProperty("pdf_margins")]
      public string PDFMargins;

      [JsonProperty("pdf_orientation")]
      public string PDFOrientation;

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

      [JsonProperty("pdf_width")]
      public string PdfWidth;

      [JsonProperty("pdf_height")]
      public string PdfHeight;

      [JsonProperty("pdf_header")]
      public string PdfHeader;

      [JsonProperty("pdf_footer")]
      public string PdfFooter;

      [JsonProperty("block_ads")]
      public bool BlockAds;

      [JsonProperty("block_cookie_warnings")]
      public bool BlockCookieWarnings;
    }

    public Html2Pdf(string accessToken)
    {
      client = new Client("https://restpack.io/api/html2pdf/v7/convert", accessToken).GetClient();

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

    private async Task<byte[]> ExecuteBytesAsync(RequestOptions options)
    {
      RestRequest request = new RestRequest();

      var jsonBody = JsonConvert.SerializeObject(options, Formatting.Indented);

      request.AddJsonBody(jsonBody);

      var response = await client.ExecutePostTaskAsync(request);

      if (!response.IsSuccessful)
      {
        var resultError = JsonConvert.DeserializeObject<ResultError>(response.Content);
        throw new Exception(resultError.Error);
      }

      return response.RawBytes;
    }


    public ResultJson Convert(string url, RequestOptions options = new RequestOptions())
    {
      options.Json = true;
      options.Url = url;

      return this.Execute(options);
    }

    public byte[] ConvertBytes(string url, RequestOptions options = new RequestOptions())
    {
      options.Json = false;
      options.Url = url;

      return this.ExecuteBytes(options);
    }

    public ResultJson ConvertHTML(string html, RequestOptions options = new RequestOptions())
    {
      options.Json = true;
      options.HTML = html;

      return this.Execute(options);
    }

    public byte[] ConvertHTMLBytes(string html, RequestOptions options = new RequestOptions())
    {
      options.Json = false;
      options.HTML = html;

      return this.ExecuteBytes(options);
    }

    public async Task<byte[]> ConvertHTMLBytesAsync(string html, RequestOptions options = new RequestOptions())
    {
      options.Json = false;
      options.HTML = html;

      return await ExecuteBytesAsync(options);
    }
  }
}
