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

        public static void OnBattle(Player player, List<Enemy> enemies)
        {
            player.Action();
        }


        public static void OnNormalAttack(Player player, List<Enemy> enemies)
        {
            if (enemies.Count == 0) return;

            // 1. 플레이어가 때릴 적 선택
            Enemy target = SelectEnemy(enemies);
            player.Attack(target);

            // 2. 죽었으면 제거
            if (target.status.Hp <= 0)
            {
                ConsoleUI.Instance.DrawTextInBox($"{target.status.Name}이(가) 쓰러졌다!", ref ConsoleUI.logView);
                player.AddGold(target.Gold);
                enemies.Remove(target);
            }

            // 3. 승리 체크
            if (enemies.Count == 0)
            {
                Game.Instance.SceneChange(Game.SceneState.Win);
                return;
            }

            // 4. 남은 적들 턴
            foreach (var enemy in enemies)
            {
                enemy.Attack(player);
                if (player.status.Hp <= 0)
                {
                    ConsoleUI.Instance.DrawTextInBox($"{player.status.Name}이(가) 쓰러졌다!", ref ConsoleUI.logView);
                    Game.Instance.SceneChange(Game.SceneState.Result);
                    return;
                }
            }
        }


        public static void OnSkillAttack(Player player, List<Enemy> enemies)
        {
            if (enemies.Count == 0) return;

            Enemy target = SelectEnemy(enemies);
            player.UseSkill(target);

            if (target.status.Hp <= 0)
            {
                ConsoleUI.Instance.DrawTextInBox($"{target.status.Name}이(가) 쓰러졌다!", ref ConsoleUI.logView);
                player.AddGold(target.Gold);
                enemies.Remove(target);
            }

            if (enemies.Count == 0)
            {
                Game.Instance.SceneChange(Game.SceneState.Win);
                return;
            }

            foreach (var enemy in enemies)
            {
                enemy.Attack(player);
                if (player.status.Hp <= 0)
                {
                    ConsoleUI.Instance.DrawTextInBox($"{player.status.Name}이(가) 쓰러졌다!", ref ConsoleUI.logView);
                    Game.Instance.SceneChange(Game.SceneState.Result);
                    return;
                }
            }
        }

        public static void Miss(Player player, List<Enemy> enemies)
        {
            ConsoleUI.Instance.DrawTextInBox("잘못된 입력입니다. 모든 적이 공격해 옵니다!", ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);

            foreach (var enemy in enemies)
            {
                enemy.Attack(player);
                if (player.status.Hp <= 0)
                {
                    ConsoleUI.Instance.DrawTextInBox($"{player.status.Name}이(가) 쓰러졌다!", ref ConsoleUI.logView);
                    Game.Instance.SceneChange(Game.SceneState.Result);
                    return;
                }
            }
        }


        public static Enemy SelectEnemy(List<Enemy> enemies)
        {
            ConsoleUI.inputView.ClearBuffer();
            ConsoleUI.Instance.DrawTextInBox("=== 적 선택 ===", ref ConsoleUI.inputView);
            for (int i = 0; i < enemies.Count; i++)
            {
                ConsoleUI.Instance.DrawTextInBox($"{i + 1}. {enemies[i].status.Name} (HP:{enemies[i].status.Hp})", ref ConsoleUI.inputView);
            }
            ConsoleUI.Instance.DrawTextInBox("선택 >>", ref ConsoleUI.inputView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);

            string? input = ConsoleUI.Read(ref ConsoleUI.inputView);
            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= enemies.Count)
            {
                return enemies[choice - 1];
            }
            else
            {
                return enemies[0]; // 잘못입력 시 첫 번째 몬스터
            }
        }


    }

}





