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
        {
            

            return View(GetFestival());

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("festival/{id}")]
        public IActionResult Festival(string id)
        {
            var model = GetFestival(id);


            return View(model);
        }
        [Route("Info")]
        public IActionResult Information()
        {
            return View();
        }
        [Route("Programma")]
        public IActionResult Programma()
        {
            return View(GetFestival());
        }
        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [Route("Contact")]
        [HttpPost]
        public IActionResult Contact(PersonModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            SavePerson(model);

            ViewData["formsucces"] = "ök";

            return View();
        }
        

        private void VALUES(object voornaam, object achternaam, object email, object geb_datum)
        {
            throw new NotImplementedException();
        }
        private void SavePerson(PersonModel person)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO klant(naam, achternaam, emailadres, geb_datum") VALUES(?voornaam,?achternaam,?EMail, ?geb_datum)", conn);


                cmd.Parameters.Add("?voornaam", MySqlDbType.VarChar).Value = person.Voornaam;
                cmd.Parameters.Add("?achternaam", MySqlDbType.VarChar).Value = person.Achternaam;
                cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = person.EMail;
                cmd.Parameters.Add("?geb_datum", MySqlDbType.Date).Value = person.Geboortedatum;
                cmd.ExecuteNonQuery();
            }
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
                            Prijs = Convert.ToDecimal(reader["Prijs"].ToString()),
                            Trailer = reader["Trailer"].ToString(),
                        };
                        festival.Add(f);
                    }
                }
            }

            return festival;
        }
        private Festival GetFestival(string id)
        {
            List<Festival> festival = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from festival where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival p = new Festival
                        {
                            Id = Convert.ToInt32(reader["ID"]),
                            Naam = reader["Naam"].ToString(),
                            Beschrijving = reader["Beschrijving"].ToString(),
                            Datum = DateTime.Parse(reader["Datum"].ToString()),
                            Locatie = reader["Locatie"].ToString(),
                            Wrapper = reader["Wrapper"].ToString(),
                            Prijs = Convert.ToDecimal(reader["Prijs"]),
                            Trailer = reader["Trailer"].ToString(),
                        };
                        festival.Add(p);
                    }
                }
            }

            return festival[0];
        }
    }
}
