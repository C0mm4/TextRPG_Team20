using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Scene;

namespace TextRPG_Team20.Scene
{
    internal class Battle
    {
        //충돌하면
        //player enemy join
        // OnBattle

        // player turn
        // player attack
        // enemy turn 
        // enemy attack

        public static bool OnBattle(Player player, Enemy enemy)
        {
            int action = player.GetPlayerAction();
                        
            switch (action)
            {
                case 1:
                    player.Attack(enemy);
                    break;
                case 2:
                    player.UseSkill(enemy);
                    break;
                default:
                    ConsoleUI.Instance.DrawTextInBox("잘못된 입력입니다. 턴을 소모합니다.", ref ConsoleUI.logView);
                    break;
            }

            // 2. 적 사망 체크
            if (enemy.status.Hp <= 0)
            {
                ConsoleUI.Instance.DrawTextInBox($"{enemy.status.Name}이(가) 쓰러졌다!", ref ConsoleUI.logView);
                return false; // 전투 종료
            }

            // 3. 적 턴
            enemy.Attack(player);

            // 4. 플레이어 사망 체크
            if (player.status.Hp <= 0)
            {
                ConsoleUI.Instance.DrawTextInBox($"{player.status.Name}이(가) 쓰러졌다!", ref ConsoleUI.logView);
                return false; // 전투 종료
            }

            return true; // 둘 다 살아있으면 계속 전투
        }
    }


}
