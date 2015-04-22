using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndividualProject
{
    public class Level
    {
        public State TurnState { get; set; }
        public enum State
        {
            RedTurn,
            BlueTurn
        }
    }
}
