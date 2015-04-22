using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace IndividualProject
{
    public class Piece
    {
        public Field Field { get; set; }
        public int Health { get; private set; }
        public int Actions { get; private set; }

        public void TakeDamage(int i)
        {
            Health -= i;
            if(Health<=0)
            {
                Die();
            }
        }

        private void Die()
        {
            throw new NotImplementedException();
        }

        public void SpendAction(int i)
        {
            Actions -= i;
        }

        
    }
}
