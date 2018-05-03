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
                /*  1;+;B
                q0;q0,1,R;q1,1,R;_
                q1;q1,1,R;_;q2,B,L
                q2;q3,B,N;_;_
                *q3; _;_;_*/
        
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
                    current = new State(moves[0]);
                    if (moves[0].Contains("*"))
                    {
                        current.isFinal = true; 
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

        public void DoTransition()
        {
            //q2,a,R
            if (!current.isFinal)
            {
                string actualTransition;
                if (!current.transitions.TryGetValue((current.name + tape[tapePosition]), out actualTransition))
                {
                    accepted = false;
                    return;
                }
                else
                {
                    string[] transition = actualTransition.Split(',');
                    current = states.Find(x => x.name.Equals(transition[0]));
                    if (tape.Length <= tapePosition)
                        tape += "B"; 
                    tape.Insert(tapePosition, transition[1]);

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



    }
}
