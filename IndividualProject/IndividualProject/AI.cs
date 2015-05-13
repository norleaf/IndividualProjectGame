﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace IndividualProject
{
    public class AI
    {
        public List<Field> openNodes { get; private set; }
        public List<Field> closedNodes { get; private set; }
        public List<Field> path { get; private set; }
        private Piece owner;

        public AI(Piece owner)
        {
            this.owner = owner;
            openNodes = new List<Field>();
            closedNodes = new List<Field>();
            path = new List<Field>();
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
                        path = FindPathToTarget(owner.Field, piece.Field, battleBoard);
                    }
                }
            }
            return bestTarget;
        }

        public virtual List<Field> FindPathToTarget(Field origin, Field target, BattleBoard board)
        {
            Field currentNode;
            openNodes.Clear();
            closedNodes.Clear();
            path.Clear();
            board.ClearParents();
            bool done = false;
            openNodes.Add(origin);
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
             //   Console.WriteLine(currentNode.X+","+currentNode.Y+" cost: "+currentNode.PathCost);
                if (currentNode.Equals(target))
                {
                    done = true;
                    Console.WriteLine("Success");
                    while (currentNode.PathParent != null)
                    {
                   //     Console.WriteLine("adding node: " + currentNode.X + "," + currentNode.Y);
                   //     Console.WriteLine("node parent: " + currentNode.PathParent.X + "," + currentNode.PathParent.Y);
                        path.Add(currentNode);
                        currentNode = currentNode.PathParent;
                    } 
                  //  Console.WriteLine("path: "+path);
                    return path;
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
