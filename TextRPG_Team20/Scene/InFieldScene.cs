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
        ConsoleUI.Rect left, right;
        public override int GetAction()
        {
            ConsoleUI.SplitRectHorizontal(ConsoleUI.inputView, out left, out right);

            ConsoleUI.Instance.DrawTextInBox("           ↑위로 이동", ref left);
            ConsoleUI.Instance.DrawTextInBox("←왼쪽 이동           →오른쪽 이동", ref left);
            ConsoleUI.Instance.DrawTextInBox("           ↓하단 이동                    0 : 던전 탈출", ref left);

            ConsoleUI.Instance.PrintView(ref left, "left", "middle");

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
                case ConsoleKey.NumPad0:
                case ConsoleKey.D0:
                    return 4;
                    break;
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
                case 4:
                    ConsoleUI.Instance.DrawTextInBox("던전을 나가시겠습니까?", ref right);
                    {
                        int index = 0;
                        string cursor = " >>";
                        string notcursor = "";
                        while (true)
                        {
                            ConsoleUI.Instance.DrawTextInBox($"{(index == 0 ? cursor : notcursor)} Y / {(index == 1 ? cursor : notcursor)} N ", ref right);
                            ConsoleUI.Instance.PrintView(ref right, "left", "middle");
                            var key = Console.ReadKey();

                            if(key.Key == ConsoleKey.Enter)
                            {
                                break;
                            }
                            else if (key.Key == ConsoleKey.Escape)
                            {
                                index = 1;
                                break;
                            }
                            else if(key.Key == ConsoleKey.RightArrow)
                            {
                                index = Math.Min(1, index + 1);
                            }
                            else if(key.Key == ConsoleKey.LeftArrow)
                            {
                                index = Math.Max(0, index - 1);
                            }
                            ConsoleUI.RemoveLines(ref right, 1);
                        }
                        if(index == 0)
                        {
                            Game.Instance.ReturnToLobby();
                            return true;
                        }
                        else
                        {
                            ConsoleUI.ClearView(right);
                            return false;
                        }
                    }
                default:
                    InvalidInput();
                    return true;

            }

            Game.playerInstance.playercollision(deltaX, deltaY);
            return false;
        }
    }
}
