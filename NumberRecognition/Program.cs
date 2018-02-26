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
                for (int z = 0; z < 64; z++)
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
            List<List<int>> trainingInputs = new List<List<int>>();
            List<List<int>> testInputs = new List<List<int>>();
            Console.WriteLine("Parsing training document");
            trainingInputs = parseInputs(trainingFile);
            Console.WriteLine("Parsing test document");
            testInputs = parseInputs(testFile);
            ArtificialNeuralNet ANN = new ArtificialNeuralNet();

            Console.WriteLine("Create a new Artifical Neural Net, or load a saved one? (new/load)");
            bool proceed = false;
            while (!proceed)
            {
                string response = Console.ReadLine();
                if (response.ToLower() == "new")
                {
                    proceed = true;
                }
                else if (response.ToLower() == "load")
                {
                    ANN.loadWeightsAndBiases();
                    proceed = true;
                }
                else if (response.ToLower() == "exit")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Response not recognized.");
                }
            }
            proceed = false;
            Console.WriteLine("Train the ANN or test it? (train/test)");
            while (!proceed)
            {
                string response = Console.ReadLine();
                if (response.ToLower() == "train")
                {
                    Console.WriteLine("Training");
                    foreach (List<int> l in trainingInputs)
                    {
                        ANN.processInputs(l);
                    }
                    Console.WriteLine("Train again, or test? (train/test)");
                }
                else if (response.ToLower() == "test")
                {
                    foreach (List<int> l in testInputs)
                    {
                        ANN.processInputs(l);
                    }
                    Console.WriteLine("Train more, test more, save values? (train/test/save)");
                }
                else if (response.ToLower() == "save")
                {
                    ANN.saveWeightsAndBiases();
                }
                else if (response.ToLower() == "exit")
                {
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Response not recognized.");
                }
            }
        }
    }
}