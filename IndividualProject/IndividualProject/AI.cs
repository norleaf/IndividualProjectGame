using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace IndividualProject
{
    public class AI
    {
        public List<Field> openNodes { get; private set; }
        public List<Field> closedNodes { get; private set; }
       // public List<Field> path { get; private set; }
        private Piece owner;
        public Path Path;

        public AI(Piece owner)
        {
            this.owner = owner;
            openNodes = new List<Field>();
            closedNodes = new List<Field>();
            Path = new Path(owner, owner.Target);
        }

        public virtual Piece ChooseTarget(BattleBoard battleBoard)
        {
            Piece bestTarget = null;
            foreach (var piece in battleBoard.Pieces)
            {
                if (piece.teamColor != owner.teamColor)
                {
                    if (bestTarget == null)
                    {
                        bestTarget = piece;
                        Path = FindPathToTarget(piece, battleBoard);
                    }
                    else
                    {
                        Path comparePath = FindPathToTarget(piece, battleBoard);
                        if (comparePath.Value > Path.Value)
                        {
                            Path = comparePath;
                            bestTarget = piece;
                        }
                    }
                }
            }
            return bestTarget;
        }

        public virtual Path FindPathToTarget(Piece targetPiece, BattleBoard board)
        {
            Field origin = owner.Field;
            Field target = targetPiece.Field;
            Field currentNode;
            //create the open list of nodes, initially containing only our starting node
            //create the closed list of nodes, initially empty
            openNodes.Clear();
            closedNodes.Clear();
            openNodes.Add(origin);
            Path.Fields.Clear();
            board.ClearParents();
            bool done = false;
            while (!done)
            {
                //consider the best node in the open list (the node with the lowest f value)
                float lowestF = openNodes[0].F(target);
                currentNode = openNodes[0];
                foreach (var node in openNodes)
                {
                    if (node.F(target)<lowestF)
                    {
                        currentNode = node;
                        lowestF = node.F(target);
                    }
                }
                if (currentNode.Equals(target))
                {
                    done = true;
                    Console.WriteLine("Success");
                    while (currentNode.PathParent != null)
                    {
                        Path.Fields.Add(currentNode);
                        currentNode = currentNode.PathParent;
                    } 
                    return Path;
                }
                else
                {
                    //move the current node to the closed list and consider all of its neighbors
                    openNodes.Remove(currentNode);
                    closedNodes.Add(currentNode);
                    List<Field> neighbors = currentNode.Neighbors(board, target);
                    foreach (var node in neighbors)
                    {
                        if (closedNodes.Contains(node) || openNodes.Contains(node))
                        {
                            if (node.PathCost > currentNode.PathCost + node.StepCost(currentNode))
                            {
                                node.PathCost = currentNode.PathCost + node.StepCost(currentNode);
                                node.PathParent = currentNode;
                            }
                        }
                        else
                        {
                            node.PathCost = currentNode.PathCost + node.StepCost(currentNode);
                            node.PathParent = currentNode;
                            openNodes.Add(node);
                        }
                    }
                }
                if (openNodes.Count == 0)
                {
                    return null;
                }
            }
            return null;
        }

        
    }
}
