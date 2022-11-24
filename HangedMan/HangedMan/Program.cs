using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace HangMan
{
    class Program
    {
        static void Main(string[] args)
        {        
            List<string> countries = new List<string>();
            try
            {
                StreamReader sr = new StreamReader("countries.txt");
                string line = sr.ReadLine();
                while (line != null)
                {
                    line = sr.ReadLine();
                    countries.Add(line);
                }
                sr.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("File does not exist!");
            }     
            
            Random random = new Random();
            int country = random.Next(countries.Count);
            // Chosen country
            string randomCountry = countries[country];
            int length = randomCountry.Length;
            // Empty/Temp country
            StringBuilder tempCountry = new StringBuilder();
          
            for (int i = 0; i < length; i++)
            {
                if (Char.ToLower(randomCountry[i]) == Char.ToLower(randomCountry[0]) || randomCountry[i] == randomCountry[length - 1])
                    tempCountry.Append(randomCountry[i]);
                else if (randomCountry[i] == ' ')
                    tempCountry.Append(" ");
                else 
                    tempCountry.Append("-");
                
            }
            Console.WriteLine(tempCountry.ToString());

            int totalStrikes = 9;
            List<char> wrongCharacters = new List<char>();
            while (totalStrikes != 0)
            {
                
                char inputCharacter = char.Parse(Console.ReadLine());
                bool isFound = false;
                for (int i = 0; i < length; i++)
                {
                    if (Char.ToLower(inputCharacter) == Char.ToLower(randomCountry[i]))
                    {
                        tempCountry.Replace('-', randomCountry[i], i, 1);
                        isFound = true;
                    }                
                }
                if (!isFound)
                {
                    if (wrongCharacters.Contains(inputCharacter))
                    {
                        Console.WriteLine($"You have already entered that character: {inputCharacter}");
                        Console.Write("List of incorrect characters: ");
                        foreach (char item in wrongCharacters)
                        {
                            Console.Write(item + " ");
                        }
                        Console.WriteLine();
                    }
                    else
                    {
                        wrongCharacters.Add(inputCharacter);
                        totalStrikes--;
                        Console.WriteLine($"You have {totalStrikes} strikes left.");
                    }
                    
                }
                Console.WriteLine(tempCountry.ToString());
                if (tempCountry.ToString().ToLower() == randomCountry.ToLower())
                {
                    Console.WriteLine();
                    Console.WriteLine("You won!");
                    Console.WriteLine($"The country was: {randomCountry}");
                    return;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Game Over!");
            Console.WriteLine($"The country was {randomCountry}");

        }
    }
}
