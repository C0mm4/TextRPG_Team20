using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class TitleScene : IScene
    {

        public void PrintScene()
        {
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}Game Title{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1. Start Game", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("2. Resume Game", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0. End Game", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);


            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
        }


        public bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.PopScene();
                    ConsoleUI.Instance.DrawTextInBox("Bye Bye", ref ConsoleUI.mainView);
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
                    ((IScene)this).InvalidInput();
                    return true;
            }
        }
    }
}
