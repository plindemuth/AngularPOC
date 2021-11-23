using System.Collections.Generic;
using System.IO;

namespace ApiUtilities.Models
{
    public class MultiPartRequest
    {
        public MultiPartRequest()
        {
        }

        public string FileName { get; set; }

        public MemoryStream FileStream { get; set; }
    }
}

