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

            Action<char> printWrongCharactersMessage = (inputCharacter) =>
            {
                Console.WriteLine($"You have already entered that character: {inputCharacter}");
                Console.Write("List of incorrect characters: ");
            };

            Action<string> printWrongCountriesMessage = (input) =>
            {
                Console.WriteLine($"You have already entered that country: {input}");
                Console.Write("List of incorrect countries: ");
            };


            while (anotherGame != "n")
            {
                Random random = new Random();
                int country = random.Next(countries.Count);
                // Chosen country
                string randomCountry = countries[country];
                int length = randomCountry.Length;
                // Empty/Temp country
                StringBuilder tempCountry = new StringBuilder();
                List<char> guessedCharacters = new List<char>();

                PrintCountry(length, randomCountry, tempCountry, guessedCharacters);    
                

                int totalStrikes = 9;
                List<char> wrongCharacters = new List<char>();
                List<string> wrongInputs = new List<string>();
                
                char inputCharacter = ' ';
                
                while (totalStrikes != 0)
                {
                    string input = Console.ReadLine();
                    if (input.Length == 1)
                    {
                        inputCharacter = input[0];
                        if (Char.IsLetter(inputCharacter))
                        {
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
                                IncorrectGuess<char>(inputCharacter, wrongCharacters, ref totalStrikes, printWrongCharactersMessage);

                            Console.WriteLine(tempCountry.ToString());
                            if (tempCountry.ToString().ToLower() == randomCountry.ToLower())
                            {
                                HandleGameWon(randomCountry, ref winStreak);
                                break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("The entered character isn't a letter!");
                            continue;
                        }                       
                    }
                    else
                    {
                        if (input.Count(c => Char.IsLetter(c) || c == ' ') == input.Length)
                        {
                            if (input.ToLower() == randomCountry.ToLower())
                            {
                                HandleGameWon(randomCountry, ref winStreak);
                                break;
                            }
                            else
                                IncorrectGuess<string>(input, wrongInputs, ref totalStrikes, printWrongCountriesMessage);
                        }
                        else
                        {
                            Console.WriteLine("The input is not a word!");
                            continue;
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
        public static void PrintCountry(int length, string randomCountry, StringBuilder tempCountry, List<char> guessedCharacters)
        {
            for (int i = 0; i < length; i++)
            {
                if (Char.ToLower(randomCountry[i]) == Char.ToLower(randomCountry[0]) || Char.ToLower(randomCountry[i]) == Char.ToLower(randomCountry[length - 1]))
                {
                    tempCountry.Append(randomCountry[i]);
                    guessedCharacters.Add(Char.ToLower(randomCountry[i]));
                }
                else if (randomCountry[i] == ' ')
                    tempCountry.Append(" ");
                else
                    tempCountry.Append("-");
            }
            Console.WriteLine(tempCountry.ToString());
        }
        public static void IncorrectGuess<T>(T input, List<T> wrongInputs, ref int totalStrikes, Action<T> printInvalidMessage)
        {
            if (wrongInputs.Contains(input))
            {
                printInvalidMessage(input);                
                foreach (var item in wrongInputs)
                {
                    Console.Write(item + ", ");
                }
                Console.WriteLine();
            }
            else
            {
                wrongInputs.Add(input);
                totalStrikes--;
                Console.WriteLine($"You have {totalStrikes} strikes left.");
            }
        }
        public static void HandleGameWon(string randomCountry, ref int winStreak)
        {
            Console.WriteLine();
            Console.WriteLine("You won!");
            Console.WriteLine($"The country was: {randomCountry}");
            winStreak += 1;
            Console.WriteLine($"Current win streak: {winStreak}");
        }
    }
}
