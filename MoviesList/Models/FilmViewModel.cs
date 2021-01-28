using Microsoft.CodeAnalysis.CSharp.Syntax;
using StarWarsApiCSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MoviesList.Models
{
    public class FilmViewModel : Film
    {
        public int Score { get; set; }
        public double? AverageScore { get; set; }
        public int Id 
        {
            get
            {
                if (!id.HasValue)
                    id = GetFilmId(Url);
                return id.Value;
            }
        }
        private int? id;

        // Get film id from url
        // Example: "https://swapi.dev/api/films/1/" => 1
        private int GetFilmId(string url)
        {
            // Check if there is '/' at the end of input and remove if so
            url = url.EndsWith('/') ? url.Remove(url.Length - 1) : url;
            string idString = url.Substring(url.LastIndexOf('/') + 1);
            if (!int.TryParse(idString, out int id))
                throw new Exception("Url has invalid format");
            return id;
        }
    }
}
