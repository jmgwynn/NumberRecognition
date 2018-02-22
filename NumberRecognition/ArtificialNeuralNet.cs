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
        public float random;
        public float reward;

        public ArtificialNeuralNet(List<Node> i, List<Node> h, List<Node> o)
        {

        }

        public void newInputs(List<float> inputs)
        {

        }

        public void processInputs()
        {
            //
            float largestValue = OutputNodes[0].value;
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

        private float sigmoid(double x)
        {
            return 1.0f / (1.0f + (float)Math.Exp(-x));
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
