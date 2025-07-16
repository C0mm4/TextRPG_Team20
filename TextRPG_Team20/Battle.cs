using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20
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
        public List<Enemy.> Items { get; private set; }
        private Inventory _inventory;


        public static bool OnBattle(Character player, Character enemy)
        {
            // 1. 플레이어 턴
            int playerDamage = Math.Max(player.status.TotalAtk - enemy.status.TotalDef, 1);
            enemy.status.Hp -= playerDamage;

            if (enemy.status.Hp <= 0)
            {
                return true;  // 전투 종료 신호
            }

            // 2. 적 턴
            int enemyDamage = Math.Max(enemy.status.TotalAtk - player.status.TotalDef, 1);
            player.status.Hp -= enemyDamage;

            if (player.status.Hp <= 0)
            {
                return true;  // 전투 종료
            }

            return false; // 둘 다 살아있으면 전투 계속
        }
    }
}
