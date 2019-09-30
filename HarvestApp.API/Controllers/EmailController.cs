using System;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using HarvestApp.API.Helpers;
using Microsoft.Extensions.Options;
using HarvestApp.API.Dtos;
using HarvestApp.API.Data;
using AutoMapper;
using System.Security.Claims;
using HarvestApp.API.Models;
using System.Linq;

namespace HarvestApp.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepository _repoEmail;
        private readonly IDatingRepository _repo;
        private readonly IOptions<GmailSettings> _gmailConfig;
        private readonly string _userName;
        private readonly string _password;

        public EmailController (IOptions<GmailSettings> gmailConfig, IDatingRepository repo, IEmailRepository repoEmail)
        {
        
            _gmailConfig = gmailConfig;
            _userName = _gmailConfig.Value.UserName;
            _password = _gmailConfig.Value.ApiSecret;
            _repo = repo;
            _repoEmail = repoEmail;
        }


        [HttpPost]
        public async Task<IActionResult> SendEmail(int userId, EmailDataDto emailDataDto)
        {
            var isCurrentUser = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) == userId;

            var sender = await _repo.GetUser(userId, isCurrentUser);
            var photoUrl = GetPropertyValue(sender,"Photos");
            var realPhotoUrl = GetPropertyValue(photoUrl,"Url");

            if (sender.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
          
            var recipient = await _repoEmail.GetEmailUser(emailDataDto.RecipientId);

            if(recipient == null)
                return BadRequest("Could not find user");

            if(recipient.Email==null)
                return BadRequest("User does not have an email address");

            var message = new MimeMessage ();
            message.From.Add (new MailboxAddress ("Harvest Application", _userName));
            message.To.Add (new MailboxAddress ("Harvest Application", recipient.Email));
            message.Subject = "HarvestApp: New Message from " + sender.KnownAs;

            message.Body = new TextPart ("html") {

                Text = "<table><tr><td align=center><img src=" + photoUrl +"><tr><td>" + "<b>Hi " + recipient.KnownAs +  ", </b><br><br>" + "You have a new message from " + sender.KnownAs +  ": <br><br>" + emailDataDto.Content 
                + "<br><br> Now it begins...<div class='form-group text-center'><a href='https://harvestapp.azurewebsites.net/'><button>Begin</button></a></div>"
                +"<tr><td align=center>"+ "<br><br> <b>The Harvest, 2019 </b> <br> Orange County, CA" + "</table>"

            };

            using (var client = new SmtpClient ()) {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s,c,h,e) => true;

                client.Connect ("smtp.gmail.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate (_userName, _password);

                client.Send (message);
                client.Disconnect (true);

            }

            return Ok();
        }



        public static object GetPropertyValue(object src, string propName)
        {
            if (src == null) throw new ArgumentException("Value cannot be null.", "src");
            if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

            if(propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else
            {
                var prop = src.GetType().GetProperty(propName);
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }

    }
    
}