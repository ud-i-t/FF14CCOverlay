using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGOverlayControl
{
    internal class MatchViewModel
    {
        private Model.Match _match;
        private PlayerDatas _players;

        public MatchViewModel(Model.Match match, PlayerDatas player) 
        { 
            _match = match;
            _players = player;
        }

        public override string ToString()
        {
            return $"{_match.CenterText} {_players.players[_match.Player1Index].name} vs {_players.players[_match.Player2Index].name}";
        }
    }
}
