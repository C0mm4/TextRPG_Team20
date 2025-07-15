using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class LobbyScene : IScene
    {
        public bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.PopScene();
                    return false;
                case 1:
                    return false;
                default:
                    ((IScene)this).InvalidInput();
                    return true;
            }
        }

        public void PrintScene()
        {
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}Lobby{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("Please input your action", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1. Enter Dungeon", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0. Go to Title", ref ConsoleUI.mainView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
        }
    }
}
