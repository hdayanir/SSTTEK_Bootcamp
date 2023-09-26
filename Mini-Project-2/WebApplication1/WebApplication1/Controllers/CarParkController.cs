using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarParkController : ControllerBase
    {
        private readonly ILogger<CarParkController> _logger;

        public CarParkController(ILogger<CarParkController> logger)
        {
            _logger = logger;
        }
        [HttpGet("GetAllVehicles")]
        public IActionResult GetAllVehicles()
        {
            using (var context = new WebApplication1.Context.ApplicationDbContext())
            {
                var  vehicless = context.OtoparkAracLists.FirstOrDefault();
                List<OtoparkArac?>? vehicles = context.OtoparkAracLists
                    .GroupBy(p => p.ArabaSinif)
                    .Select(g => g.FirstOrDefault())
                    .ToList();
                return Ok(vehicles);
            }
        }
        [HttpGet("GetAllVehiclesByClass")]
        public IActionResult GetAllVehiclesByClass(int arabaSinif)
        {
            using (var context = new WebApplication1.Context.ApplicationDbContext())
            {
                var vehicles = context.OtoparkAracLists.Where(p => p.ArabaSinif == arabaSinif).ToList();
                return Ok(vehicles);
            }
        }
        [HttpPost("AddVehicle")]
        public IActionResult AddVehicle( string plaka, string renk,int arabaSinif, int modelYili, string modelAdi,bool otomatikPilot,decimal? aracFiyati,int? bagajHacmi,bool yedekLastik)
        {
            using (var context = new WebApplication1.Context.ApplicationDbContext())
            {
                if (arabaSinif == 1)
                {
                    if (otomatikPilot.GetType() != typeof(bool))
                    {
                        return BadRequest("Otomatik pilot bilgisi eksik veya hatalý.");

                    }
                    if (aracFiyati == null)
                    {
                        return BadRequest("Araç fiyat bilgisi eksik veya hatalý.");
                    }
                }
                else if (arabaSinif == 2)
                {
                    if (bagajHacmi == null)
                    {
                        return BadRequest("Bagaj hacmi bilgisi eksik veya hatalý.");

                    }
                    if (yedekLastik.GetType() != typeof(bool))
                    {
                        return BadRequest("Yedek lastik bilgisi eksik veya hatalý.");
                    }
                }
                if (arabaSinif != 1 && arabaSinif != 2 && arabaSinif != 3)
                {
                    return BadRequest("Sadece 1. , 2. ve 3. sýnýf araçlar girebilir.");

                }
                OtoparkArac vehicle = new OtoparkArac();
                vehicle.ArabaSinif = arabaSinif;
                vehicle.Renk = renk;
                vehicle.Plaka = plaka;
                vehicle.ModelYili = modelYili;
                vehicle.ModelAdi = modelAdi;
                vehicle.OtomatikPilot = arabaSinif == 1 ? otomatikPilot : default(bool);
                vehicle.ArabaFiyat = arabaSinif == 1 ? aracFiyati : default(decimal?);
                vehicle.BagajHacmi = arabaSinif == 2 ? bagajHacmi : default(int?); 
                vehicle.YedekLastik = arabaSinif == 2 ? yedekLastik : default(bool);
                vehicle.GirisZamani = DateTime.Now;
                vehicle.CikisZamani = default(DateTime);
                context.OtoparkAracLists.Add(vehicle);
                context.SaveChanges();

                return Ok(vehicle);
            }
        }
        [HttpPut("CarWashing")]
        public IActionResult CarWashing([FromQuery] string plaka)
        {
            using (var context = new WebApplication1.Context.ApplicationDbContext())
            {
                if (plaka == null)
                {
                    return BadRequest("Araç bilgileri eksik veya hatalý.");
                }
                var vehicle = context.OtoparkAracLists.Where(p => p.Plaka == plaka).FirstOrDefault();

                if (vehicle.ArabaSinif != 1)
                {
                    return BadRequest("Sadece 1.sýnýf araçlar yýkama hizmetinden faydalanabilir.");
                }
                else
                {
                    vehicle.ArabaYikama = true;
                    context.Update<OtoparkArac>(vehicle);
                    return Ok(vehicle);
                }
            }
        }
        [HttpPut("TireChange")]
        public IActionResult TireChange([FromQuery] string plaka)
        {
            using (var context = new WebApplication1.Context.ApplicationDbContext())
            {
                if (plaka == null)
                {
                    return BadRequest("Araç bilgileri eksik veya hatalý.");
                }
                var vehicle = context.OtoparkAracLists.Where(p => p.Plaka == plaka).FirstOrDefault();

                if (vehicle.ArabaSinif != 2)
                {
                    return BadRequest("Sadece 2.sýnýf araçlar yýkama hizmetinden faydalanabilir.");
                }
                else
                {
                    vehicle.LastikDegisimi = true;
                    context.Update<OtoparkArac>(vehicle);
                    return Ok(vehicle);
                }
            }
        }
        [HttpPut("VehicleExit")]
        public IActionResult VehicleExit([FromQuery] string plaka)
        {
            using (var context = new WebApplication1.Context.ApplicationDbContext())
            {
                if (plaka == null)
                {
                    return BadRequest("Araç bilgileri eksik veya hatalý.");
                }
                var vehicle = context.OtoparkAracLists.Where(p => p.Plaka == plaka).FirstOrDefault();

                if (vehicle.ArabaSinif == 1)
                {
                    vehicle.OtoparkUcreti = Ucret.otoparkUcretiDefault * 3;
                    vehicle.CikisZamani = DateTime.Now;
                    context.Update<OtoparkArac>(vehicle);
                    return Ok(vehicle.OtoparkUcreti);
                }
                if (vehicle.ArabaSinif == 2)
                {
                    vehicle.OtoparkUcreti = Ucret.otoparkUcretiDefault * 2;
                    vehicle.CikisZamani = DateTime.Now;
                    context.Update<OtoparkArac>(vehicle);
                    return Ok(vehicle.OtoparkUcreti);
                }
                else
                {
                    vehicle.OtoparkUcreti = Ucret.otoparkUcretiDefault;
                    vehicle.CikisZamani = DateTime.Now;
                    context.Update<OtoparkArac>(vehicle);
                    return Ok(vehicle.OtoparkUcreti);
                }
            }
        }
    }
    public class OtoparkArac
    {
        public int ArabaId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Sýnýf türü giriniz.")]
        public int ArabaSinif { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Renk giriniz.")]
        public string Renk { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Plaka giriniz.")]
        public string Plaka { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Model yýlý giriniz.")]
        public int ModelYili { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Model Adý giriniz.")]
        public string ModelAdi { get; set; }

        public int BeygirGucu { get; set; }
        public bool OtomatikPilot { get; set; }
        public decimal? ArabaFiyat { get; set; }
        public DateTime GirisZamani { get; set; }
        public DateTime? CikisZamani { get; set; }
        public int? BagajHacmi { get; set; }
        public bool YedekLastik { get; set; }
        public bool ArabaYikama { get; set; }
        public bool LastikDegisimi { get; set; }

        public decimal? OtoparkUcreti { get; set; }

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
    

}