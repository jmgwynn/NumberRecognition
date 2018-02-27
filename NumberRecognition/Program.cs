using System;
using System.Collections.Generic;
using System.IO;

namespace NumberRecognition
{
    static class Program
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

        public static void Shuffle<T>(this IList<T> list, Random rng)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
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
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            ArtificialNeuralNet ANN = new ArtificialNeuralNet(rand);
            //ANN.loadWeightsAndBiases();

            int epoch = 0;
            foreach (List<int> l in testInputs)
            {
                ANN.processInputs(l, false);
            }
            Console.WriteLine("Epoch -1. Accuracy: " + (ANN.numberCorrect / 1797.0));
            ANN.numberCorrect = 0;
            while (epoch < 30)
            {
                Shuffle<List<int>>(trainingInputs, rand);
                foreach (List<int> l in trainingInputs)
                {
                    ANN.processInputs(l, true);
                }
                foreach (List<int> l in testInputs)
                {
                    ANN.processInputs(l, false);
                }
                Console.WriteLine("Epoch "+epoch+". Accuracy: " + (ANN.numberCorrect/1797.0));
                ANN.numberCorrect = 0;
                epoch++;
            }
            string input = Console.ReadLine();
            bool loop = true;
            while (loop)
            {
                if(input.ToLower() == "exit")
                {
                    loop = false;
                }
            }
        }
    }
}