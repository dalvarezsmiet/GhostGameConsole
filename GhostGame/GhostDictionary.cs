using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace GhostGame
{
    public class GhostDictionary
    {
        private static int MIN_WORD_LENGTH = 4;

        private Dictionary<char, LetterNode> _words = new Dictionary<char, LetterNode>();

        /// <summary>
        /// Add a word to the dictionary if it meets two criteria:
        /// <ul>
        ///   <li> The word must be at least MIN_WORD_LENGTH letters long.
        ///   <li> The word must not start with another valid word.
        /// </ul>
        /// The second criteria is enforced by the LetterNode class.
        /// </summary>
        /// <param name="word"> The word to add to the dictionary. </param>
        private void AddWord(string word)
        {
            if (word.Length >= MIN_WORD_LENGTH)
            {
                char key = Convert.ToChar(word[0]);
                LetterNode node = null;
                
                if (_words.ContainsKey(key))    
                    node = _words[key];

                if (node == null)
                    _words[key] = new LetterNode(word);
                else
                    node.AddWord(word);
            }
        }

        /// <summary>
        /// The file constructor for the GhostDictionary class.
        /// </summary>
        /// <param name="fileName"> The name of the dictionary file.  This file is
        ///                 assumed to be sorted in alphabetical order. </param>
        public GhostDictionary(string fileName)
        {
            //Thread.Sleep(30000);

            try
            {
                StreamReader reader = new StreamReader(fileName);
                string word = null;
                while ((word = reader.ReadLine()) != null)
                {
                    AddWord(word);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File " + fileName + " not found.");
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading file:  " + ex.ToString());
            }
        }

        /// <summary>
        /// Return the number of words in the dictionary.
        /// </summary>
        public virtual int Size()
        {
            int count = 0;

            foreach (LetterNode node in _words.Values)
            {
                count += node.LeafNodeCount();
            }

            return count;
        }

        /// <summary>
        /// Return the length of the longest string in the dictionary.
        /// </summary>
        public virtual int MaxLength()
        {
            int length = 0;
            int maximumLength = 0;

            foreach (LetterNode node in _words.Values)
            {
                length = node.MaxLength();
                if (length > maximumLength)
                {
                    maximumLength = length;
                }
            }

            return maximumLength;
        }

        /// <summary>
        /// Find the node matching the string, if any.
        /// </summary>
        /// <param name="string"> The string to look for. </param>
        public virtual LetterNode TerminalNode(string @string)
        {
            char key = Convert.ToChar(@string[0]);

            LetterNode startNode = null;
            if (!_words.ContainsKey(key))
                return null;
            else
                startNode = _words[key];

            LetterNode node = startNode;

            for (int i = 1; i < @string.Length; i++)
            {
                node = node.Child(@string[i]);
                if (node == null)
                {
                    break;
                }
            }

            return node;
        }

        /// <summary>
        /// Determine if the word is in the dictionary.
        /// </summary>
        /// <param name="word"> The word to look for. </param>
        public virtual bool IsFullWord(string word)
        {
            bool isFullWord = false;

            LetterNode node = TerminalNode(word);
            if ((node != null) && (node.LeafNode))
            {
                isFullWord = true;
            }

            return isFullWord;
        }

        /// <summary>
        /// Determine if the string is the start of a word in the dictionary.
        /// </summary>
        /// <param name="stem"> The substring to look for. </param>
        public virtual bool IsWordStem(string stem)
        {
            bool isWordStem = false;

            LetterNode node = TerminalNode(stem);
            if (node != null)
            {
                isWordStem = true;
            }

            return isWordStem;
        }

        /// <summary>
        /// A test harness for the GhostDictionary class.
        /// </summary>
        /// <param name="args"> The command line arguments passed in. </param>
        public static void TestGhostDictionary(string[] args)
        {
            if (args.Length == 1)
            {
                GhostDictionary dictionary = new GhostDictionary(args[0]);

                Console.WriteLine("Created dictionary with " + dictionary.Size() + " words, the longest having " + dictionary.MaxLength() + " letters.");
            }
            else
            {
                Console.WriteLine("Usage:  TestGhostDictionary " + typeof(GhostDictionary).FullName + " <word-list>");
            }
        }
    }
}
