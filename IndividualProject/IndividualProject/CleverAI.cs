using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndividualProject
{
    class CleverAI : AI
    {
        public CleverAI(Piece owner) : base(owner)
        {
            F1 = 0.5f;  //AttackDam Importance
            F2 = 0.5f;  //Life Importance
            F3 = 5f;  //Armor importance
            F4 = 1f + 3f/owner.MaxActionPoints; //Distance importance
        }
    }
}
