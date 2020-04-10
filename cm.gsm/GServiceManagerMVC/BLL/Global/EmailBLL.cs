using System;
using System.Net.Mail;

namespace GServiceManagerMVC.BLL.Global
{
    public class EmailBLL
    {
        public bool EnviarEmail(string emailPara, string tituloEmail, string corpoEmail)
        {
            System.Net.NetworkCredential credencial = new System.Net.NetworkCredential("servico@gsoftware.com.br", "P@ssw0rd");

            SmtpClient mailClient = new SmtpClient("smtp.office365.com");

            mailClient.Port = 587;
            mailClient.EnableSsl = true;
            mailClient.Credentials = credencial;

            MailMessage message = new MailMessage();
            message.From = new MailAddress("servico@gsoftware.com.br", "Global System");
            message.To.Add(emailPara);
            message.Subject = tituloEmail;
            message.IsBodyHtml = true;
            message.Body = corpoEmail;

            try
            {
                mailClient.Send(message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}