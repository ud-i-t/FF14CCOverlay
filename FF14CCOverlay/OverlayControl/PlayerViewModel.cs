using FTGOverlayControl.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTGOverlayControl
{
    internal class PlayerViewModel : INotifyPropertyChanged
    {
        private PlayerSetting _setting;
        private Action _onChange;

        private string _name = "";
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                _setting.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                _setting.Score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        private int _teamScore;
        public int TeamScore
        {
            get { return _teamScore; }
            set
            {
                _teamScore = value;
                _setting.TeamScore = value;
                OnPropertyChanged(nameof(TeamScore));
            }
        }

        private int _selectedIndex = 0;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (value < 0) return;
                if (value >= Players.Count()) return;
                _selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand IncrementScore { get; private set; }
        public RelayCommand DecrementScore { get; private set; }
        public RelayCommand IncrementTeamScore { get; private set; }
        public RelayCommand DecrementTeamScore { get; private set; }
        public IEnumerable<PlayerModel> Players { get; }

        public PlayerViewModel(PlayerSetting setting, Action onChange, IEnumerable<PlayerModel> players)
        {
            Players = players;

            _setting = setting;
            _onChange = onChange; 
            Name = _setting.Name;
            Score = _setting.Score;
            TeamScore = _setting.TeamScore;

            IncrementScore = new RelayCommand(_ => 
            { 
                Score++;
                _onChange();
            }, _ => true);
            DecrementScore = new RelayCommand(_ =>
            {
                Score--;
                _onChange();
            }, _ => true);
            IncrementTeamScore = new RelayCommand(_ =>
            {
                TeamScore++;
                _onChange();
            }, _ => true);
            DecrementTeamScore = new RelayCommand(_ =>
            {
                TeamScore--;
                _onChange();
            }, _ => true);
        }
    }
}
