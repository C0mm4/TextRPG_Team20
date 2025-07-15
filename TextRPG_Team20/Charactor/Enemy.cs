using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20
{
    internal class Enemy : Character
    {

        public Enemy(string name, Status status) : base(name, "Enemy", 0, status)
        {

        }

        public override void Action()
        {
            Console.WriteLine($"{Name}이(가) 공격을 준비합니다.");
            
        }
    }
}
