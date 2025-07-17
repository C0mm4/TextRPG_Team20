using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Charactor.Enemys;

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

        internal static List<Enemy> CreateEnemys()
        {
            return new List<Enemy>
    {
        new BlueSnail(),
        new StoneGolem(),
        new BlueSnail()

    };
        }


        public override void PrintScene()

        {
            if (this.enemys == null || this.enemys.Count == 0)
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}적을 마주쳤습니다!!{AnsiColor.Reset}", ref ConsoleUI.logView);
                this.enemys = CreateEnemys();
            }


            // 모든 영역 초기화
            ConsoleUI.mainView.ClearBuffer();
            ConsoleUI.info1View.ClearBuffer();
            ConsoleUI.info2View.ClearBuffer();
            ConsoleUI.inputView.ClearBuffer();

            // 메인 뷰: 전투 메뉴
            ConsoleUI.Instance.DrawTextInBox("=== Battle Scene ===", ref ConsoleUI.mainView);
            Battle.OnBattle(player, enemys);

            // 플레이어 정보
            player.CharacterInfo();

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);
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
                        Battle.OnNormalAttack(player, enemys);
                        break;

                    case 2: //스킬사용
                        Battle.OnSkillAttack(player, enemys);
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
