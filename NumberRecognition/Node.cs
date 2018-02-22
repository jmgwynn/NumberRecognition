using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberRecognition
{
    class Node
    {
        public float value;
        public float weight;
        public float bias;

        public Node(float w, float b)
        {
            value = 0;
            weight = w;
            bias = b;
        }
    }
}
