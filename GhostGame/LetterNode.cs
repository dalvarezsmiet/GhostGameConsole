using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostGame
{
    public class LetterNode
    {
        //private static Logger logger_ = Logger.getLogger(LetterNode.class.getName());
        private char _letter;
        private Dictionary<char?, LetterNode> _nodes = new Dictionary<char?, LetterNode>();


        /**
         * The full constructor for the LetterNode class.
         *
         * @param word The word, or substring, to add to this node.
         */
        public LetterNode(string word)
        {
            if (word.Length > 0)
                _letter = word[0];

            if (word.Length > 1)
                _nodes[Convert.ToChar(word[1])] = new LetterNode(word.Substring(1));
        }

        /// <summary>
        /// Add a word to the tree rooted at this node.  This method implements
        /// the Ghost rule that no word may begin with another word.  That
        /// requires two rules:
        /// 
        ///   - If this node has no child nodes, the passed word begins with
        ///     this word and must not be added.
        ///   - If the passed word has a length of zero, this is the end of the
        ///     word and all child nodes must be deleted.
        /// </summary>
        /// <param name="word"> The word, or substring, to add to this node. </param>
        public void AddWord(string word)
        {
            if (word[0] == _letter)
            {
                if (_nodes.Count > 0)
                {
                    if (word.Length == 1)
                        _nodes.Clear();
                    else
                    {
                        char key = Convert.ToChar(word[1]);

                        LetterNode nextNode = null;
                        if (_nodes.ContainsKey(key))
                            nextNode = _nodes[key];

                        if (nextNode == null)
                            _nodes[key] = new LetterNode(word.Substring(1));
                        else
                            nextNode.AddWord(word.Substring(1));
                    }
                }
            }
            else
                throw new ArgumentException("Invalid first letter.");
        }

        /// <summary>
        /// Determine the maximum length of a word reachable from this node.
        /// </summary>
        public virtual int MaxLength()
        {
            int length = 0;
            int maxLength = 0;

            foreach (LetterNode node in _nodes.Values)
            {
                length = node.MaxLength();
                if (length > maxLength)
                {
                    maxLength = length;
                }
            }

            return maxLength + 1;
        }


        /// <summary>
        /// Determine if this is a leaf node.
        /// </summary>
        public virtual bool LeafNode
        {
            get
            {
                return _nodes.Count == 0;
            }
        }


        /// <summary>
        /// Determine the number of leaf nodes reachable from this node.
        /// </summary>
        public virtual int LeafNodeCount()
        {
            int leaves = 0;

            if (LeafNode)
            {
                leaves = 1;
            }
            else
            {
                foreach (LetterNode node in _nodes.Values)
                {
                    leaves += node.LeafNodeCount();
                }
            }

            return leaves;
        }


        /// <summary>
        /// Return the child node mapped to the character, if any.
        /// </summary>
        /// <param name="letter"> The character of the child node. </param>
        public virtual LetterNode Child(char letter)
        {
            char key = Convert.ToChar(letter);

            if (_nodes.ContainsKey(key))
                return _nodes[key];
            return null;
        }


        /// <summary>
        /// Letter accessor.
        /// </summary>
        public virtual char Letter()
        {
            return _letter;
        }


        /// <summary>
        /// Children accessor.
        /// </summary>
        public virtual Dictionary<char?, LetterNode> Children()
        {
            return _nodes;
        }


        /// <summary>
        /// Standard method inherited from Object.
        /// </summary>
        /// <param name="object"> The object to compare for equality. </param>
        public override bool Equals(object @object)
        {
            return (@object == this) || ((@object != null) && (@object is LetterNode) && (_letter == ((LetterNode)@object)._letter));
        }


        /// <summary>
        /// Standard method inherited from Object.  Algorithm from Josh Bloch's
        /// </summary>
        /// <returns> The hashcode for this instance of the LetterNode class. </returns>
        public override int GetHashCode()
        {
            int result = 17;

            result = 37 * result + (int)_letter;

            return result;
        }


        /// <summary>
        /// A test harness for the LetterNode class.
        /// </summary>
        /// <param name="args"> The command line arguments passed in. </param>
        public static void TestLetterNode(string[] args)
        {
            if (args.Length > 0)
            {
                LetterNode node = new LetterNode(args[0]);

                for (int i = 1; i < args.Length; i++)
                {
                    node.AddWord(args[i]);
                }

                Console.Write("Total words = " + node.LeafNodeCount());
                Console.Write("Maximum word length = " + node.MaxLength());
            }
            else
            {
                Console.Write("Usage:  GhostGame " + typeof(LetterNode).FullName + " <word-list>");
            }
        }
    }
}
