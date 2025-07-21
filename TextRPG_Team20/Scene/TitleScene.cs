using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextRPG_Team20.ConsoleUI;

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
            if (File.Exists("SaveData.json"))
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
                    bool confirmExit = GetYesNoConfirmation(
                        "정말 게임을 종료하시겠습니까?", ref ConsoleUI.mainView);

                    if (confirmExit) 
                    {
                        
                        Game.Instance.GameEnd(); 
                        return false;
                    }
                    else 
                    {
                       
                        ConsoleUI.Instance.DrawTextInBox("게임을 계속합니다.", ref ConsoleUI.logView);
                        ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
                        return true;
                    }
                case 1:
                    Game.Instance.GameStart();
                    return false;
                case 2:

                    if (File.Exists("SaveData.json"))
                    {

                        Game.Instance.LoadGame();
                        return false;
                    }
                    InvalidInput();
                    return true;
                default:
                    ((Scene)this).InvalidInput();
                    return true;
            }
        }
        public bool GetYesNoConfirmation(string message, ref ConsoleUI.Rect targetView, bool clearViewAfterInput = true)
        {
            targetView.ClearBuffer();
            ConsoleUI.Instance.DrawTextInBox("  게임 종료 ", ref targetView);
            ConsoleUI.Instance.DrawTextInBox("", ref targetView);

            ConsoleUI.Instance.DrawTextInBox(message, ref targetView);

            int index = 0;
            string cursor = " >>";
            string notcursor = "";

            while (true)
            {

                if (targetView.lines.Count > 0)
                {

                    RemoveLines(ref targetView, 1, RemoveLinePos.back);
                }


                ConsoleUI.Instance.DrawTextInBox($"{(index == 0 ? cursor : notcursor)} Y / {(index == 1 ? cursor : notcursor)} N ", ref targetView);
                ConsoleUI.Instance.PrintView(ref targetView, "center", "middle");

                Console.CursorVisible = true;
                var key = Console.ReadKey(true);
                Console.CursorVisible = false;

                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    index = 1;
                    break;
                }
                else if (key.Key == ConsoleKey.RightArrow)
                {
                    index = Math.Min(1, index + 1);
                }
                else if (key.Key == ConsoleKey.LeftArrow)
                {
                    index = Math.Max(0, index - 1);
                }
            }

            if (clearViewAfterInput)
            {
                targetView.ClearBuffer();
                ConsoleUI.Instance.PrintView(ref targetView);
            }


            return index == 0;
        }
    }
}
