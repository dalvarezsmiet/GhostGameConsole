using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GhostGame
{
    public class HumanConsolePlayer : GhostPlayer
    {
        private string _name = "HumanConsole";

        /// <summary>
        /// The default constructor for the HumanConsolePlayer class.
        /// </summary>
        public HumanConsolePlayer()
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
        /// Get the human player's next letter.  This method is inherited from
        /// GhostPlayer.
        /// </summary>
        /// <param name="wordInPlay"> The word currently being played. </param>
        public virtual char Play(string wordInPlay)
        {
            if (wordInPlay.Length == 0)
            {
                Console.WriteLine("Pick your first letter:  ");
            }
            else
            {
                Console.WriteLine("Pick your next letter:  " + wordInPlay);
            }

            char letter = '\0';
            try
            {
                TextReader reader = Console.In;
                letter = (char)reader.Read();
            }
            catch (IOException e)
            {
                Console.WriteLine("Error reading from Console.In:  " + e.ToString());
            }

            return letter;
        }
    }

}
