using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;

namespace MvcNetCoreUtilidades.Controllers
{
    public class MailsController : Controller
    {
        private IConfiguration configuration;
        public MailsController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> SendMail(string to, string asunto, string mensaje)
        {
            MailMessage mail = new MailMessage();

            string user = this.configuration.GetValue<string>
                ("MailSettings:Credentials:User");
            mail.From = new MailAddress(user);
            mail.To.Add(to);
            mail.Subject = asunto;
            mail.Body = mensaje;
            mail.Priority = MailPriority.Normal;
            string password = this.configuration.GetValue<string>
                ("MailSettings:Credentials:Password");
            string host = this.configuration.GetValue<string>
            ("MailSettings:Server:Host");
            int port = this.configuration.GetValue<int>
            ("MailSettings:Server:Port");
            bool ssl = this.configuration.GetValue<bool>
            ("MailSettings:Server:Ssl");
            bool defaultCredentials = this.configuration.GetValue<bool>
            ("MailSettings:Server:DefaultCredentials");
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = host;
            smtpClient.Port = port;
            smtpClient.EnableSsl = ssl;
            smtpClient.UseDefaultCredentials = false;
            NetworkCredential credentials = new NetworkCredential(user, password);
            smtpClient.Credentials = credentials;
            await smtpClient.SendMailAsync(mail);
            ViewData["mensaje"] = "mail enviado";
            return View();
        }
    }
}
