using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Charactor.Enemys;

namespace TextRPG_Team20.Scene
{
    internal class WinScene : Scene
    {
        public override void PrintScene()
        {
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}전투에서 승리했습니다!{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}획득한 골드 : {Game.playerInstance.LastBattleGold} {AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("계속 진행하시겠습니까?", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0. 로비로 돌아가기", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1. 던전으로 돌아가기", ref ConsoleUI.mainView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);
            ConsoleUI.logView.ClearBuffer();
        }
        public override bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.ReturnToLobby();
                    return true;
                case 1:
                    Game.Instance.SceneChange(Game.SceneState.Battle);
                    return false;
                default:
                    ConsoleUI.Instance.DrawTextInBox("입력이 잘못되었습니다. 다시 입력해주세요.", ref ConsoleUI.logView);
                    ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
                    return true;
            }
        }
    }
}
