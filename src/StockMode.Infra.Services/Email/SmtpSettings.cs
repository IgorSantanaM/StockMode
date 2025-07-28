using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMode.EmailWorker
{
    public class SmtpSettings
    {
        public string Host { get; set; } = "localhot";
        public int Port { get; set; } = 1025;
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool Authenticate => Username != null && Password != null;
    }
}
