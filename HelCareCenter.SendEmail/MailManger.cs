using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelCareCenter.SendEmail
{
    public class MailManger
    {
        public static async Task sendEmailFn(MailStore item)
        {
            try
            {


                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("yourName", "yourEmail");
                message.From.Add(from);



                MailboxAddress to = new MailboxAddress(item.Name, item.Email);
                message.To.Add(to);

                message.Subject = "This is email subject";

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<h1>Hello  {item.Name} </h1>";
                bodyBuilder.TextBody = $"{item.Message} ";

                message.Body = bodyBuilder.ToMessageBody();

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    

                    
                    client.Authenticate("yourEmail", "yourPassword");
                   
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }


               
               

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }



        }
    }
}
