using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            private string _name;
            private string _surname;
            protected int[] _minutes;
            private int total;

            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public int[] PenaltyTimes
            {
                get
                {
                    if (_minutes == null) return null;
                    if (_minutes.Length == 0) return new int[0];
                    int[] minutes = new int[_minutes.Length];
                    Array.Copy(_minutes, minutes, minutes.Length);
                    return minutes;
                }
            }
            public int TotalTime
            {
                get
                {
                    if (_minutes == null || _minutes.Length == 0) return 0;
                    int total = 0;
                    for (int i = 0; i < _minutes.Length; i++)
                    {
                        total += _minutes[i];
                    }
                    return total;
                }
            }
            public virtual bool IsExpelled
            {
                get
                {
                    if (_minutes == null) return false;
                    foreach (int minute in _minutes)
                    {
                        if (minute == 10)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _minutes = new int[0];
                total = 0;

            }

            public virtual void PlayMatch(int time)
            {

                Array.Resize(ref _minutes, _minutes.Length + 1);
                _minutes[_minutes.Length - 1] = time;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                Array.Sort(array, (a, b) => a.TotalTime.CompareTo(b.TotalTime));

            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {TotalTime}, Is expelled: {IsExpelled}");
            }
        }

        public class BasketballPlayer  : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {

            }
            public override bool IsExpelled
            {
                get
                {
                    if (_minutes == null || _minutes.Length == 0) { return false; }
                    int fiveFoulsMatches = 0;
                    foreach (int fouls in _minutes)
                    {
                        if (fouls == 5) fiveFoulsMatches++;
                    }

                    bool tooMany5Fouls = (fiveFoulsMatches * 100 / _minutes.Length) > 10;
                    bool doubleFouls = TotalTime > (2 * _minutes.Length);

                    return tooMany5Fouls || doubleFouls;

                }
            }

            public override void PlayMatch(int fouls)
            {
                if (fouls < 0 || fouls > 5) return;
                base.PlayMatch(fouls);
            }

        }

        public class HockeyPlayer : Participant
        {
            private static int _totalHockeyPlayers;
            private static int _totalPenaltyTime;

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _totalHockeyPlayers++;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (_minutes == null) return false;

                    foreach (int minute in _minutes)
                    {
                        if (minute == 10) return true;
                    }

                    if (_totalHockeyPlayers == 0) return false;
                    double averagePenalty = _totalPenaltyTime / (double)_totalHockeyPlayers;
                    return TotalTime > (averagePenalty * 0.1);
                }
            }

            public override void PlayMatch(int penaltyTime)
            {
                if (penaltyTime < 0 || penaltyTime > 10) return;

                base.PlayMatch(penaltyTime);
                _totalPenaltyTime += penaltyTime;
            }
        }
    }
}
