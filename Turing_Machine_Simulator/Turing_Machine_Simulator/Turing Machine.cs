using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turing_Machine_Simulator
{
    class Turing_Machine
    {
        public string tape;
        public string[] alphabet;
        public State initial;
        public State current;
        public List<State> states;
        private int tapePosition;
        public bool accepted;
        public int stepCounter; 

        public Turing_Machine(string table)
        {
            states = new List<State>(); 
            Initialize(table);
            tapePosition = 0;
            tape = "";
            stepCounter = 0; 
        }
        
        private void Initialize(string transitions)
        {
            string[] lines = transitions.Split('\n');
            for(int i = 0; i < lines.Length; i++)
            {
                string[] moves = lines[i].Split(';');
                if (i == 0)
                {
                    alphabet = moves;
                }
                else if (i == 1)
                {
                    initial = new State(moves[0]);
                    states.Add(initial);
                    for(int j = 0; j < moves.Length-1; j++)
                    {
                        initial.AddTransition(alphabet[j], moves[j + 1]);
                    }
                }
                else 
                {
                   
                    if (moves[0].Contains("*"))
                    {
                        current = new State(moves[0].Substring(1));
                        current.isFinal = true;
                    }
                    else
                    {
                        current = new State(moves[0]);
                    }
                    states.Add(current);
                    for(int j = 0; j < moves.Length-1; j++)
                    {
                        current.AddTransition(alphabet[j], moves[j + 1]);
                    }
                }
            }
            current = initial; 
        }

        public void Restart()
        {
            current = initial;
            tape = "";
            tapePosition = 0;
            accepted = false;
            stepCounter = 0; 
        }
        public void DoTransition()
        {
            if (!current.isFinal)
            {
                string actualTransition;
                if (tape.Length <= tapePosition)
                    tape += "B";
                else if(tapePosition < 0)
                {
                    tapePosition++;
                    tape = "B" + tape;
                }
          
                string key = current.name + "." + tape[tapePosition];
                if (!current.transitions.TryGetValue(key, out actualTransition))
                {
                    
                    accepted = false;
                    return;
                }
                else
                {
                    string[] transition = actualTransition.Split(',');
                    current = states.Find(x => x.name.Equals(transition[0]));

                    tape = ReplaceAtIndex(tapePosition, transition[1], tape); 
                    //tape.Insert(tapePosition, transition[1]);

                    if (transition[2].Equals("R"))
                        tapePosition++;
                    else if (transition[2].Equals("L"))
                        tapePosition--;
                    stepCounter++; 
                }
            }
            else
            {
                accepted = true; 
                return; 
            }                 
        }

        private string ReplaceAtIndex(int i, string value, string word)
        {
            char[] letters = word.ToCharArray();
            letters[i] = value.ToCharArray()[0];
            return string.Join("", letters);
        }

    }
}
