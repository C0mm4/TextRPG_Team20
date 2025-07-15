using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class IntroScene : IScene
    {
        string? nameInput;
        public bool Action(int input)
        {
            switch (input) 
            {
                case 1:
                    Console.WriteLine("Welcome");
                    // Add Player Instance Initialize
                    Game.Instance.CreatePlayerInstance(nameInput);
                    Game.Instance.SceneChange(Game.SceneState.Lobby);
                    return false;
                case 2:
                    Console.WriteLine("Okay...");
                    return true;
                default:
                    Console.WriteLine("Input Error!");
                    return true;

            }

        }

        public void PrintScene()
        {
            ConsoleUI.Instance.DrawTextInBox("메인 뷰 테스트", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("메인 뷰 테스트", ref ConsoleUI.mainView);

            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트1", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트2", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트3", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트4", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트5", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트6", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트7", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트8", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트9", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트10", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트11", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트12", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트13", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트14", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트15", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트16", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트17", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트18", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트19", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트20", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트21", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트22", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트23", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트24", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트25", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트26", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트27", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트28", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트29", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트30", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트31", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트32", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트33", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트34", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트35", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트36", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트37", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트38", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트39", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트40", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트41", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트42", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트43", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트44", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트44", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트44", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트44", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트44", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("로그 뷰 테스트43", ref ConsoleUI.logView);

            ConsoleUI.Instance.DrawTextInBox("Info1 테스트", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox("Info2 테스트", ref ConsoleUI.info2View);
            ConsoleUI.Instance.DrawTextInBox("Input 테스트", ref ConsoleUI.inputView);
            ConsoleUI.Instance.DrawTextInBox("Info1 테스트", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox("Info2 테스트", ref ConsoleUI.info2View);
            ConsoleUI.Instance.DrawTextInBox("Input 테스트", ref ConsoleUI.inputView);

            ConsoleUI.Instance.PrintView(ConsoleUI.mainView, "right", "top");
            ConsoleUI.Instance.PrintView(ConsoleUI.logView, "right", "top");
            ConsoleUI.Instance.PrintView(ConsoleUI.info1View, "right", "top");
            ConsoleUI.Instance.PrintView(ConsoleUI.info2View, "right", "top");
            ConsoleUI.Instance.PrintView(ConsoleUI.inputView, "right", "top");
            /*
            ConsoleUI.Instance.DrawTextInBox("Welcome to this game", ref ConsoleUI.mainView);
            Console.Write("Please input your charachor name >> ");
            nameInput = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine($"{AnsiColor.Cyan}{nameInput}{AnsiColor.Reset} is your name. right?");
            Console.WriteLine();
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            */
        }
    }
}
