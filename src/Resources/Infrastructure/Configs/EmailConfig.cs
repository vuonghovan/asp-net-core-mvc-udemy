using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Configs
{
    public class EmailConfig
    {
        public string MailServerName { get; set; }
        public string MailServerAddress { get; set; }
        public string MailServerDomain { get; set; }
        public string MailServerPort { get; set; }
        public string MailServePassword { get; set; }
        public string MailToDebug { get; set; }
        public bool IsEnable { get; set; }
    }
}
