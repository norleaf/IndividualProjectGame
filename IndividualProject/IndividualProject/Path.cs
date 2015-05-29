using System;
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
        private Piece Owner;

        public Path(Piece owner, Piece targetPiece)
        {
            Owner = owner;
            TargetPiece = targetPiece;
            Fields = new List<Field>();
        }

        public int PieceValue
        {
            get { return (int)(TargetPiece.AttackDamage * Owner.AI.F1 - TargetPiece.Health * Owner.AI.F2 - TargetPiece.Armor * Owner.AI.F3); }
        }

        public int Value
        {
            get { return (int)(PieceValue - Cost * Owner.AI.F4 - Damage); }
        }
    }
}
