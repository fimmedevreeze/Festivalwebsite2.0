using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SchoolTemplate.Database;
using SchoolTemplate.Models;

namespace SchoolTemplate.Controllers
{
 public class HomeController : Controller
    {

    string connectionString = "Server= informatica.st-maartenscollege.nl;Port=3306;Database=110109;Uid=110109;Pwd=EGNARDeA;"; // voor thuis werken//
    //string connectionString = "Server= 172.16.160.21;Port=3306;Database=110109;Uid=110109;Pwd=EGNARDeA;"; // voor op school werken//

    
    public IActionResult Index()
    {           return View(GetFestival());

        }

        public IActionResult Privacy()
    {
      return View();
    }

        [Route("festival/{id}")]
    public IActionResult Festival(string id)
    {
        ViewData["id"] = id;

        return View();
    }
        [Route("info")]
    public IActionResult Information()
    {
           return View();
    }
        [Route("programma")]
    public IActionResult Programma()
        {
            return View(GetFestival());
        }
        [Route("contact")]
    public IActionResult Contact()
    {
            return View();
    }
        [Route("contact")]
        [HttpPost]
    public IActionResult Contact(PersonModel model)
    {
            return View(model);
    }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private List<Festival> GetFestival()
        {
            List<Festival> festival = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from festival", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival f = new Festival
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["Naam"].ToString(),
                            Beschrijving = reader["Beschrijving"].ToString(),
                            Datum = DateTime.Parse(reader["Datum"].ToString()),
                            Locatie = reader["Locatie"].ToString(),
                            Wrapper = reader["Wrapper"].ToString(),
                        };
                        festival.Add(f);
                    }
                }
            }

            return festival;
        }
    }
}