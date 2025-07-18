using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class ResultScene : Scene
    {
        public override void PrintScene()
        {
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}전투에서 패배했습니다!{AnsiColor.Reset}", ref ConsoleUI.mainView);

            //획득한 골드
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}이번 던전에서 얻은 골드는 {/*얼마*/""} 입니다.{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0.로비로 돌아가기", ref ConsoleUI.mainView);

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
                    Game.Instance.GameStart();
                    return false;
                default:
                    ConsoleUI.Instance.DrawTextInBox("잘못된 입력입니다.", ref ConsoleUI.logView);
                    ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
                    return true;
            }
        }
    }
}

