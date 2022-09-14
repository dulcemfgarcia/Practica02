using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classlibrary
{
    public class HuffmanNode
    {
        public char Character { get; set; }
        public int Frequency { get; set; }
        public HuffmanNode RightNode { get; set; }
        public HuffmanNode LeftNode { get; set; }

        public List<bool> Traverse(char symbol, List<bool> data)
        {
            if(RightNode == null && LeftNode == null)
            {
                if(symbol.Equals(this.Character))
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                List<bool> left = null;
                List<bool> right = null;

                if (LeftNode != null)
                {
                    List<bool> leftPath = new List<bool>();
                    leftPath.AddRange(data);
                    leftPath.Add(false);

                    left = LeftNode.Traverse(symbol, leftPath);
                }

                if (RightNode != null)
                {
                    List<bool> rightPath = new List<bool>();
                    rightPath.AddRange(data);
                    rightPath.Add(true);
                    right = RightNode.Traverse(symbol, rightPath);
                }

                if (left != null)
                {
                    return left;
                }
                else
                {
                    return right;
                }
            }
        }

    }
}
