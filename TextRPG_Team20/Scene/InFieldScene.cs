using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Dungeon;

namespace TextRPG_Team20.Scene
{
    internal class InFieldScene : Scene
    {
        public override int GetAction()
        {
            ConsoleUI.Instance.DrawTextInBox("           ↑위로 이동", ref ConsoleUI.inputView);
            ConsoleUI.Instance.DrawTextInBox("←왼쪽 이동           →오른쪽 이동", ref ConsoleUI.inputView);
            ConsoleUI.Instance.DrawTextInBox("           ↓하단 이동", ref ConsoleUI.inputView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView, "left", "middle");

            ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
            var key = keyInfo.Key;
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    return 0;
                case ConsoleKey.RightArrow:
                    return 1;
                case ConsoleKey.UpArrow:
                    return 2;
                case ConsoleKey.DownArrow:
                    return 3;
            }

            return -1;
        }

        public override void PrintScene()
        {
            var fields = DungeonManager.Instance.currentField.ToPrintString();
            foreach (var field in fields)
            {
                ConsoleUI.Instance.DrawTextInBox(field, ref ConsoleUI.mainView);
            }

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
        }

        public override bool Action(int input)
        {
            int deltaX = 0;
            int deltaY = 0;
            switch (input)
            {
                case 0:
                    deltaX = -1;
                    break;
                case 1:
                    deltaX = 1;
                    break;
                case 2:
                    deltaY = -1;
                    break;
                case 3:
                    deltaY = 1;
                    break;
                default:
                    InvalidInput();
                    return true;

            }

            Game.playerInstance.playercollision(deltaX, deltaY);
            return false;
        }
    }
}
