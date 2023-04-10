using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.model
{

    public class Team
    {
        public string? Abbreviation { get; set; }
        public string? Name { get; set; }
        public SpecialRanking SpecialRanking { get; set; } = SpecialRanking.N;

        public Team()
        {

        }

        public Team(string? abbreviation, string? name)
        {
            Abbreviation = abbreviation;
            Name = name;
        }

        public Team(string? abbreviation, string? name, SpecialRanking specialRanking)
        {
            Abbreviation = abbreviation;
            Name = name;
            SpecialRanking = specialRanking;
        }


    }


} // namespace ends here
