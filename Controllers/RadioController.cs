using E5irProjet.Repositorys;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E5irProjet.Controllers
{
     [ApiController]
    [Route("[controller]")]
    public class RadioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Get()
        {
            var ftp_conn = new FTP_conn();
           // ftp_conn.DownloadFileFTP();
            var csvParserService = new CsvParserService();
             csvParserService.ParserWS();
            return new JsonResult("Added Successfully");
        }
    }
}
