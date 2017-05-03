/**
 * GhostGame is the main class for playing an optimal game of Ghost.  The
* puzzle description from ITA's website is:
* <br>
* In the game of Ghost, two players take turns building up a word from
* left to right.  Each player contributes one letter per turn. The goal
* is to not complete the spelling of a word:  if you add a letter that
* completes a word (of 4+ letters), or if you add a letter that
* produces a string that cannot be extended into a word, you lose.
* (Bluffing plays and "challenges" may be ignored for the purpose of
* this puzzle.)
* <br>
* Write a program that plays Ghost optimally against a human, given the
* following dictionary: WORD.LST. Allow the human to play first.  If
* the computer thinks it will win, it should play randomly among all
* its winning moves; if the computer thinks it will lose, it should
* play so as to extend the game as long as possible (choosing randomly
* among choices that force the maximal game length).  A simple console
* UI will suffice.
* <br>
* This source code is copyright 2012 by Patrick May.  All rights
* reserved.
*
* @author Patrick May (patrick.may@mac.com)
* @author &copy; 2012 Patrick May.  All rights reserved.
* @version 2
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GhostGame
{
    public class GhostGame
    {
        private GhostDictionary _dictionary = null;
        private GhostPlayer[] _players = new GhostPlayer[2];
        private int _currentPlayerIndex = 0;
        private StringBuilder _wordInPlay = new StringBuilder();
        private bool _gameOver = false;
        private GhostPlayer _winner = null;

        /// <summary>
        /// The full constructor for the GhostGame class.
        /// </summary>
        /// <param name="fileName"> The name of the dictionary file. </param>
        /// <param name="playerOne"> A reference to the first player. </param>
        /// <param name="playerTwo"> A reference to the second player. </param>
        public GhostGame(string fileName, GhostPlayer playerOne, GhostPlayer playerTwo)
        {
            _dictionary = new GhostDictionary(fileName);
            if (_dictionary.Size() == 0)
            {
                throw new Exception("Unable to load dictionary from " + fileName + ".");
            }

            _players[0] = playerOne;
            _players[1] = playerTwo;
        }


        /// <summary>
        /// Switch the current player.
        /// </summary>
        /// <returns> The updated current player index. </returns>
        private int SwitchPlayer()
        {
            return (_currentPlayerIndex = ++_currentPlayerIndex % 2);
        }


        /// <summary>
        /// Add a letter to the word in play.
        /// </summary>
        private void AddLetter(char letter)
        {
            _wordInPlay.Append(letter);

            if (_dictionary.IsFullWord(_wordInPlay.ToString()) || !_dictionary.IsWordStem(_wordInPlay.ToString()))
            {
                _gameOver = true;
                _winner = _players[SwitchPlayer()];
            }
        }


        /// <summary>
        /// Play the game.
        /// </summary>
        /// <returns> The winning player. </returns>
        public virtual GhostPlayer Play()
        {
            while (!_gameOver)
            {
                AddLetter(_players[_currentPlayerIndex].Play(_wordInPlay.ToString()));
                SwitchPlayer();
            }

            return _winner;
        }


        /// <summary>
        /// Return the word in play.
        /// </summary>
        public virtual string WordInPlay()
        {
            return _wordInPlay.ToString();
        }


        /// <summary>
        /// A test harness for the GhostGame class.
        /// </summary>
        /// <param name="args"> The command line arguments passed in. </param>f
        public static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                GhostGame game = null;
                Type typePlayer1 = GetPlayerType(args[1]);
                Type typePlayer2 = GetPlayerType(args[2]);

                try
                {
                    GhostPlayer playerOne = (GhostPlayer)Activator.CreateInstance(typePlayer1);
                    GhostPlayer playerTwo = (GhostPlayer)Activator.CreateInstance(typePlayer2);

                    game = new GhostGame(args[0], playerOne, playerTwo);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unable to initialize GhostGame:  " + e.ToString());
                }

                GhostPlayer winner = game.Play();

                Console.WriteLine("Final word:  " + game.WordInPlay() + ", " + winner.Name() + " wins!");
            }
            else
            {
                Console.WriteLine("Usage:  GhostGame" + typeof(GhostGame).FullName + " <word-list> <player-one> <player-two>");
            }
        }

        private static Type GetPlayerType(string argument)
        {
            if (argument.ToUpper().Equals("C"))
                return typeof(ComputerPlayer);
            else if (argument.ToUpper().Equals("H"))
                return typeof(HumanConsolePlayer);
            else
                throw new ArgumentException("Wrong input argument " + argument);
        }
    }

}
