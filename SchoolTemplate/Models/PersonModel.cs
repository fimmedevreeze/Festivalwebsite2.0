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