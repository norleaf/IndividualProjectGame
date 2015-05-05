using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IndividualProject
{
    class AI
    {
        public virtual Node FindPathToTarget(Piece origin, Piece target, BattleBoard board)
        {
            Node currentNode = new Node();
            List<Node> openNodes = new List<Node>();
            List<Node> closedNodes = new List<Node>();
            currentNode.Field = target.Field;
            return null;
        }
    }
}
