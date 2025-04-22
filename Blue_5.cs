using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            private string _name;
            private string _surname;
            private int _place;

            public string Name => _name;
            public string Surname => _surname;
            public int Place => _place;

            public Sportsman(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _place = 0;
            }

            public void SetPlace(int place)
            {
                if (_place != 0) return;
                _place = place;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: место {Place}");
            }
        }

        public abstract class Team
        {
            private string _name;
            private Sportsman[] _sportsmen;
            private int _count;

            public string Name => _name;
            public Sportsman[] Sportsmen
            {
                get
                {
                    if (_sportsmen == null)
                    {
                        return null;
                    }
                    return _sportsmen;
                }
            }

            public int SummaryScore
            {
                get
                {
                    if (_sportsmen == null) return 0;
                    int score = 0;
                    foreach (var s in _sportsmen)
                    {
                        if (s == null) continue;
                        switch (s.Place)
                        {
                            case 1: score += 5; break;
                            case 2: score += 4; break;
                            case 3: score += 3; break;
                            case 4: score += 2; break;
                            case 5: score += 1; break;
                        }
                    }
                    return score;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (_sportsmen == null)
                    {
                        return int.MaxValue;
                    }
                    int minPlace = int.MaxValue;
                    foreach (var s in _sportsmen)
                    {
                        if (s != null && s.Place > 0 && s.Place < minPlace)
                        {
                            minPlace = s.Place;
                        }

                    }
                    return minPlace;
                }
            }

            public Team(string name)
            {
                _name = name;
                _sportsmen = new Sportsman[6];
                _count = 0;
            }

            public void Add(Sportsman sportsman)
            {
                if (_count < 6 && sportsman != null) _sportsmen[_count++] = sportsman;

            }

            public void Add(Sportsman[] sportsmen)
            {
                if (sportsmen == null || _sportsmen == null) return;
                foreach (var s in sportsmen)
                {
                    if (_count >= 6) break;
                    Add(s);
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null) return;

                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = 0; j < teams.Length - i - 1; j++)
                    {
                        if (teams[j].SummaryScore < teams[j + 1].SummaryScore)
                        {
                            var temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }

                        else if (teams[j].SummaryScore == teams[j + 1].SummaryScore && teams[j].TopPlace > teams[j + 1].TopPlace)
                        {
                            var temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                    }
                }
            }

            protected abstract double GetTeamStrength();

            public static Team GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0) return null;
                Team champion = null;
                double maxStrength = double.MinValue;
                foreach ( var team in teams)
                {
                    if (team == null) continue;

                    double strength = team.GetTeamStrength();
                    if (strength > maxStrength)
                    {
                        maxStrength = strength;
                        champion = team;
                    }
                }
                return champion;
            }
            public void Print()
            {
                Console.WriteLine(_name);
                for (int i = 0; i < _count; i++)
                {
                    _sportsmen[i].Print();
                }
            }

        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }


            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || Sportsmen.Length == 0) return 0.0;

                double sum = 0.0;
                double count = 0;

                foreach ( var s in Sportsmen)
                {
                    if (s != null && s.Place > 0)
                    {
                        sum += s.Place;
                        count++;
                    }
                }
                if (count == 0) return 0.0;
                return 100/(sum/count);
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }


            protected override double GetTeamStrength()
            {
                if (Sportsmen == null || Sportsmen.Length == 0) return 0;

                double sum = 0;
                double product = 1;
                int count = 0;

                foreach (var s in Sportsmen)
                {
                    if (s != null && s.Place > 0)
                    {
                        sum += s.Place;
                        product *= s.Place;
                        count++;
                    }
                }

                if (product == 0) return 0;
                return 100 *( sum * count / product);
            }
        }
    }
}
