using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.model
{
    public class Game
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        public Game()
        {

        }

        public Game(string team1, string team2)
        {
            HomeTeam = team1;
            AwayTeam = team2;
        }
    }
} // namespace ends here
