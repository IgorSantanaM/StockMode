using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.Application.Common.Dtos
{
    public class EmailAttachment
    {
        public string FileName { get; }
        public byte[] Data { get; }
        public string MimeType { get; }

        public EmailAttachment(string fileName, byte[] data, string mimeType)
        {
            FileName = fileName;
            Data = data;
            MimeType = mimeType;
        }
    }
}
