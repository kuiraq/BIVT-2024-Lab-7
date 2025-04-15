using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            private string _name;
            protected int _votes;

            public string Name { get { return _name; } }
            public int Votes { get { return _votes; } }

            public Response(string name)
            {
                _name = name;
                _votes = 0;
            }

            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;
                int count = 0;
                foreach (Response response in responses)
                {
                    if (response.Name == _name) { count++; }

                }

                _votes = count;
                return _votes;
            }

            public virtual void Print()
            {
                Console.WriteLine($"{Name}: {Votes}");
            }
        }

        public class HumanResponse : Response
        {
            private string _surname;
            public string Surname { get { return _surname; } }
            public HumanResponse(string name, string surname) : base (name)
            {
                _surname = surname;
            }

            public override int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length == 0) return 0;
                int count = 0;
                foreach (Response response in responses)
                {
                    if (response is HumanResponse humanResponse)
                    {
                        if (humanResponse.Name == Name && humanResponse._surname == _surname)
                        {
                            count++;
                        }
                    }
                }

                _votes = count;
                return _votes;
            }

            public override void Print()
            {
                Console.WriteLine($"{Name} {Surname}: {Votes}");
            }
        }
    }
}
