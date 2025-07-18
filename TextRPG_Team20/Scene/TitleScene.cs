using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class TitleScene : Scene
    {

        public override void PrintScene()
        {
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}게임 이름{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1. 게임 시작", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("2.게임 재개", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0. 게임 종료", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);


            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
        }


        public override bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.PopScene();
                    ConsoleUI.Instance.DrawTextInBox("당신의 다음 도전을 기다리겠습니다.", ref ConsoleUI.mainView);
                    ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
                    Console.SetCursorPosition(0, 49);
                    return true;
                case 1:
                    Game.Instance.GameStart();
                    return false;
                case 2:
                    Game.Instance.LoadGame();
                    return false;
                default:
                    ((Scene)this).InvalidInput();
                    return true;
            }
        }
    }
}
