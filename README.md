# restpack-csharp

Official C# client for Restpack APIs

## Installation

The recommended way to install restpack-csharp is through [Nuget](https://nuget.org):

Install the latest restpack-csharp nuget module:

```
$ dotnet add package Restpack
```

Finally, you need to require the library in your C# application:

```csharp
Using Restpack;
```

## Screenshot API

For detailed documentation, please visit [Screenshot API v5 Reference page](https://restpack.io/screenshot/docs).

```csharp

using System;
using Restpack.Screenshot;

namespace Program
{
  class Program
  {
    static void Main(string[] args)
    {
      var Screenshot = new Screenshot("<YOUR ACCESS TOKEN>");

      var options = new Screenshot.RequestOptions();
      options.Format = "png";
      options.Delay = 3000;

      // Capture given URL. Return the document details and CDN url of the Image
      var captureResult = Screenshot.Capture("https://google.com", options);
      Console.WriteLine(captureResult.Image);

      // Capture given URL. Return the image file as bytes[]
      var captureBytesResult = Screenshot.CaptureBytes("https://google.com", options);
      Console.WriteLine(captureBytesResult);

      // Capture given html content. Return the document details and CDN url of the Image
      var captureHTMLResult = Screenshot.CaptureHTML("<h1>Test</h1>", options);
      Console.WriteLine(captureHTMLResult.Image);

      // Capture given html content. Return the image file as bytes[]
      var captureBytesResult = Screenshot.CaptureHTMLBytes("<h1>Test</h1>", options);
      Console.WriteLine(captureBytesResult);
    }
  }
}

```

## HTML To PDF API

For detailed documentation, please visit [HTML to PDF API v5 Reference page](https://restpack.io/html2pdf/docs).

```php
using System;
using Restpack.Pdf2Html;

namespace Program
{
  class Program
  {
    static void Main(string[] args)
    {
      var Pdf2Html = new Pdf2Html("<YOUR ACCESS TOKEN>");

      var options = new Pdf2Html.RequestOptions();
      options.PDFOrientation = "landscape";

      // Convert given URL to PDF. Return the document details and CDN url of PDF
      var captureResult = Pdf2Html.Convert("https://google.com", options);
      Console.WriteLine(captureResult.Image);

      // Convert given URL to PDF. Return the PDF document as Buffer
      var captureBytesResult = Pdf2Html.ConvertBytes("https://google.com", options);
      Console.WriteLine(captureBytesResult);

      // Convert given html content to PDF. Return the document details and CDN url of PDF
      var captureHTMLResult = Pdf2Html.ConvertHTML("<h1>Test</h1>", options);
      Console.WriteLine(captureHTMLResult.Image);

      // Convert given html content to PDF. Return the PDF document as Buffer
      var captureBytesResult = Pdf2Html.ConvertHTMLBytes("<h1>Test</h1>", options);
      Console.WriteLine(captureBytesResult);
    }
  }
}
```
