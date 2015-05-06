using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IndividualProject
{
    class AI
    {
        public List<Field> openNodes { get; private set; }
        public List<Field> closedNodes { get; private set; }

        public virtual Field FindPathToTarget(Field origin, Field target, BattleBoard board)
        {
            Field currentNode;
            
            openNodes = new List<Field>();
            closedNodes = new List<Field>();
            bool done = false;
            openNodes.Add(origin);
            while (!done)
            {
                //consider the best node in the open list (the node with the lowest f value)
                int lowestF = openNodes[0].F(target);
                currentNode = openNodes[0];
                foreach (var node in openNodes)
                {
                    if (node.F(target)<lowestF)
                    {
                        currentNode = node;
                        lowestF = node.F(target);
                    }
                }
                Console.WriteLine(currentNode.X+","+currentNode.Y+" cost: "+currentNode.PathCost);
                if (currentNode.Equals(target))
                {
                    done = true;
                    Console.WriteLine("Success");
                    return currentNode;
                }
                else
                {
                    openNodes.Remove(currentNode);
                    closedNodes.Add(currentNode);
                    List<Field> neighbors = currentNode.Neighbors(board, target);
                    
                    foreach (var node in neighbors)
                    {
                        if (closedNodes.Contains(node) || openNodes.Contains(node))
                        {
                            if (node.PathCost > currentNode.PathCost + node.StepCost)
                            {
                                node.PathCost = currentNode.PathCost + node.StepCost;
                                node.PathParent = currentNode;
                            }
                        }
                        else
                        {
                            node.PathCost = currentNode.PathCost + node.StepCost;
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
