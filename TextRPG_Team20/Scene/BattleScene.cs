using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Charactor.Enemys;
using TextRPG_Team20.System;

namespace TextRPG_Team20.Scene
{
    internal class BattleScene : Scene
    {
        private Player player;

        public BattleScene(Player player)
        {
            this.player = player;
        }

        internal static List<Enemy> CreateEnemys()
        {
            return new List<Enemy>
    {
                MobSpawnner.Instance.Create(0),
                MobSpawnner.Instance.Create(0),
                MobSpawnner.Instance.Create(0),
    };
        }


        public override void PrintScene()

        {
            if (Battle.enemies == null || Battle.enemies.Count == 0)
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}적을 마주쳤습니다!!{AnsiColor.Reset}", ref ConsoleUI.logView);
                Battle.enemies = CreateEnemys();
                player.ResetLastBattleGold();
                player.AddGold(2000000);
                player.AddSkill();
            }


            // 모든 영역 초기화

            ConsoleUI.SplitRect(ConsoleUI.mainView, out List<ConsoleUI.Rect> rects, Battle.enemies.Count, 1);

            for (int i = 0; i < Battle.enemies.Count; i++) 
            {
                ConsoleUI.Rect rect = rects[i];
                if (Battle.enemies[i] != null)
                {
                    Battle.enemies[i].DrawAscii(ref rect);
                }
            }

            // 메인 뷰: 전투 메뉴
            ConsoleUI.Instance.DrawTextInBox("=== 전투 장면 ===", ref ConsoleUI.mainView);
            Battle.OnBattle(player, Battle.enemies);

            // 플레이어 정보
            player.CharacterInfo();

            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);


        }

        public override bool Action(int input)
        {
            
                switch (input)
                {
                    case 1: // 공격
                        Battle.OnNormalAttack(player, Battle.enemies);
                        break;

                    case 2: //스킬사용
                        Battle.OnSkillAttack(player, Battle.enemies);
                        break;

                    default: // 잘못입력
                        //Battle.Miss(player, enemys);
                        ((Scene)this).InvalidInput();
                        break;
                }
            

            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            return true;
        }
    }
}
