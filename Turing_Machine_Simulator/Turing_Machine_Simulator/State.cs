using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turing_Machine_Simulator
{
    class State
    {
        public string name;
        public Dictionary<string, string> transitions;
        public bool isFinal;

        public State(string _name)
        {
            name = _name.Trim();
            isFinal = false;
            transitions = new Dictionary<string, string>(); 
        }

        public void AddTransition(string symbol, string transition)
        {
            if (!transition.Trim().Equals("_"))
            {
                string key = name +"."+symbol;
                transitions.Add(key.Trim(), transition.Trim());
            }
           
        }
    }
}
