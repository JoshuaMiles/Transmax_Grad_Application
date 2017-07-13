using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace transmaxSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            String extractedText = RemoveEmptyLines(TextExtract());

            //var something = from person in BuildTupleList(extractedText)
                            //orderby person.Item3
                            //select person;

            var people = BuildTupleList(extractedText)
                .OrderByDescending(t =>  t.Item3).ThenBy(t => t.Item2).Select(t=>t);

            foreach(var i in people){
                Console.WriteLine(i);    
            }

        }

        //Extracts the text from the input text file
        static String TextExtract()
        {
            System.IO.MemoryStream currentDirectoryStream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(
            Directory.GetCurrentDirectory() + "/input_file-graded.txt"));

            return System.IO.File.ReadAllText(
                Directory.GetCurrentDirectory() + "/input_file-graded.txt");
        }

        // Queries the text and formats the string 
        static List<Tuple<String, String, int>> BuildTupleList(String text)
        {
            string[] stringArray = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
			List<Tuple<String, String, int>> SplitTupledList = new List<Tuple<String, String, int>>();

			foreach (String str in stringArray)
            {
                string[] splitString = str.Split(',');
                string firstName = "";
                string lastName = "";
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        lastName = splitString[0];
                    }
                    else if (i == 1)
                    {
                        firstName = splitString[1];
                    }
                    else if (i == 2)
                    {
                        Tuple<String, String, int> PersonTuple = Tuple.Create(firstName, lastName, Int32.Parse(splitString[2]));
                        
                        SplitTupledList.Add(PersonTuple);
                    }
					//firstName = ""; // Clearing so that if there is an error in 
					//lastName = ""; // reading it is easier to see
                    //TODO make a just in case method, shouldnt ever get here but handle just in case 
                }
            }
            return SplitTupledList;
        }

        static string RemoveEmptyLines(string lines)
        {
            return Regex.Replace(lines, @"^\s*$\n|\r", "", RegexOptions.Multiline).TrimEnd();
        }
    }
}