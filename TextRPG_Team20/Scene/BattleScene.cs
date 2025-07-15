using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class BattleScene : IScene
    {
        private Character player;
        private Character enemy;


        public void PrintScene()
        {
            // 모든 영역 초기화
            ConsoleUI.mainView.ClearBuffer();
            ConsoleUI.logView.ClearBuffer();
            ConsoleUI.info1View.ClearBuffer();
            ConsoleUI.info2View.ClearBuffer();
            ConsoleUI.inputView.ClearBuffer();

            // 메인 뷰: 전투 메뉴
            ConsoleUI.Instance.DrawTextInBox("=== Battle Scene ===", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1. Attack", ref ConsoleUI.mainView);


            //  플레이어 정보
            ConsoleUI.Instance.DrawTextInBox($"[{player.Name}] - {player.Job}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"HP: {player.status.Hp}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"ATK: {player.status.TotalAtk}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"DEF: {player.status.TotalDef}", ref ConsoleUI.info1View);

            //  적 정보
            ConsoleUI.Instance.DrawTextInBox($"[{enemy.Name}] - {enemy.Job}", ref ConsoleUI.info2View);
            ConsoleUI.Instance.DrawTextInBox($"HP: {enemy.status.Hp}", ref ConsoleUI.info2View);
            ConsoleUI.Instance.DrawTextInBox($"ATK: {enemy.status.TotalAtk}", ref ConsoleUI.info2View);
            ConsoleUI.Instance.DrawTextInBox($"DEF: {enemy.status.TotalDef}", ref ConsoleUI.info2View);




            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);

        }
            public bool Action(int input)
        {
            ConsoleUI.logView.ClearBuffer();

            switch (input)
            {
                case 1: // 공격
                    ConsoleUI.Instance.DrawTextInBox($"{player.Name} attacks {enemy.Name}!", ref ConsoleUI.logView);
                    player.Attack(enemy);

                    if (enemy.status.Hp <= 0)
                    {
                        ConsoleUI.Instance.DrawTextInBox($"{enemy.Name} has been defeated!", ref ConsoleUI.logView);
                        Game.Instance.PopScene(); // 전투 종료
                        return false;
                    }

                    // 몬스터 공격
                    ConsoleUI.Instance.DrawTextInBox($"{enemy.Name} attacks back!", ref ConsoleUI.logView);
                    enemy.Attack(player);

                    if (player.status.Hp <= 0)
                    {
                        ConsoleUI.Instance.DrawTextInBox($"{player.Name} has fallen...", ref ConsoleUI.logView);
                        Game.Instance.SceneChange(Game.SceneState.Result);
                        return false;
                    }
                    break;

                default:
                    ((IScene)this).InvalidInput();
                    break;
            }
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            return true;
        }
    }
}
