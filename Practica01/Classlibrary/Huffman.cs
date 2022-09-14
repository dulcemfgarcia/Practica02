using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Classlibrary
{
    [Serializable()]
    public class Huffman
    {
        private List<HuffmanNode> nodes = new List<HuffmanNode>();
        public HuffmanNode Root { get; set; }
        public Dictionary<char, int> Frequencies = new Dictionary<char, int>();

        public void Build(string source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (!Frequencies.ContainsKey(source[i]))
                {
                    Frequencies.Add(source[i], 0);
                }

                Frequencies[source[i]]++;
            }

            foreach (KeyValuePair<char, int> symbol in Frequencies)
            {
                nodes.Add(new HuffmanNode() { Character = symbol.Key, Frequency = symbol.Value });
            }

            while (nodes.Count > 1)
            {
                List<HuffmanNode> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<HuffmanNode>();

                if (orderedNodes.Count >= 2)
                {
                    // Take first two items
                    List<HuffmanNode> taken = orderedNodes.Take(2).ToList<HuffmanNode>();

                    // Create a parent node by combining the frequencies
                    HuffmanNode parent = new HuffmanNode()
                    {
                        Character = '*',
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        LeftNode = taken[0],
                        RightNode = taken[1]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                this.Root = nodes.FirstOrDefault();

            }

        }

        public BitArray Encode(string source)
        {
            List<bool> encodedSource = new List<bool>();

            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = this.Root.Traverse(source[i], new List<bool>());
                encodedSource.AddRange(encodedSymbol);
            }

            BitArray bits = new BitArray(encodedSource.ToArray());

            return bits;
        }

        public string Decode(BitArray bits)
        {
            HuffmanNode current = this.Root;
            string decoded = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.RightNode != null)
                    {
                        current = current.RightNode;
                    }
                }
                else
                {
                    if (current.LeftNode != null)
                    {
                        current = current.LeftNode;
                    }
                }

                if (IsLeaf(current))
                {
                    decoded += current.Character;
                    current = this.Root;
                }
            }

            return decoded;
        }

        public bool IsLeaf(HuffmanNode node)
        {
            return (node.LeftNode == null && node.RightNode == null);
        }
    }
}
