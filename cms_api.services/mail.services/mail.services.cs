using cms_api.entities;
using cms_api.entities.entities;
using cms_api.services.mail.interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace cms_api.services
{
    public class MailServices : IMailServices
    {
        private readonly IConfiguration _configuration;
        public MailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public MailNotification sendNotificationMail(MailEntities mailEntities)
        {
            try
            {
                mailEntities.from = _configuration.GetSection("mailInfo").GetSection("from").Value;
                string password = _configuration.GetSection("mailInfo").GetSection("password").Value;
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(mailEntities.from);
                message.To.Add(new MailAddress(mailEntities.to));
                message.Subject = mailEntities.subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = mailEntities.message;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(mailEntities.from, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return new MailNotification() { IsSuccess = true, Message = "sendSuccess" };

            }
            catch (Exception ex)
            {
                return new MailNotification() { IsSuccess = false, Message = ex.Message };
            }
        }

        public string getInformMessageBody(MailEntities mailEntities)
        {
            try
            {
                string messageBody =
                    $@"<div style='font-size: 14px;font-family:Trebuchet MS'>Hi, <b>Sachin</b></div><br/> " +
                    $@"<div style='font-size: 14px;font-family:Trebuchet MS'>The below user has tried to reach to you for the following query</div><br/>" +
                    $@"<div style = 'font-size: 14px;font-family:Trebuchet MS'><b> Name:</b><font> {char.ToUpper(mailEntities.firstname[0]) + mailEntities.firstname.Substring(1) + " " + char.ToUpper(mailEntities.lastname[0]) + mailEntities.lastname.Substring(1)}</font></div><br/> " +
                    $@"<div style='font-family:Trebuchet MS;font-size: 14px;'><b>Email Id:</b> <font>{mailEntities.email}</font></div><br/>" +
                    $@"<div style='text-align:justify;font-family:Trebuchet MS;font-size: 14px;' ><b> Description:</b><font> {mailEntities.description} </font></div><br/>" +
                    $@"<div style='font-size: 14px;font-family:Trebuchet MS'>Thanks & Regards,<br/><b>NoReply ASM 007</b></div>";

                return messageBody; // return HTML Table as string from this function  
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string getResponseMessage(MailEntities mailEntities)
        {
            try
            {
                string messageBody =
                    $@"<div style='font-size: 14px;font-family:Trebuchet MS'>Hi, <b> {char.ToUpper(mailEntities.firstname[0])+ mailEntities.firstname.Substring(1) + " " + char.ToUpper(mailEntities.lastname[0]) + mailEntities.lastname.Substring(1)}</b></div><br/>
                        <div style='font-size: 14px;font-family:Trebuchet MS'>
                         Thank you for inquiring about our new {mailEntities.subject} application. We will contact you with a detailed explanation of the query that fits your business need.</div><br/>
                        <div style='font-size: 14px;font-family:Trebuchet MS'>
                        Thanks again for your inquiry.
                        </div><br/>
                        <div style='font-size: 14px;font-family:Trebuchet MS'>
                        Thanks & Regards, 
                        <br/>
                        <b><a style='text - decoration:none' href='https://sm-cka.surge.sh/'>Sachin Mishra</a></b><br/>
                        <b> +91-9594500523 </b>
                        </div>";

                return messageBody; // return HTML Table as string from this function  
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
