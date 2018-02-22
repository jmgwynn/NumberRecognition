using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NumberRecognition
{
    class Program
    {
        public static List<List<int>> parseInputs(string path)
        //parses the input file into a 2D list of ints to feed into the ANN one at a time.
        {
            List<List<int>> inputs = new List<List<int>>();
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"text files", path);
            string fullText = File.ReadAllText(fullPath);
            string temp = "";
            int subStringEnd = 0;
            while (fullText != "")
            {
                List<int> currentLine = new List<int>();
                subStringEnd = fullText.IndexOf("\n");
                temp = fullText.Substring(0, subStringEnd);
                for(int z = 0; z < 64; z++)
                {
                    int indexOfComma = temp.IndexOf(",");
                    currentLine.Add(Int32.Parse(temp.Substring(0, temp.IndexOf(","))));
                    temp = temp.Substring(indexOfComma + 1);
                }
                currentLine.Add(Int32.Parse(temp));
                fullText = fullText.Substring(subStringEnd + 1);
                inputs.Add(currentLine);
            }
            return inputs;
        }
        static void Main(string[] args)
        {
            string testFile = "optdigits_test.txt";
            string trainingFile = "optdigits_train.txt";
            List<List<int>> inputs = new List<List<int>>();
            ArtificialNeuralNet ANN = new ArtificialNeuralNet();

            Console.WriteLine("Train the ANN or use existing weights? (train/proceed)");
            bool proceed = false;
            while (proceed == false)
            {
                string response = Console.ReadLine();
                if (response.ToLower() == "train")
                {
                    Console.WriteLine("Parsing training document");
                    inputs = parseInputs(trainingFile);
                    proceed = true;
                }
                else if (response.ToLower() == "proceed")
                {
                    Console.WriteLine("Parsing test document");
                    inputs = parseInputs(testFile);
                    proceed = true;
                }
                else
                {
                    Console.WriteLine("Response not recognized.");
                }
            }
        }
    }
}
