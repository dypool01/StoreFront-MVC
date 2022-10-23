using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StoreFront.Models;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Data.SqlClient;
using Microsoft.CodeAnalysis.Emit;

namespace StoreFront.Controllers
{
    public class ContactViewModelsController : Controller
    {
 
        private readonly IConfiguration _config;

        
        public ContactViewModelsController(IConfiguration config)
        {
            _config = config;
        }

        

        public IActionResult Contact()
        {
            return View();

        }



        [HttpPost] 
        public IActionResult Contact(ContactViewModel cvm)
        {
            

            if (!ModelState.IsValid)
            {
                

                return View(cvm);
            }

            string message = $"You have received a new email from your site's contact form!<br />" +
                $"Sender: {cvm.Name}<br />Email: {cvm.Email}<br />Subject: {cvm.Subject}<br />" +
                $"Message: {cvm.Message}";

            var mm = new MimeMessage();


            mm.From.Add(new MailboxAddress("Sender", _config.GetValue<string>("Credentials:Email:User")));


            mm.To.Add(new MailboxAddress("Personal", _config.GetValue<string>("Credentials:Email:Recipient")));

            mm.Subject = cvm.Subject;

            mm.Body = new TextPart("HTML") { Text = message };

            mm.Priority = MessagePriority.Urgent;

            mm.ReplyTo.Add(new MailboxAddress("User", cvm.Email));

            using (var client = new SmtpClient())
            {
                client.Connect(_config.GetValue<string>("Credentials:Email:Client"));

                client.Authenticate(

                    //Username
                    _config.GetValue<string>("Credentials:Email:User"),

                    //Password
                    _config.GetValue<string>("Credentials:Email:Password")

                    );

                try
                {
                    //Try to send the email
                    client.Send(mm);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"There was an error processing your request. Please " +
                        $"try again later.<br />Error Message: {ex.StackTrace}";

                    return View(cvm);

                }

            }

            return View("EmailConfirmation", cvm);
        }

    }
}