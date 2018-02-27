using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace NumberRecognition
{
    class ArtificialNeuralNet
    {
        private List<Node> InputNodes;
        private List<Node> HiddenNodes;
        private List<Node> OutputNodes;
        private int correctAnswer;
        private int givenAnswer;
        public int numberCorrect = 0;
        private int numOfHidden = 30;

        public ArtificialNeuralNet(Random rand)
        {
            /*Initialization of the ANN.
			Weights and Biases are initialized as random values between -1 and 1.
			*/
            InputNodes = new List<Node>();
            HiddenNodes = new List<Node>();
            OutputNodes = new List<Node>();
            for (int x = 0; x < 64; x++)
            {
                InputNodes.Add(new Node(new List<double>(), 0));
            }
            for (int x = 0; x < numOfHidden; x++)
            {
                List<double> weights = new List<double>();
                for (int y = 0; y < 64; y++)
                {
                    weights.Add(((rand.NextDouble() * 2) - 1));
                }
                HiddenNodes.Add(new Node(weights, ((rand.NextDouble() * 2) - 1)));
            }
            for (int x = 0; x < 10; x++)
            {
                List<double> weights = new List<double>();
                for (int y = 0; y < numOfHidden; y++)
                {
                    weights.Add(((rand.NextDouble() * 2) - 1));
                }
                OutputNodes.Add(new Node(weights, ((rand.NextDouble() * 2) - 1)));
            }
        }

        public void processInputs(List<int> inputs, bool train)
        {
            for (int x = 0; x < 64; x++)
            {
                InputNodes[x].value = inputs[x];
            }
            correctAnswer = inputs[64];
            foreach (Node n in HiddenNodes)
            {
                calculateValue(n);
            }
            foreach (Node n in OutputNodes)
            {
                calculateValue(n);
            }
            double largestValue = OutputNodes[0].value;
            int largestIndex = 0;
            for (int x = 1; x < OutputNodes.Count; x++)
            {
                if (OutputNodes[x].value > largestValue)
                {
                    largestIndex = x;
                    largestValue = OutputNodes[x].value;
                }
            }
            givenAnswer = largestIndex;
            
            if (train)
            {
                backPropagate();
            }
            else
            {
                if (givenAnswer == correctAnswer)
                {
                    numberCorrect++;
                }
            }
            //Console.WriteLine("Given Answer: " + givenAnswer + ". Correct Answer: " + correctAnswer + ".");
        }

        private void backPropagate()
        {
            double learningRate = 0.1252;
            double globalError = 0.0;
            for(int x = 0; x < 10; x++)
            {
                double desiredOutput = 0.0;
                if(correctAnswer == x)
                {
                    desiredOutput = 1.0;
                }
                globalError = sigmoidPrime(OutputNodes[x].value) * (desiredOutput - OutputNodes[x].value);
                for(int y = 0; y < numOfHidden; y++)
                {
                    OutputNodes[x].weights[y] += (globalError * HiddenNodes[y].value * learningRate);
                }
                OutputNodes[x].bias += globalError * learningRate;
                for(int y = 0; y < numOfHidden; y++)
                {
                    double localError = sigmoidPrime(HiddenNodes[y].value) * globalError * OutputNodes[x].weights[y];
                    HiddenNodes[y].bias += localError * learningRate;
                    for(int z = 0; z< 64; z++)
                    {
                        HiddenNodes[y].weights[z] += (localError * InputNodes[z].value * learningRate);
                    }
                }
            }
        }

        private void calculateValue(Node n)
        {
            double calculatedValue = 0;
            if (n.weights.Count == 64)
            {
                for (int x = 0; x < 64; x++)
                {
                    calculatedValue -= (n.weights[x] * InputNodes[x].value);
                }
            }
            else
            {
                for (int x = 0; x < numOfHidden; x++)
                {
                    calculatedValue -= (n.weights[x] * HiddenNodes[x].value);
                }
            }
            calculatedValue -= n.bias;
            n.value = sigmoid(calculatedValue);
        }

        private double sigmoid(double x)
        {
            return 1.0f / (1.0f + Math.Exp(x));
        }

        private double sigmoidPrime(double x)
        {
            return sigmoid(x) * (1 - sigmoid(x));
        }

        public void saveWeightsAndBiases()
        {
            string output0 = JsonConvert.SerializeObject(HiddenNodes);
            string output1 = JsonConvert.SerializeObject(OutputNodes);
            File.WriteAllText(@Path.GetFullPath("HiddenNodes.txt"), output0);
            File.WriteAllText(@Path.GetFullPath("OutputNodes.txt"), output1);
        }

        public void loadWeightsAndBiases()
        {
            string input0 = File.ReadAllText("HiddenNodes.txt");
            string input1 = File.ReadAllText("OutputNodes.txt");
            HiddenNodes = JsonConvert.DeserializeObject<List<Node>>(input0);
            OutputNodes = JsonConvert.DeserializeObject<List<Node>>(input1);
        }
    }
}