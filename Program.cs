using System;
using NLog.Web;
using System.IO;
using System.Collections.Generic;

namespace MovieLibrary
{
    class Program
    {
        
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            string movieFilePath = Directory.GetCurrentDirectory() + "\\movies.csv";

            logger.Info("Program started");

            MovieFile movieFile = new MovieFile(movieFilePath);

            string choice = "";
            do
            {
                
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display All Movies");
                Console.WriteLine("Enter to quit");
                
                choice = Console.ReadLine();
                logger.Info("User choice: {Choice}", choice);

                if (choice == "1")
                {
                    
                    Movie movie = new Movie();
                    
                    Console.WriteLine("Enter movie title");
                    
                    movie.title = Console.ReadLine();
                    
                    if (movieFile.isUniqueTitle(movie.title)){
                        
                        string input;
                        do
                        {
                            
                            Console.WriteLine("Enter genre (or done to quit)");
                            
                            input = Console.ReadLine();
                            
                            if (input != "done" && input.Length > 0)
                            {
                                movie.genres.Add(input);
                            }
                        } while (input != "done");
                        
                        if (movie.genres.Count == 0)
                        {
                            movie.genres.Add("(no genres listed)");
                        }
                        
                        movieFile.AddMovie(movie);
                    }
                } else if (choice == "2")
                {
                    
                    foreach(Movie m in movieFile.Movies)
                    {
                        Console.WriteLine(m.Display());
                    }
                }
            } while (choice == "1" || choice == "2");

            logger.Info("Program ended");
        }
    }
} 
