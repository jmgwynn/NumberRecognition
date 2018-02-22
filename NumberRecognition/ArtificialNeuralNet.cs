using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace NumberRecognition
{
    class ArtificialNeuralNet
    {
        public List<Node> InputNodes;
        public List<Node> HiddenNodes;
        public List<Node> OutputNodes;
        public int correctAnswer = 0;
        public int givenAnswer = 0;
        public double random;
        public double reward;

        public ArtificialNeuralNet()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            InputNodes = new List<Node>();
            HiddenNodes = new List<Node>();
            OutputNodes = new List<Node>();
            for (int x =0; x < 64; x++)
            {
                InputNodes.Add(new Node(0, 0));
            }
            for (int x = 0; x < 16; x++)
            {
                HiddenNodes.Add(new Node(rand.NextDouble(), rand.NextDouble()));
            }
            for (int x = 0; x < 10; x++)
            {
                OutputNodes.Add(new Node(rand.NextDouble(), rand.NextDouble()));
            }
        }

        public void newInputs(List<int> inputs)
        {

        }

        public void processInputs()
        {
            //
            double largestValue = OutputNodes[0].value;
            int largestIndex = 0;
            for(int x = 1; x < OutputNodes.Count; x++)
            {
                if (OutputNodes[x].value > largestValue)
                {
                    largestIndex = x;
                }
            }
            givenAnswer = largestIndex;
            if(givenAnswer == correctAnswer)
            {
                rightAnswer(givenAnswer);
            }
            else
            {
                wrongAnswer(givenAnswer, correctAnswer);
            }
        }

        private void rightAnswer(int x)
        {

        }

        private void wrongAnswer(int x, int y)
        {

        }

        private double sigmoid(double x)
        {
            return 1.0f / (1.0f + Math.Exp(-x));
        }

        public void saveWeights()
        {
            string output = JsonConvert.SerializeObject(HiddenNodes);
            Console.WriteLine(output);
            File.WriteAllText(@Path.GetFullPath("HiddenNodes.txt"), output);
        }

        public void loadWeights()
        {
            string input = File.ReadAllText("HiddenNodes.txt");
            HiddenNodes = JsonConvert.DeserializeObject<List<Node>>(input);
        }
    }
}
