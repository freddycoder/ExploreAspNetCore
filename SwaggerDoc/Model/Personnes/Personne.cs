using System;
using System.ComponentModel;

namespace SwaggerDoc.Model.Personnes
{
    public class Personne
    {
        [DefaultValue("Frédéric")]
        public string Prenom { get; set; }

        [DefaultValue("Jacques")]
        public string Nom { get; set; }

        [DefaultValue(Genre.M)]
        public Genre Genre { get; set; }

        [DefaultValue("1994-01-21")]
        public DateTime DateNaissance { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        internal string Xml { get; set; }
    }

    public enum Genre
    {
        M = 1,
        F = 2
    }
}
