using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal interface IScene
    {
        public void DrawUI()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("┌ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ┐ ┌ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ┐");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │"); 
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("└ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ┘ │                                           │");

            Console.WriteLine("┌ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ┐   ┌ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ┐ │                                           │");
            Console.WriteLine("│                                                     │   │                                                     │ │                                           │");
            Console.WriteLine("│                                                     │   │                                                     │ │                                           │");
            Console.WriteLine("│                                                     │   │                                                     │ │                                           │");
            Console.WriteLine("│                                                     │   │                                                     │ │                                           │");
            Console.WriteLine("│                                                     │   │                                                     │ │                                           │");
            Console.WriteLine("│                                                     │   │                                                     │ │                                           │");
            Console.WriteLine("│                                                     │   │                                                     │ │                                           │");
            Console.WriteLine("└ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ┘   └ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ┘ │                                           │");

            Console.WriteLine("┌ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ┐ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("│                                                                                                               │ │                                           │");
            Console.WriteLine("└ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ┘ └ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ─ ┘");

        }

        public virtual void Print()
        {
            Console.Clear();
            ClearBuffer();
//            DrawUI();
            SetPlayerInfo();
            PrintUIViews();
            PrintScene();
            var input = GetAction();
            var isDelay = Action(input);
            if(isDelay)
                Thread.Sleep(500);
        }

        public void ClearBuffer()
        {
            // 혹시 모를 싱글톤 생성 전 Clear 방지 위한 싱글톤 호출
            var c = ConsoleUI.Instance;
            ConsoleUI.mainView.ClearBuffer();
            ConsoleUI.info1View.ClearBuffer();
            ConsoleUI.info2View.ClearBuffer();
            ConsoleUI.inputView.ClearBuffer();

            ConsoleUI.mainView.DrawRect();
            ConsoleUI.info1View.DrawRect();
            ConsoleUI.info2View.DrawRect();
            ConsoleUI.inputView.DrawRect();
            ConsoleUI.logView.DrawRect();
        }



        public void PrintScene();

        public virtual int GetAction()
        {
            ConsoleUI.Instance.DrawTextInBox("Please input your action >>", ref ConsoleUI.inputView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);
            string? s = ConsoleUI.Read(ref ConsoleUI.inputView);
            var isAble = int.TryParse(s, out var action);
            if (isAble)
            {
                return action;
            }
            else
            {
                return -1;
            }
        }
        public void InvalidInput()
        {
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}Input Error!{AnsiColor.Reset}", ref ConsoleUI.inputView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView, "left", "top");
        }

        /// <summary>
        /// 입력 값으로 씬의 행동을 서술하는 메소드
        /// </summary>
        /// <param name="input">입력값</param>
        /// <returns>딜레이 트리거 boolean</returns>
        public bool Action(int input);

        public void SetPlayerInfo()
        {
            ConsoleUI.info1View.ClearBuffer();
            ConsoleUI.info2View.ClearBuffer();

            if (Game.playerInstance != null)
                Game.playerInstance.CharacterInfo();
        }

        public void PrintUIViews()
        {
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);
        }
    }
}
