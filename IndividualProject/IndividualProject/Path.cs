﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IndividualProject
{
    public class Path
    {
        public List<Field> Fields { get; set; }
        public int Cost;
        public int Damage;
        
        private Piece TargetPiece;

        public Path(Piece targetPiece)
        {
            TargetPiece = targetPiece;
            Fields = new List<Field>();
        }

        public int PieceValue
        {
            get { return TargetPiece.AttackDamage - TargetPiece.Health - TargetPiece.Armor; }
        }

        public int Value
        {
            get { return PieceValue - Cost - Damage; }
        }
    }
}