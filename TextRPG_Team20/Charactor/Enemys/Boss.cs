using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Charactor
{
    internal class Boss : Enemy
    {
        public Boss() : base() { }

        public Boss(Status status) : base(status)
        {

        }

        public override void Action()
        {
            
        }

        public override void Attack(Character target)
        {
            base.Attack(target);
        }


    }
}
