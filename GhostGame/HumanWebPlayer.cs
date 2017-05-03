using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostGame
{
    public class HumanWebPlayer : GhostPlayer
    {
        private string _name = "HumanWeb";

        public string Name()
        {
            return _name;
        }

        public char Play(string character)
        {
            if (character.Length == 0)
                return 'x';
            else
                return character[0];
        }
    }
}
