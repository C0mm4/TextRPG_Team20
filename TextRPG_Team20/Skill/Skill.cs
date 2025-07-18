using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Scene;

namespace TextRPG_Team20.Skill
{
    internal class Skill : ISkill, IComparable<Skill>   
    {
        public SkillData Data { get; set; }

        public List<Enemy> targets;
        public void Action(List<Enemy> targets)
        {
            this.targets = targets;
            Execute();
        }

        public virtual void Execute()
        {
            for (int i = 0; i < targets.Count; i++)
            {
                int actualDamage = Math.Max(1, (int)(Data.AtkPercent * Game.playerInstance.status.Atk) - targets[i].status.Def);


                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Cyan}{Game.playerInstance.status.Name}의 {Data.Name} 스킬 사용!{AnsiColor.Reset}", ref ConsoleUI.logView);
                for (int j = 0; j < Data.HitCount; j++)
                {
                    targets[i].DecreaseHp(actualDamage);
                    ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}{targets[i].status.Name}에게 {actualDamage}의 피해를 입혔습니다!{AnsiColor.Reset}", ref ConsoleUI.logView);
                }

                if (targets[i].status.HP <= 0)
                {
                    ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}{targets[i].status.Name}이(가) 쓰러졌다!{AnsiColor.Reset}", ref ConsoleUI.logView);
                    Game.playerInstance.AddGold(targets[i].status.Gold);
                    Battle.enemies.Remove(targets[i]);
                }
            }
        }

        public int CompareTo(Skill? other)
        {
            if (Data.isEquipped)
            {
                if (other.Data.isEquipped)
                {
                    return Data.ID.CompareTo(other.Data.ID);
                }
                return -1;
            }
            if (other.Data.isEquipped)
            {
                return 1;
            }
            return Data.ID.CompareTo(other.Data.ID);
        }
    }
}
