using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class IntroScene : Scene
    {
        string? nameInput;
        public override bool Action(int input)
        {
            switch (input) 
            {
                case 1:
                    ConsoleUI.Instance.DrawTextInBox("Welcome", ref ConsoleUI.mainView);
                    // Add Player Instance Initialize
                    Game.Instance.CreatePlayerInstance(nameInput);
                    ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
                    Game.Instance.SceneChange(Game.SceneState.Lobby);
                    return false;
                case 2:
                    ConsoleUI.Instance.DrawTextInBox("Okay...", ref ConsoleUI.mainView);
                    ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
                    return true;
                default:
                    ((Scene)this).InvalidInput();
                    return true;

            }

        }

        public override void PrintScene()
        {            
            ConsoleUI.Instance.DrawTextInBox("Welcome to this game", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("Please input your charachor name >> ", ref ConsoleUI.inputView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView, "left", "top");

            nameInput = ConsoleUI.Read(ref ConsoleUI.inputView);

            ConsoleUI.mainView.ClearBuffer();
            ConsoleUI.inputView.ClearBuffer();

            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Cyan}{nameInput}{AnsiColor.Reset} is your name. right?", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1. Yes", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("2. No", ref ConsoleUI.mainView);
            
            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
        }
    }
}
