using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GhostGame;

namespace GhostGameWeb
{
    public class GhostGameService
    {
        private static string _filePath;

        public GhostGameService(string path)
        {
            _filePath = path;
        }

        public void Play()
        {
            GhostGame.GhostGame game = null;
            Type typePlayer1 = typeof(HumanWebPlayer);
            Type typePlayer2 = typeof(ComputerPlayer);

            try
            {
                GhostPlayer playerOne = (GhostPlayer)Activator.CreateInstance(typePlayer1);
                GhostPlayer playerTwo = (GhostPlayer)Activator.CreateInstance(typePlayer2);

                game = new GhostGame.GhostGame(_filePath, playerOne, playerTwo);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to initialize GhostGame:  " + e.ToString());
            }

            GhostPlayer winner = game.Play();

            Console.WriteLine("Final word:  " + game.WordInPlay() + ", " + winner.Name() + " wins!");

        }
    }
}

