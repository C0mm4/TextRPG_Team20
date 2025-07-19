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
            ConsoleUI.Instance.DrawTextInBox("←왼쪽 이동           →오른쪽 이동       1 : 포션 사용", ref left);
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
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    return 5;
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

        public override void Print()
        {
            int input = 0;
            while (input != 4)
            {
                ClearBuffer();
                SetPlayerInfo();
                PrintUIViews();
                PrintScene();
                input = GetAction();
                Action(input);

                if (Game.playerInstance.playercollision(deltaX, deltaY))
                    break;
            }
        }

        int deltaX = 0;
        int deltaY = 0;
        public override bool Action(int input)
        {
            deltaX = 0;
            deltaY = 0;
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
                case 5:
                    {

                        List<Item> potions = Game.playerInstance.Inventory.Items
            .Where(i => i.data.Name.Contains("포션") || i.data.Name.Contains("엘릭서"))
            .ToList();

                        if (potions.Count == 0)
                        {
                            ConsoleUI.Instance.DrawTextInBox("사용 가능한 포션이 없습니다.", ref right);
                            ConsoleUI.Instance.PrintView(ref right, "left", "middle");
                            Thread.Sleep(1500);
                            ConsoleUI.ClearView(right);
                            return false;
                        }

                        int index = 0;
                        string cursor = ">>";
                        string notcursor = "  ";

                        while (true)
                        {
                            ConsoleUI.ClearView(right);
                            right.ClearBuffer();
                            ConsoleUI.Instance.DrawTextInBox("사용할 포션을 선택하세요", ref right);

                            for (int i = 0; i < potions.Count; i++)
                            {
                                var potion = potions[i];
                                string line = (index == i ? cursor : notcursor) +
                                              $" {potion.data.Name} (보유량 : {potion.CurrentStack}개)";
                                ConsoleUI.Instance.DrawTextInBox(line, ref right);
                            }

                            ConsoleUI.Instance.PrintView(ref right, "left", "middle");

                            var key = Console.ReadKey(true);

                            if (key.Key == ConsoleKey.Enter)
                            {
                                ConsumeItem selected = potions[index] as ConsumeItem;
                                if (selected.CurrentStack > 0)
                                {
                                    selected.Execute();
                                    return true;
                                }
                                else
                                {
                                    ConsoleUI.ClearView(right);
                                    ConsoleUI.Instance.DrawTextInBox("보유 수량이 없습니다!", ref ConsoleUI.logView);
                                    ConsoleUI.Instance.PrintView(ref right, "left", "middle");
                                    return true;
                                }
                            }
                            else if (key.Key == ConsoleKey.Escape)
                            {
                                ConsoleUI.ClearView(right);
                                return false;
                            }
                            else if (key.Key == ConsoleKey.UpArrow)
                            {
                                index = (index - 1 + potions.Count) % potions.Count;
                            }
                            else if (key.Key == ConsoleKey.DownArrow)
                            {
                                index = (index + 1) % potions.Count;
                            }
                        }
                    }
                    break;
                default:
                    InvalidInput();
                    return false;

            }

            return false;
        }
    }
}
