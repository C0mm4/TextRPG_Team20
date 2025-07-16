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
                    Game.Instance.SceneChange(Game.SceneState.DungeonSelect);
                    return false;
                case 2:
                    Game.Instance.SceneChange(Game.SceneState.Shop);
                    return false;
                case 3:
                    Game.Instance.SceneChange(Game.SceneState.Status);
                    return false;
                case 4:
                    Game.Instance.SceneChange(Game.SceneState.Inventory);
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
            ConsoleUI.Instance.DrawTextInBox("2. Enter Shop", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("3. View Status", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("4. Inventory", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0. Go to Title", ref ConsoleUI.mainView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
        }
    }
}
