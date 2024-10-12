using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGOverlayControl.Model
{
    internal class Matches
    {
        public Match[] Items { get; set; }
    }

    internal class Match
    {
        public int Player1Index { get; set; }
        public int Player2Index { get; set; }
        public string CenterText { get; set; } = string.Empty;
    }
}
