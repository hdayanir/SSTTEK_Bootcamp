using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using static WebApplication1.Controllers.EMailController;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EMailController : ControllerBase
    {

        private readonly ILogger<EMailController> _logger;

        public EMailController(ILogger<EMailController> logger)
        {
            _logger = logger;
        }
        [HttpGet("GetEmailLists")]
        public IActionResult GetEmailLists()
        {
            using (var context = new ApplicationDbContext())
            {
                var EmailLists = context.EmailLists
                    .GroupBy(p => p.ToAdress)
                    .Select(g => g.FirstOrDefault())
                    .ToList();
                foreach (var emailLists in EmailLists)
                {
                    return Ok(EmailLists);
                }
                return Ok("Herhangi bir veri yoktur");
            }
        }
        [HttpGet("GetEmailListDetails")]
        public IActionResult Get()
        {
            using (var context = new ApplicationDbContext())
            {

                var EmailListDetails = context.EmailListDetails
                    .GroupBy(p => p.ToAdress)
                    .Select(g => g.FirstOrDefault())
                    .ToList();
                foreach (var emailListDetails in EmailListDetails)
                {
                    return Ok(EmailListDetails);
                }
                return Ok("Herhangi bir veri yoktur");
            }
        }
        [HttpPost("PostEmail")]
        public IActionResult Set(string ToAdress, string Subject, string Body)
        {
            if (!IsValidEmail(ToAdress))
            {
                return StatusCode(200, "Hatalý e mail");
            }
            using (var context = new ApplicationDbContext())
            {
                var newEmail = new EmailList
                {
                    ToAdress = ToAdress
                };
                context.EmailLists.Add(newEmail);
                context.SaveChanges();

                var newEmailDetail = new EmailListDetail
                {
                    FromAdress = FromAdress,
                    ToAdress = ToAdress,
                    Subject = Subject,
                    Body = Body,
                    SentTime = DateTime.Now
                };
                context.EmailListDetails.Add(newEmailDetail);
                context.SaveChanges();
            }
            return Ok("Veriler baþarýyla kaydedildi.");
        }
        [HttpDelete("DeleteEmail")]
        public async Task<IActionResult> Delete(string ToAdress)
        {
            if (!IsValidEmail(ToAdress))
            {
                return StatusCode(200, "Hatalý e mail");
            }
            using (var context = new ApplicationDbContext())
            {
                var email = await context.EmailLists.FirstOrDefaultAsync(e => e.ToAdress == ToAdress);
                var emailDetail = await context.EmailListDetails.FirstOrDefaultAsync(e => e.ToAdress == ToAdress);

                if (email == null && emailDetail == null)
                {
                    return Ok(ToAdress + " mail adresi listede bulunamadý");
                }
                if (email != null)
                {
                    context.EmailLists.Remove(email);
                    context.SaveChanges();
                }
                if (emailDetail != null)
                {
                    context.EmailListDetails.Remove(emailDetail);
                    context.SaveChanges();
                }
                return Ok(ToAdress + " mail adresi listeden baþarýyla silindi.");
            }
        }

        public class EmailList
        {
            public int EmailListId { get; set; }
            public string ToAdress { get; set; }
        }
        public class EmailListDetail
        {
            public int EmailListDetailId { get; set; }
            public string FromAdress { get; set; }
            public string ToAdress { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public DateTime SentTime { get; set; }
        }
        public class ApplicationDbContext : DbContext
        {
            public DbSet<EmailList> EmailLists { get; set; }
            public DbSet<EmailListDetail> EmailListDetails { get; set; }

        }
        public static bool IsValidEmail(string email)
        {
            // Mail adresini kontrol etmek için bir regex kullanýlýyor.
            string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
            return Regex.IsMatch(email, pattern);
        }
        public const string FromAdress = "hsyndayanir@gmail.com";
    }
}