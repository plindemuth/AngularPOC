using ApiUtilities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System.IO;

namespace ApiUtilities
{
    public static class MultiPartRequestUtility
    {
        public static MultiPartRequest ProcessRequest(HttpRequest request)
        {
            var multipartRequestRepresentation = new MultiPartRequest();

            var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(request.ContentType).Boundary).Value;
            var reader = new MultipartReader(boundary, request.Body);
            var section = reader.ReadNextSectionAsync().Result;

            multipartRequestRepresentation.FileStream = new MemoryStream();
            multipartRequestRepresentation.FileName = section.AsFileSection()?.FileName;
            section.AsFileSection()?.FileStream.CopyToAsync(multipartRequestRepresentation.FileStream).ConfigureAwait(false);

            return multipartRequestRepresentation;
        }
    }
}
