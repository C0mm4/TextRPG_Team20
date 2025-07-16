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
            Console.WriteLine($"{status.Name}의 차례입니다. 행동을 선택하세요.");
        }


        public override void CharacterInfo()
        {
            ConsoleUI.Instance.DrawTextInBox($"캐릭터 정보", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"Lv. {status.Level:D2}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"{status.Name} {Job}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"공격력 : {status.Atk} {(status.ExtraAtk == 0 ? "" : $" + ({status.ExtraAtk})")}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"방어력 : {status.Def} {(status.ExtraDef == 0 ? "" : $" + ({status.ExtraDef})")}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"체력 : {status.Hp}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"Gold : {Gold} G", ref ConsoleUI.info1View);
        }
    }
}
