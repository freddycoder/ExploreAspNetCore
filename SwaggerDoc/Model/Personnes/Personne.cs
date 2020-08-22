﻿using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SwaggerDoc.Model.Personnes
{
    public class Personne
    {
        [DefaultValue("Frédéric")]
        public string Prenom { get; set; }

        [DefaultValue("Jacques")]
        public string Nom { get; set; }

        [DefaultValue("M")]
        public Genre Genre { get; set; }

        [DefaultValue("1994-01-21")]
        public DateTime DateNaissance { get; set; }
    }

    public enum Genre
    {
        M = 1,
        F = 2
    }
}
