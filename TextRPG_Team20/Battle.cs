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

        public static void OnBattle(Player player)
        {
            player.Action();
        }
        public static void OnNormalAttack(Player player, Enemy target, List<Enemy> enemies)
        {
            player.Attack(target);
            CheckWin(player, target, enemies);
        }

        public static void OnSkillAttack(Player player, Enemy target, List<Enemy> enemies)
        {
            player.UseSkill(target);
            CheckWin(player, target, enemies);
        }

        public static void Miss(Player player, List<Enemy> enemies)
        {
            ConsoleUI.Instance.DrawTextInBox("잘못된 입력입니다. 적들이 반격합니다.", ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            EnemyTurn(enemies, player);
        }

        public static void CheckWin(Player player, Enemy target, List<Enemy> enemies)
        {
            if (target.status.Hp <= 0)
            {
                ConsoleUI.Instance.DrawTextInBox($"{target.status.Name}을(를) 쓰러뜨렸다!", ref ConsoleUI.logView);
                player.AddGold(target.Gold);
            }

            if (enemies.All(e => e.IsDead))
            {
                ConsoleUI.Instance.DrawTextInBox("모든 적을 쓰러뜨렸습니다!", ref ConsoleUI.logView);
                Game.Instance.SceneChange(Game.SceneState.Win);
                return;
            }

            EnemyTurn(enemies, player);

            if (player.IsDead)
            {
                ConsoleUI.Instance.DrawTextInBox($"{player.status.Name}이(가) 쓰러졌다!", ref ConsoleUI.logView);
                Game.Instance.SceneChange(Game.SceneState.Result);
            }
        }

        public static void EnemyTurn(List<Enemy> enemies, Player player)
        {
            foreach (var enemy in enemies)
            {
                if (!enemy.IsDead)
                {
                    enemy.Action();
                    enemy.Attack(player);
                }
            }
        }

        public static void CheckEnemies(Player player, List<Enemy> enemies)
        {
            // 죽은 적 필터링
            var deadEnemies = enemies.Where(e => e.status.Hp <= 0).ToList();

            if (deadEnemies.Count > 0)
            {
                int totalGold = 0;

                foreach (var enemy in deadEnemies)
                {
                    ConsoleUI.Instance.DrawTextInBox($"{enemy.status.Name}이(가) 쓰러졌다!", ref ConsoleUI.logView);
                    totalGold += enemy.Gold;
                    enemies.Remove(enemy);
                }

                if (totalGold > 0)
                {
                    player.AddGold(totalGold);
                    ConsoleUI.Instance.DrawTextInBox($"총 {totalGold}G를 획득했습니다!", ref ConsoleUI.logView);
                }
            }



            // 모든 적이 죽었으면 전투 승리 처리
            if (enemies.Count == 0)
            {
                ConsoleUI.Instance.DrawTextInBox($"모든 적을 처치했습니다!", ref ConsoleUI.logView);
                Game.Instance.SceneChange(Game.SceneState.Win);
            }
        }


        public static Enemy SelectEnemy(List<Enemy> enemies)
        {
            while (true)
            {
                ConsoleUI.inputView.ClearBuffer();
                ConsoleUI.Instance.DrawTextInBox("=== 적 목록 ===", ref ConsoleUI.inputView);

                for (int i = 0; i < enemies.Count; i++)
                {
                    var e = enemies[i];
                    ConsoleUI.Instance.DrawTextInBox($"{i + 1}. {e.status.Name} (HP: {e.status.Hp})", ref ConsoleUI.inputView);
                }

                ConsoleUI.Instance.DrawTextInBox("공격할 적 번호를 입력하세요 >>", ref ConsoleUI.inputView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);

                string? s = ConsoleUI.Read(ref ConsoleUI.inputView);

                if (int.TryParse(s, out int idx) && idx >= 1 && idx <= enemies.Count)
                {
                    return enemies[idx - 1];
                }

                ConsoleUI.Instance.DrawTextInBox("잘못된 입력입니다.", ref ConsoleUI.logView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            }
        }
    }

}



