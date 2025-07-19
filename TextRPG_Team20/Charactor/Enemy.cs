using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Scene;

namespace TextRPG_Team20
{
    internal class Enemy : Character, ICloneable
    {
        public List<string> asciiData = new List<string>();

        public ConsoleUI.Rect targetRect;

        public Enemy() { }

        public Enemy(string name, int gold,  Status status) : base(name, JobType.None, gold, status)
        {

        }

        public object Clone()
        {
            var clone = Activator.CreateInstance(this.GetType()) as Enemy;
            clone.status = new Status
            (
                status.ID,
                status.Level,
                status.HP,
                status.Atk,
                status.Def,
                status.Name,
                status.Gold,
                status.Spawn,
                status.ExtraAtk,
                status.ExtraDef
            );
            clone.asciiData = asciiData;
            clone.Job = JobType.None;
            return clone;
        }

        public override void Action()
        {
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}{status.Name}이(가) 공격을 준비합니다.{AnsiColor.Reset}", ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);         
        }

        public override void Attack(Character target)
        {
            int damage = status.TotalAtk;
            int actualDamage = Math.Max(1, damage - target.status.TotalDef);
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}{status.Name}{AnsiColor.Reset}이(가) {AnsiColor.Red}{target.status.Name}{AnsiColor.Reset}에게 일반 공격!", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}{actualDamage}{AnsiColor.Reset}의 피해를 받았습니다.", ref ConsoleUI.logView);
            target.DecreaseHp(actualDamage);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
        }

        public override void PrintData()
        {
        }

        public override void PrintInField()
        {
        }

        public void DrawAscii(ref ConsoleUI.Rect rect)
        {
            targetRect = rect;

            var blank = new List<string> { new string(' ', rect.width) };

            ConsoleUI.Instance.InsertTextInBox(blank, ref rect);
            ConsoleUI.Instance.InsertTextInBox(blank, ref rect);
            ConsoleUI.Instance.InsertTextInBox(asciiData, ref rect);
            ConsoleUI.Instance.InsertTextInBox(blank, ref rect);

            int barLength = 30;

            double hpRatio = (double)status.HP / status.MaxHP;

            int filledLength = (int)(hpRatio * barLength);
            int emptyLength = barLength - filledLength;
            string bar = "[" + AnsiColor.Green + new string('=', filledLength) + new string(' ', emptyLength) + $"{AnsiColor.Reset}] {status.HP}/{status.MaxHP} ";

            ConsoleUI.Instance.DrawTextInBox($"[{bar}]", ref rect); 
            ConsoleUI.Instance.PrintView(ref rect, "center", "middle");

        }
    }
}
