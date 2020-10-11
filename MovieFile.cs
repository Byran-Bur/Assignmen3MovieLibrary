using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace MovieLibrary
{
    public class MovieFile
    {
        
        public string filePath { get; set; }
        public List<Movie> Movies { get; set; }
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

                public MovieFile(string movieFilePath)
        {
            filePath = movieFilePath;
            Movies = new List<Movie>();


            try
            {
                StreamReader sr = new StreamReader(filePath);

                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                   
                    Movie movie = new Movie();
                    string line = sr.ReadLine();
                   
                    int idx = line.IndexOf('"');
                    if (idx == -1)
                    {
                       
                        string[] movieDetails = line.Split(',');
                        movie.movieId = UInt64.Parse(movieDetails[0]);
                        movie.title = movieDetails[1];
                        movie.genres = movieDetails[2].Split('|').ToList();
                    }
                    else
                    {
                        
                        movie.movieId = UInt64.Parse(line.Substring(0, idx - 1));
                       
                        line = line.Substring(idx + 1);
                        
                        idx = line.IndexOf('"');
                       
                        movie.title = line.Substring(0, idx);
                       
                        line = line.Substring(idx + 2);
                        
                        movie.genres = line.Split('|').ToList();
                    }
                    Movies.Add(movie);
                }
                
                sr.Close();
                logger.Info("Movies in file {Count}", Movies.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public bool isUniqueTitle(string title)
        {
            if (Movies.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate movie title {Title}", title);
                return false;
            }
            return true;
        }

        public void AddMovie(Movie movie)
        {
            try
            {
              
                movie.movieId = Movies.Max(m => m.movieId) + 1;
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{movie.movieId},{movie.title},{string.Join("|", movie.genres)}");
                sw.Close();
                
                Movies.Add(movie);
              
                logger.Info("Movie id {Id} added", movie.movieId);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
