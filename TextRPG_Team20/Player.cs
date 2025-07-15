using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20
{
    internal class Player : Character
    {
        public Player(string name, string job, int gold, Status status)  : base(name, job, gold, status)
        {

        }
        public override void Action()
        {
            Console.WriteLine($"{Name}의 차례입니다. 행동을 선택하세요.");
        }
    }
}
