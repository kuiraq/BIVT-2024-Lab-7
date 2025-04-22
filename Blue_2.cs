using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class Blue_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int total;

            public string Name { get { return _name; } }
            public string Surname => _surname;

            public int[,] Marks
            {
                get
                {
                    if (_marks == null || _marks.GetLength(0) != 2 || _marks.GetLength(1) != 5) return null;
                    int[,] marks = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < 2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            marks[i, j] = _marks[i, j];
                        }
                    }
                    return marks;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_marks == null || _marks.GetLength(0) == 0 || _marks.GetLength(1) == 0) return 0;
                    int total = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            total += _marks[i, j];
                        }
                    }
                    return total;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                total = 0;
            }

            public void Jump(int[] result)
            {
                if (result == null || _marks == null) return;
                for (int i = 0; i < 2; i++)
                {
                    bool isEmpty = true;

                    for (int j = 0; j < 5; j++)
                    {
                        if (_marks[i, j] != 0)
                        {
                            isEmpty = false;
                            break;
                        }
                    }
                    if (isEmpty)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            _marks[i, j] = result[j];
                        }
                        break;
                    }
                }
            }

            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length == 0) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - 1 - i; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore) (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {TotalScore}");
            }


        }

        public abstract class WaterJump
        {
            private string _tournament;
            private int _bank;
            private Participant[] _participants;

            public string Name => _tournament;
            public int Bank => _bank;
            public Participant[] Participants => _participants;

            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                _tournament = name;
                _bank = bank;
                _participants = new Participant[0];
            }

            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;
            }
            public void Add(Participant[] participants)
            {
                if (participants == null) return;
                foreach (var p in participants) { Add(p); }

            }

        }
        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants == null || Participants.Length < 3) return null;
                    double[] awards = new double[3];
                    awards[0] = Bank * 0.5;
                    awards[1] = Bank * 0.3;
                    awards[2] = Bank * 0.2;
                    return awards;

                }
            }
        }


        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    if (Participants == null || Participants.Length < 3) return null;

                    int amountWinners = Math.Min(Participants.Length / 2, 10); //  Не менее 3 и не более половины но не больше 10

                    double[] awards = new double[amountWinners];
                    double N = 20.0 / amountWinners;

                    awards[0] = 0.40 * Bank;
                    awards[1] = 0.25 * Bank;
                    awards[2] = 0.15 * Bank;

                    
                    for (int i = 0; i < awards.Length; i++)
                    {
                        awards[i] += Bank * (N / 100); ;
                    }

                    return awards;
                }
            }
        }



    }
}
