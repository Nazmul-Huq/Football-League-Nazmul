using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballLeague.model
{
    public enum SpecialRanking
    {      
        [Description("last years champion")] 
        W,
        [Description("Last years cup winner")]
        C,
        [Description("Promoted team")]
        P,
        [Description("Relagated team")]
        R,
        [Description("No special ranking")]
        N
    }
}
