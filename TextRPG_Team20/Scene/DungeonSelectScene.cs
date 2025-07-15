using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextRPG_Team20.Game;

namespace TextRPG_Team20.Scene
{
    internal class DungeonSelectScene : IScene
    {
     
        public bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.PopScene(); // 이전 씬으로 돌아간다
                    return false;

                case 1:
                    Console.WriteLine("Enetering Dungeon...");
                    Game.Instance.SceneChange(Game.SceneState.InField);
                    return true;



                default:
                    Console.WriteLine("Invalid input. Try again."); // 잘못된 입력
                    return true;
            }
        }
        public void PrintScene()
        {
            ConsoleUI.mainView.ClearBuffer();
            ConsoleUI.logView.ClearBuffer();
            ConsoleUI.info1View.ClearBuffer();
            ConsoleUI.info2View.ClearBuffer();
            ConsoleUI.inputView.ClearBuffer();

            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Cyan}Dungeon Select{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("Choose a dungeon to enter:", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1. Dungeon", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0. Back to Lobby", ref ConsoleUI.mainView);

            ConsoleUI.Instance.PrintView(ConsoleUI.mainView);
            ConsoleUI.Instance.PrintView(ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ConsoleUI.info1View);
            ConsoleUI.Instance.PrintView(ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ConsoleUI.inputView);
        }
    }
    

    
}
