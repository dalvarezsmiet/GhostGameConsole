using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostGame
{
    public interface GhostPlayer
    {
        /// <summary>
        /// The name of the player.
        /// </summary>
        string Name();

        /// <summary>
        /// Add a character to the word in play.
        /// </summary>
        /// <param name="wordInPlay"> The word currently being played. </param>
        char Play(string wordInPlay);
    }
}
