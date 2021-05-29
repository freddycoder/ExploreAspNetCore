using System;
using System.ComponentModel;

namespace ExploreAspNetCore.Model.Personnes
{
    /// <summary>
    /// Classe représentant une personne.
    /// </summary>
    public class Personne
    {
        /// <summary>
        /// Le prénom de la personne
        /// </summary>
        [DefaultValue("Frédéric")]
        public string? Prenom { get; set; }

        /// <summary>
        /// Le nom de la personne
        /// </summary>
        [DefaultValue("Jacques")]
        public string? Nom { get; set; }

        /// <summary>
        /// Le genre de la personne
        /// </summary>
        [DefaultValue(Genre.M)]
        public Genre Genre { get; set; }

        /// <summary>
        /// La date de naissance de la personne
        /// </summary>
        [DefaultValue("1994-01-21")]
        public DateTime DateNaissance { get; set; }

        [System.Xml.Serialization.XmlIgnore]
        internal string? Xml { get; set; }
    }

    /// <summary>
    /// Enumération représentant le genre d'une personne
    /// </summary>
    public enum Genre
    {
        /// <summary>
        /// Genre Masculin
        /// </summary>
        M = 1,

        /// <summary>
        /// Genre Féminin
        /// </summary>
        F = 2
    }
}
