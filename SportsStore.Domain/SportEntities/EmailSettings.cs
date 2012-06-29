using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.Domain.Entities
{
    public class EmailSettings
    {
        public string mailToAddress = "orders@examle.mail.com";
        public string mailFromAddress = "sportsstore@example.com";
        public bool useSsl = true;
        public string userName = "MySmtpUserName";
        public string password = "MySmtpPassword";
        public string serverName = "smtp.example.com";

        public int serverPort = 587;
        public bool writeAsFile = false;
        public string FileLocation = @"D:\Practice\SvnLocalDir\SportsStore\Emails";
    }

}
