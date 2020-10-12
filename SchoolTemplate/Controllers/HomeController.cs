﻿using System;
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
  
    string connectionString = "Server= informatica.st-maartenscollege.nl;Port=3306;Database=110109;Uid=110109;Pwd=EGNARDeA;";

    public IActionResult Index()
    {
      List<Product> products = new List<Product>();
      // products = GetProducts();

      return View(products);
    }  

    public IActionResult Privacy()
    {
      return View();
    }

        [Route("festival/(id)")]
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
           return View();
    }
        [Route("contact")]
    public IActionResult Contact()
    {
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

    private List<Product> GetFestivals()
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from festival", conn);

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
    }
}