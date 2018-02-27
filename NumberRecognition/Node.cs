using System.Collections.Generic;
namespace NumberRecognition
{
    class Node
    {
        public double value;
        public List<double> weights;
        public double bias;

        public Node(List<double> w, double b)
        {
            value = 0;
            weights = w;
            bias = b;
        }
    }
}