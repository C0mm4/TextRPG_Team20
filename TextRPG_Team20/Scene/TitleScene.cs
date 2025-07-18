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
            ConsoleUI.mainView.ClearBuffer();
            ConsoleUI.logView.ClearBuffer();
            ConsoleUI.info1View.ClearBuffer();
            ConsoleUI.info2View.ClearBuffer();
            ConsoleUI.inputView.ClearBuffer();

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);

            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}페이팔스토리{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1.게임 시작", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("2.게임 재개", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0.게임 종료", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);

           

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");

            Console.SetCursorPosition(0, Console.WindowHeight - 1);
        }


        public override bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    
                    ConsoleUI.Instance.DrawTextInBox("정말 종료하시겠습니까?", ref ConsoleUI.mainView);
                    ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
                    Console.SetCursorPosition(0, 49);
                    Game.Instance.GameEnd();
                    return false;
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
