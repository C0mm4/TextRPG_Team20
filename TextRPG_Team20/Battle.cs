using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Charactor.Enemys;
using TextRPG_Team20.Scene;
using TextRPG_Team20.Skill;
using static TextRPG_Team20.ConsoleUI;

namespace TextRPG_Team20.Scene
{
    internal class Battle
    {
        public static List<Enemy> enemies = new List<Enemy>();

        public static int selectIndex;
        public static int range = 2;
        public static void OnBattle(Player player, List<Enemy> enemies)
        {
            player.Action();
        }


        public static bool OnNormalAttack(Player player, List<Enemy> enemies)
        {
            if (enemies.Count == 0) return true;
            range = 1;
            // 1. 플레이어가 때릴 적 선택
            var targets = SelectEnemy(enemies);
            if (targets == null)
            {
                ConsoleUI.Instance.DrawTextInBox("공격을 취소했습니다.", ref ConsoleUI.logView);
                return false; // 공격 사용을 취소하고 아무 일도 안 함
            }
            else
                player.Attack(targets[0]);

            var target = targets[0];

            // 2. 죽었으면 제거
            if (target.status.HP <= 0)
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}{target.status.Name}이(가) 쓰러졌다!{AnsiColor.Reset}", ref ConsoleUI.logView);
                player.AddGold(target.status.Gold);
                enemies.Remove(target);
            }
            return true;
        }


        public static bool OnSkillAttack(Player player, List<Enemy> enemies)
        {
            if (enemies.Count == 0) return true;

            if (player.skills.Count == 0)
            {
                ConsoleUI.Instance.DrawTextInBox("사용할 수 있는 스킬이 없습니다.", ref ConsoleUI.logView);
                return false;
            }
            Skill.Skill? skill = player.SelectSkill();
            if (skill == null)
            {
                ConsoleUI.Instance.DrawTextInBox("스킬사용을 취소했습니다.", ref ConsoleUI.logView);
                return false;
            }

            if(player.status.Gold < skill.Data.Cost)
            {
                ConsoleUI.Instance.DrawTextInBox("골드가 부족합니다!!.", ref ConsoleUI.logView);
                return false;
            }

            range = skill.Data.Range;

            var target = SelectEnemy(enemies);
            if (target == null)
            {
                ConsoleUI.Instance.DrawTextInBox("스킬사용을 취소했습니다.", ref ConsoleUI.logView);
                return false; // 스킬 사용을 취소하고 아무 일도 안 함
            }
            else
            {
                player.UseSkill(target, skill);
                player.DecreaseGold(skill.Data.Cost);
            }

            return true;

        }

        public static List<Enemy>? SelectEnemy(List<Enemy> enemies)
        {
            ConsoleUI.inputView.ClearBuffer();
            selectIndex = enemies.Count / 2;
            bool isEndFlag = false;
            while (true)
            {

                for(int i = Math.Max(0, selectIndex - range + 1); i < Math.Min(enemies.Count, selectIndex + range); i++)
                {
                    ConsoleUI.Instance.InsertTextInBox(new List<string> { "", "", "^" }, ref enemies[i].targetRect);
                    ConsoleUI.Instance.PrintView(ref enemies[i].targetRect, "center", "middle");

                }

                var keyValue = Console.ReadKey();
                
                for (int i = Math.Max(0, selectIndex - range + 1); i < Math.Min(enemies.Count, selectIndex + range); i++)
                {
                    ConsoleUI.ClearView(enemies[i].targetRect);
                    ConsoleUI.RemoveLines(ref enemies[i].targetRect, 3);
                    ConsoleUI.Instance.PrintView(ref enemies[i].targetRect, "center", "middle");

                }

                switch (keyValue.Key)
                {
                    case ConsoleKey.Enter:
                        isEndFlag = true;
                        break;
                    case ConsoleKey.LeftArrow:
                        selectIndex = Math.Max(0, selectIndex - 1);
                        break;
                    case ConsoleKey.RightArrow:
                        selectIndex = Math.Min(enemies.Count - 1, selectIndex + 1);
                        break;
                    case ConsoleKey.Escape:
                        for (int i = Math.Max(0, selectIndex - range + 1); i < Math.Min(enemies.Count, selectIndex + range); i++)
                        {
                            ConsoleUI.ClearView(enemies[i].targetRect);

                            // 화살표 없이 정상적으로 출력
                            ConsoleUI.Instance.PrintView(ref enemies[i].targetRect, "center", "middle");
                        }
                        return null;
                        
                }


                if (isEndFlag)
                {
                    break;
                }

            }

            List<Enemy> ret = new();

            for (int i = Math.Max(0, selectIndex - range + 1); i < Math.Min(enemies.Count, selectIndex + range); i++)
            {
                ret.Add(enemies[i]);
            }

            return ret;
        }


    }

}





