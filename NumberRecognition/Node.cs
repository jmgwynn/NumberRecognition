using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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