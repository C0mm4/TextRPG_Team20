using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Dungeon;
using TextRPG_Team20.Scene;

namespace TextRPG_Team20
{
    internal class Player : Character
    {
        public Player(string name, string job, int gold, Status status)  : base(name, job, gold, status)
        {

        }
        public override void Action()
        {

            ConsoleUI.inputView.ClearBuffer();
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}{status.Name}의 차례입니다. 행동을 선택하세요.{AnsiColor.Reset}", ref ConsoleUI.inputView);
            ConsoleUI.Instance.DrawTextInBox("1. 일반 공격", ref ConsoleUI.inputView);
            ConsoleUI.Instance.DrawTextInBox("2. 스킬 사용", ref ConsoleUI.inputView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);

            

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

        public override void Attack(Character target)
        {
            int damage = status.TotalAtk;
            int actualDamage = Math.Max(1, damage - target.status.TotalDef); //최소한 1의 피해는 무조건 입힌다
            target.DecreaseHp(actualDamage);

            ConsoleUI.Instance.DrawTextInBox($"{status.Name}이(가) {target.status.Name}에게 일반 공격!", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}{actualDamage}{AnsiColor.Reset}의 피해를 입혔습니다.", ref ConsoleUI.logView);
        }

        public void UseSkill(Character target)
        {
            int skillDamage = status.TotalAtk * 2;
            int actualDamage = Math.Max(1, skillDamage - target.status.TotalDef);
            target.DecreaseHp(actualDamage);

            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Cyan}{status.Name}의 스킬 사용!{AnsiColor.Reset}", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox($"{target.status.Name}에게 {AnsiColor.Red}{actualDamage}{AnsiColor.Reset}의 피해를 입혔습니다!", ref ConsoleUI.logView);
        }

        public void playercollision(int x, int y)
        {

            DungeonManager.Instance.currentField[this.y, this.x] = 0;
            int[] newpos = { this.x + x, this.y + y };
            if (newpos[0] < 0 || newpos[1] < 0 || newpos[0] > 4 || newpos[1] > 4)
            { }
            else
            {

                if (DungeonManager.Instance.currentField[newpos[1], newpos[0]] == 0)
                {
                    this.x = newpos[0];
                    this.y = newpos[1];
                }
                else if (DungeonManager.Instance.currentField[newpos[1], newpos[0]] == 1)
                { }

                else if (DungeonManager.Instance.currentField[newpos[1], newpos[0]] == 2)
                {
                    Console.WriteLine("상자를 발견했습니다. 축하합니다 5골드를 획득하셨습니다.");
                    DungeonManager.Instance.currentField[newpos[1], newpos[0]] = 0;
                    this.x = newpos[0];
                    this.y = newpos[1];
                }
                else if (DungeonManager.Instance.currentField[newpos[1], newpos[0]] == 3)
                {
                    Console.WriteLine("닫힌 문입니다 지금은 지나가실 수 없습니다.");

                }
                else if (DungeonManager.Instance.currentField[newpos[1], newpos[0]] == 4)
                {
                    Console.WriteLine("발판이 눌렸습니다 어딘가의 문이나 상자가 열렸을지도 모르겠네요.");
                    DungeonManager.Instance.currentField[newpos[1], newpos[0]] = 0;

                    this.x = newpos[0];
                    this.y = newpos[1];
                }
                else if (DungeonManager.Instance.currentField[newpos[1], newpos[0]] == 5)
                {
                    Console.WriteLine("다음방으로 넘어갑니다.");
                }
            }

            DungeonManager.Instance.currentField[this.y, this.x] = -1;
        }

        public override void PrintData()
        {

        }

        public override void PrintInField()
        {

        }
    }
}
