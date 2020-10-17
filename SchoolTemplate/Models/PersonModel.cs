using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolTemplate.Models
{
  public class PersonModel
  {
    public string Voornaam { get; set; }

    [Required(ErrorMessage = "Achternaam is verplicht")]
    public string Achternaam { get; set; }
    [Required(ErrorMessage = "E-Mail adres is verplicht")]
    [EmailAddress(ErrorMessage ="E-Mail adres is niet geldig")]
    public string EMail { get; set; }

    public DateTime Geboortedatum { get; set; }
    }
}

private void SavePerson(PersonModel person)
{
    using (MySqlConnection conn = new MySqlConnection(connectionString))
    {
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("INSERT INTO klant(naam, achternaam, emailadres, geb_datum") VALUES(?voornaam,?achternaam,?email, ?geb_datum)", conn);
    }
}