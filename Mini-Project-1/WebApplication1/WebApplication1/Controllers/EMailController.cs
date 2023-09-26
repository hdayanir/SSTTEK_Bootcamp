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
        

        public class OtoparkArac
        {
            public int ArabaId { get; set; }
            public int ArabaSinif { get; set; }
            public string Renk { get; set; }
            public string Plaka { get; set; }
            public int ModelYili { get; set; }
            public string ModelAdi { get; set; }
            public int BeygirGucu { get; set; }
            public bool OtomatikPilot { get; set; }
            public decimal? ArabaFiyat { get; set; }
            public DateTime GirisZamani { get; set; }
            public DateTime? CikisZamani { get; set; }
            public int? BagajHacmi { get; set; }
            public bool YedekLastik { get; set; }
            public decimal? OtoparkUcreti { get; set; }

        }

        public class ApplicationDbContext : System.Data.Entity.DbContext
        {
            public System.Data.Entity.DbSet<OtoparkArac> OtoparkAracLists { get; set; }
        } 
        public class Arac
        {
            public string Renk { get; set; }
            public string Plaka { get; set; }
            public int ModelYili { get; set; }
            public string ModelAdi { get; set; }
        }
        public class BirinciSinifArac : Arac
        {
            public bool OtomatikPilot { get; set; }
            public decimal Fiyat { get; set; }
        }
        public class IkinciSinifArac : Arac
        {
            public decimal BagajHacmi { get; set; }
            public bool YedekLastik { get; set; }
        }
        [HttpGet("GetEmailLists")]
        public IActionResult GetEmailListsa()
        {
            using (var context = new ApplicationDbContext())
            {
                var EmailLists = context.OtoparkAracLists
                    .GroupBy(p => p.ArabaId)
                    .Select(g => g.FirstOrDefault())
                    .ToList();
                foreach (var emailLists in EmailLists)
                {
                    return Ok(EmailLists);
                }
                return Ok("Herhangi bir veri yoktur");
            }
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