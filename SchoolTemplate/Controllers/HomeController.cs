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

        //string connectionString = "Server= informatica.st-maartenscollege.nl;Port=3306;Database=110109;Uid=110109;Pwd=EGNARDeA;"; // voor thuis werken//
        string connectionString = "Server= 172.16.160.21;Port=3306;Database=110109;Uid=110109;Pwd=EGNARDeA;"; // voor op school werken//

        public IActionResult Index()
        {
            List<Product> products = new List<Product>();
            // products = GetProducts();

            return View(GetFestival());

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

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from product", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product p = new Product
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["Naam"].ToString(),
                            Calorieen = float.Parse(reader["calorieen"].ToString()),
                            Formaat = reader["Formaat"].ToString(),
                            Gewicht = Convert.ToInt32(reader["Gewicht"].ToString()),
                            Prijs = Decimal.Parse(reader["Prijs"].ToString())
                        };
                        products.Add(p);
                    }
                }
            }

            return products;
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
        private List<Festival> GetFestival(string id)
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

            return festival[0];
        }
    }
}
