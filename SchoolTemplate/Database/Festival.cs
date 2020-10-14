using System;

namespace SchoolTemplate.Database
{
  public class Festival
  {
        internal string Locatie;
        internal string Wrapper;

        public int Id { get; set; }
    
    public string Naam { get; set; }

    public string Beschrijving { get; set; }    

    public DateTime Datum { get; set; }

  }
}
