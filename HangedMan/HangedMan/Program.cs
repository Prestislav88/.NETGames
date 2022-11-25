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

            string anotherGame = "";
            int winStreak = 0;

            while (anotherGame != "n")
            {
                Random random = new Random();
                int country = random.Next(countries.Count);
                // Chosen country
                string randomCountry = countries[country];
                int length = randomCountry.Length;
                // Empty/Temp country
                StringBuilder tempCountry = new StringBuilder();

                for (int i = 0; i < length; i++)
                {
                    if (Char.ToLower(randomCountry[i]) == Char.ToLower(randomCountry[0]) || Char.ToLower(randomCountry[i]) == Char.ToLower(randomCountry[length - 1]))
                        tempCountry.Append(randomCountry[i]);
                    else if (randomCountry[i] == ' ')
                        tempCountry.Append(" ");
                    else
                        tempCountry.Append("-");
                }
                Console.WriteLine(tempCountry.ToString());

                int totalStrikes = 9;
                List<char> wrongCharacters = new List<char>();
                List<char> guessedCharacters = new List<char>();
                char inputCharacter = ' ';
                

                while (totalStrikes != 0)
                {
                    string input = Console.ReadLine();
                    if (input.Length == 1)
                    {
                        inputCharacter = input[0];
                        if (guessedCharacters.Contains(inputCharacter))
                        {
                            Console.WriteLine($"Character {inputCharacter} has already been guessed.");
                        }
                        bool isFound = false;
                        for (int i = 0; i < length; i++)
                        {
                            if (Char.ToLower(inputCharacter) == Char.ToLower(randomCountry[i]))
                            {
                                tempCountry.Replace('-', randomCountry[i], i, 1);
                                guessedCharacters.Add(inputCharacter);
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
                            winStreak += 1;
                            Console.WriteLine($"Current win streak: {winStreak}");
                            break;
                        }
                    }
                    else
                    {
                        if (input.ToLower() == randomCountry.ToLower())
                        {
                            Console.WriteLine("You won!");
                            Console.WriteLine($"The country was: {randomCountry}");
                            winStreak += 1;
                            Console.WriteLine($"Current win streak: {winStreak}");
                            break;
                        }
                        else
                        {
                            totalStrikes--;
                            Console.WriteLine($"You have {totalStrikes} strikes left.");
                        }

                    }
                }
                if (totalStrikes == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Game Over!");
                    Console.WriteLine($"The country was {randomCountry}");
                    winStreak = 0;
                }
                Console.WriteLine("Do you want to play one more game? - [Y/n]");
                anotherGame = Console.ReadLine();
            }
        }
    }
}
