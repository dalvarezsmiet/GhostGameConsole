using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostGame
{
    public class ComputerPlayer : GhostPlayer
    {
        private const string DEFAULT_DICTIONARY_FILE = "wordlist.txt";

        private string _name = "ComputerPlayer";
        private GhostDictionary _dictionary = new GhostDictionary(DEFAULT_DICTIONARY_FILE);

        /// <summary>
        /// The default constructor for the ComputerPlayer class.
        /// </summary>
        public ComputerPlayer()
        {
        }


        /// <summary>
        /// The name of the player.  This method is inherited from GhostPlayer.
        /// </summary>
        public virtual string Name()
        {
            return _name;
        }


        /// <summary>
        /// Find a forced win, if available.  A forced win occurs from a
        /// particular node when the all of the Children of a child node are either
        /// terminals or forced wins.
        /// </summary>
        /// <param name="node"> The node from which to find a forced win. </param>
        public virtual LetterNode ForcedWin(LetterNode node)
        {
            LetterNode winningChild = null;

            foreach (LetterNode child in node.Children().Values)
            {
                if (!child.LeafNode)
                {
                    winningChild = child;
                    foreach (LetterNode grandChild in child.Children().Values)
                    {
                        if (!(grandChild.LeafNode || (ForcedWin(grandChild) != null)))
                        {
                            winningChild = null;
                            break;
                        }
                    }
                }

                if (winningChild != null)
                {
                    break;
                }
            }

            return winningChild;
        }


        /// <summary>
        /// Find the longest word reachable from the specified node.
        /// </summary>
        /// <param name="node"> The node from which to find the longest word. </param>
        public virtual LetterNode LongestWord(LetterNode node)
        {
            LetterNode longestChild = null;

            foreach (LetterNode child in node.Children().Values)
            {
                if ((longestChild == null) || (child.MaxLength() > longestChild.MaxLength()))
                {
                    longestChild = child;
                }
            }

            return longestChild;
        }


        /// <summary>
        /// Get the player's next letter.  This method is inherited from
        /// GhostPlayer.
        /// </summary>
        /// <param name="wordInPlay"> The word currently being played. </param>
        public virtual char Play(string wordInPlay)
        {
            char nextLetter = 'x';

            LetterNode node = _dictionary.TerminalNode(wordInPlay);
            if (node != null)
            {
                LetterNode forcedWin = ForcedWin(node);
                if (forcedWin != null)
                {
                    nextLetter = forcedWin.Letter();
                }
                else
                {
                    nextLetter = LongestWord(node).Letter();
                }
            }

            return nextLetter;
        }
    }
}
