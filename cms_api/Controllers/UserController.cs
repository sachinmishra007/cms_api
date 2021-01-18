using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using cms_api.entities;
using cms_api.services.mail.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace cms_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMailServices _mailServices;
        public UserController(
            ILogger<UserController> logger,
            IConfiguration configuration,
            IMailServices mailServices
            )
        {
            _logger = logger;
            _configuration = configuration;
            _mailServices = mailServices;
        }

        [HttpPost("sendEmail")]
        public IActionResult sendEmail(MailEntities entities)
        {
            //Informing Site Owner
            entities.message = _mailServices.getInformMessageBody(entities);
            entities.to = _configuration.GetValue<string>("email");
            var _result = _mailServices.sendNotificationMail(entities);


            // Responding back to User
            entities.message = _mailServices.getResponseMessage(entities);
            entities.to = entities.email;
            entities.subject = "[Don't Reply]: " + entities.subject;
            _mailServices.sendNotificationMail(entities);
            return Ok(_result);

        }


        [HttpGet("health")]
        public IActionResult health()
        { 
            return Ok("csm api service is running");
        }

        [HttpPost("display/{name}")]
        public IActionResult display(string name)
        {
            return Ok("Name : " + name);
        }

    }
}
