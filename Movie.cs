using System;
using System.Collections.Generic;

namespace MovieLibrary
{
    public class Movie
    {
        public UInt64 movieId { get; set; }
        
        string _title;
        public string title
        {
            get
            {
                return this._title;
            }
            set
            {
               
                this._title = value.IndexOf(',') != -1 ? $"\"{value}\"" : value;
            }
        }
        public List<string> genres { get; set; }

      
        public Movie()
        {
            genres = new List<string>();
        }

        public string Display()
        {
            return $"Id: {movieId}\nTitle: {title}\nGenres: {string.Join(", ", genres)}\n";
        }
    }
}
