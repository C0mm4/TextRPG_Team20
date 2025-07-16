using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Charactor.Enemys
{
    internal class ExampleEnemy : Enemy
    {
        public ExampleEnemy(String name, Status status) : base("예시몹", status)
        {

        }

        public override void Action()
        {
            ConsoleUI.Instance.DrawTextInBox($"{Name}이(가) 공격 차례.", ref ConsoleUI.logView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
        }
    }
}
