using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            private string _name;
            private int[] _scores;

            public string Name => _name;
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;
                    int[] scores = new int[_scores.Length];
                    for (int i = 0; i < scores.Length; i++)
                    {
                        scores[i] = _scores[i];
                    }
                    return scores;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;
                    int total = 0;
                    foreach (int score in _scores) total += score;
                    return total;
                }
            }

            protected Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            public void PlayMatch(int result)
            {
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length - 1] = result;
            }

            public void Print()
            {
                Console.WriteLine($"{Name}: {TotalScore} очков");
            }

            public class ManTeam : Team
            {
                public ManTeam(string name) : base(name)
                {

                }

            }

            public class WomanTeam : Team
            {
                public WomanTeam(string name) : base(name)
                {

                }
            }

            public class Group
            {
                private string _name;
                private ManTeam[] _manTeams;
                private WomanTeam[] _womanTeams;
                private int _manCount;
                private int _womanCount;

                public string Name => _name;
                public ManTeam[] ManTeams => _manTeams.Take(_manCount).ToArray();
                public WomanTeam[] WomanTeams => _womanTeams.Take(_womanCount).ToArray();

                public Group(string name)
                {
                    _name= name;
                    _manTeams= new ManTeam[12];
                    _womanTeams= new WomanTeam[12];
                    _manCount= 0;
                    _womanCount= 0;

                }

                public void Add(Team team)
                {
                    if (team is ManTeam manTeam &&  _manCount < 12)
                    {
                        _manTeams[_manCount++] = manTeam;
                    }
                    else if (team is WomanTeam womanTeam && _womanCount < 12)
                    {
                        _womanTeams[_womanCount++] = womanTeam;
                    }
                }

                public void Add(Team[] teams)
                {
                    if (teams == null) return;
                    foreach (Team team in teams)
                    {
                        Add(team);
                    }
                }

                private void SortTeams(Team[] teams, int count)
                {
                    if (teams == null || count <= 1) return;

                    for (int i = 0; i < count - 1; i++)
                    {
                        for (int j = 0; j < count - i - 1; j++)
                        {
                            if (teams[j].TotalScore < teams[j + 1].TotalScore)
                            {
                                Team temp = teams[j];
                                teams[j] = teams[j + 1];
                                teams[j + 1] = temp;
                            }
                        }
                    }
                }

                public void Sort()
                {
                    SortTeams(_manTeams, _manCount);
                    SortTeams(_womanTeams, _womanCount);
                }

                private static Team[] MergeTeams(Team[] team1,int count1, Team[] team2, int count2, int size)
                {
                    Team[] result = new Team[size];
                    int i = 0, j = 0, k = 0;
                    int halfSize = size / 2;

                    while (i < count1 && j < count2 && k < halfSize)
                    {
                        if (team1[i].TotalScore >= team2[j].TotalScore)
                        {
                            result[k++] = team1[i++];
                        }
                        else
                        {
                            result[k++] = team2[j++];
                        }
                    }

                    while (i < count1 && k < halfSize)
                    {
                        result[k++] = team1[i++];
                    }

                    while (j < count2 && k < halfSize)
                    {
                        result[k++] = team2[j++];
                    }

                    return result;
                }

                public static Group Merge(Group group1, Group group2, int size)
                {
                    Group finalists = new Group("Финалисты");

                    Team[] bestMen = MergeTeams(group1._manTeams, group1._manCount, group2._manTeams, group2._womanCount,size);

                    Team[] bestWomen = MergeTeams(group1._womanTeams, group1._womanCount,group2._womanTeams, group2._womanCount,size);

                    finalists.Add(bestMen);
                    finalists.Add(bestWomen);

                    return finalists;
                }

                public void Print()
                {
                    Console.WriteLine($"Группа: {Name}");
                    Console.WriteLine("Мужские команды:");
                    for (int i = 0; i < _manCount; i++)
                    {
                        _manTeams[i].Print();
                    }
                    Console.WriteLine("\nЖенские команды:");
                    for (int i = 0; i < _womanCount; i++)
                    {
                        _womanTeams[i].Print();
                    }
                }
            }
        }
    }
}
