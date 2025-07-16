using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Scene;

namespace TextRPG_Team20.Scene
{
    internal class Battle
    {
        List<Enemy> enemies = new List<Enemy>();


        public static void OnBattle(Player player, Enemy enemy)
        {
            player.Action();
        }
        public static void OnNormalAttack(Player player, Enemy enemy)
        {
            {
                player.Attack(enemy);
                CheckWin(player, enemy);
                
            }
        }

       
        public static void OnSkillAttack(Player player, Enemy enemy)
        {
            player.UseSkill(enemy);
            CheckWin(player, enemy);
        
        }

        public static void Miss(Player player, Enemy enemy)
        {
            ConsoleUI.Instance.DrawTextInBox("잘못된 입력입니다. 적이 공격해옵니다.", ref ConsoleUI.logView);
            enemy.Attack(player);
            CheckWin(player, enemy);
        }

        public static void CheckWin(Player player, Enemy enemy)
        {
            if (enemy.status.Hp <= 0)
            {   //  적 사망 체크
                ConsoleUI.Instance.DrawTextInBox($"{enemy.status.Name}이(가) 쓰러졌다!", ref ConsoleUI.logView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.logView, "left", "top");
                Game.Instance.SceneChange(Game.SceneState.Win);
                return;
            }
            else
            {

                if (player.status.Hp <= 0)
                { //  플레이어 사망 체크
                    ConsoleUI.Instance.DrawTextInBox($"{player.status.Name}이(가) 쓰러졌다!", ref ConsoleUI.logView);
                    Console.ReadKey();
                    Game.Instance.SceneChange(Game.SceneState.Result);
                    return;
                }
                
            }

        }

    }

}



