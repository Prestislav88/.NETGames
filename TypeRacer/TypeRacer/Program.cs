using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;

namespace TypeRacer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> texts = new List<string>();
            try
            {
                StreamReader sr = new StreamReader("texts.txt");
                string line = sr.ReadLine();
                while (line != null)
                {
                    line = sr.ReadLine();
                    texts.Add(line);
                }
                sr.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("File does not exist!");
            }

            Random random = new Random();
            // Random index for the text
            int text = random.Next(texts.Count);
            // Chosen text
            string randomText = texts[text];
            //Prints the text that you have to write
            Console.WriteLine(randomText);

            var watch = new Stopwatch();
            string[] words = randomText.Split(" ").ToArray();

            watch.Start();
            string[] userInput = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            watch.Stop();
            var time = (watch.ElapsedMilliseconds) / 1000.0;
                     
            double allWords = words.Length;
            double correctWords = 0;
            double incorrectWords = 0;

            var wps = allWords / time;
            var wpm = wps * 60;

            if (userInput.Length == words.Length)
            {
                for (int i = 0; i < allWords; i++)
                {
                    if (words[i] == userInput[i])
                        correctWords++;
                    else
                        incorrectWords++;
                }
                Console.WriteLine();
                Console.WriteLine($"Total words: {allWords}");
                Console.WriteLine($"Mistaken words: {incorrectWords}");
                Console.WriteLine($"Word accuracy: {(correctWords / allWords) * 100:f2}%");
                // MS -> Console.WriteLine(watch.ElapsedMilliseconds);
                // Seconds -> Console.WriteLine(time);
                Console.WriteLine($"Words per minute: {wpm:f0}");
            }
            else
                Console.WriteLine("The text that you have written does not match the original text's length.");            
        }
    }
}
