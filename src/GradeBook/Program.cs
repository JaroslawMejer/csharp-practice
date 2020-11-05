using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    { 
        static void Main(string[] args)
        {
            IBook book = new DiskBook("JarekGradeBook");
            book.GradeAdded += OnGradeAdded;

            EnterGrades(book);
            
            var stats = book.GetStatistics();
            System.Console.WriteLine($"For the book called:{book.Name}");
            System.Console.WriteLine($"The lowest grade is: {stats.Low}");
            System.Console.WriteLine($"The highest grade is: {stats.High}");
            System.Console.WriteLine($"The average: {stats.Average}");
            System.Console.WriteLine($"The letter grade is: {stats.Letter}");
        }

        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                System.Console.WriteLine("Please add a grade or type q if You are done");
                var input = Console.ReadLine();
                if (input != "q")
                {
                    try
                    {
                        var parsedGrade = double.Parse(input);
                        book.AddGrade(parsedGrade);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        // executes code even if there is an error
                    }

                }
                else
                {
                    break;
                }
            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            System.Console.WriteLine("Grade was added");
        }

    }
}
