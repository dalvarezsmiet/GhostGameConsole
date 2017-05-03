using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GhostGame;

namespace GhostGameConsole
{
    public class Program
    {
        /// <summary>
        /// A test harness for the GhostGame class.
        /// </summary>
        /// <param name="args"> The command line arguments passed in. </param>f
        public static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                GhostGame.GhostGame game = null;
                Type typePlayer1 = GetPlayerType(args[1]);
                Type typePlayer2 = GetPlayerType(args[2]);

                try
                {
                    GhostPlayer playerOne = (GhostPlayer)Activator.CreateInstance(typePlayer1);
                    GhostPlayer playerTwo = (GhostPlayer)Activator.CreateInstance(typePlayer2);

                    game = new GhostGame.GhostGame(args[0], playerOne, playerTwo);
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
                Console.WriteLine("Usage:  GhostGame" + typeof(GhostGame.GhostGame).FullName + " <word-list> <player-one> <player-two>");
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
