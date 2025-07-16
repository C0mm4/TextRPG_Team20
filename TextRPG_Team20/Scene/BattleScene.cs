using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class BattleScene : Scene
    {
        private Player player;
        private List<Enemy> enemys = new List<Enemy>();

        public BattleScene(Player player, List<Enemy> enemys)
        {
            this.player = player;
            this.enemys = enemys;
        }

        public override void PrintScene()

        {
            
            // 모든 영역 초기화
            ConsoleUI.mainView.ClearBuffer();
            ConsoleUI.info1View.ClearBuffer();
            ConsoleUI.info2View.ClearBuffer();
            ConsoleUI.inputView.ClearBuffer();

            // 메인 뷰: 전투 메뉴
            ConsoleUI.Instance.DrawTextInBox("=== Battle Scene ===", ref ConsoleUI.mainView);
            Battle.OnBattle(player, enemys);

            //  플레이어 정보
            ConsoleUI.Instance.DrawTextInBox($"[{player.status.Name}] - {player.Job}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"HP: {player.status.Hp}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"ATK: {player.status.TotalAtk}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"DEF: {player.status.TotalDef}", ref ConsoleUI.info1View);



            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);


        }

        public override bool Action(int input)
        {
            
            if (player.status.Hp > 0)
            {
                switch (input)
                {
                    case 1: // 공격
                        Battle.OnNormalAttack(player, enemys);
                        break;

                    case 2: //스킬사용
                        Battle.OnSkillAttack(player, enemys);
                        break;

                    default: // 잘못입력
                        Battle.Miss(player, enemys);
                        ((Scene)this).InvalidInput();
                        break;
                }
            }
            else
            {
                Game.Instance.SceneChange(Game.SceneState.Result);
            }
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            return true;
        }
    }
}
