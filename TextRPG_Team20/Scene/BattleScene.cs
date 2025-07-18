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
            int currentFieldID = Game.Instance.GetCurrentFieldID();
            return MobSpawnner.Instance.GetEnemiesForField(currentFieldID);
        }


        public override void PrintScene()

        {
            if (Battle.enemies == null || Battle.enemies.Count == 0)
            {
                
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}적을 마주쳤습니다!!{AnsiColor.Reset}", ref ConsoleUI.logView);
                Battle.enemies = CreateEnemys();
                player.ResetLastBattleGold();

            }


            // 모든 영역 초기화

            ConsoleUI.SplitRect(ConsoleUI.mainView, out List<ConsoleUI.Rect> rects, Math.Max(Battle.enemies.Count, 1), 1);

            for (int i = 0; i < Battle.enemies.Count; i++) 
            {
                ConsoleUI.Rect rect = rects[i];
                if (Battle.enemies[i] != null)
                {
                    Battle.enemies[i].DrawAscii(ref rect);
                }
            }


            // 메인 뷰: 전투 메뉴
            Battle.OnBattle(player, Battle.enemies);


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
                    return true;
            }

            if (Battle.enemies.Count == 0)
            {
                Game.Instance.SceneChange(Game.SceneState.Win);
                return true;
            }

            foreach (var enemy in Battle.enemies)
            {
                enemy.Attack(player);
                if (player.status.HP <= 0)
                {
                    ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}{player.status.Name}이(가) 쓰러졌다!{AnsiColor.Reset}", ref ConsoleUI.logView);
                    Game.Instance.SceneChange(Game.SceneState.Defeat);
                    return true;
                }
            }

            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            return true;
        }
    }
}
