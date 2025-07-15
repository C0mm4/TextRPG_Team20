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
            DrawUI();
            PrintScene();
            var input = GetAction();
            var isDelay = Action(input);
            if(isDelay)
                Thread.Sleep(500);
        }

        public void PrintScene();

        public int GetAction()
        {
            Console.SetCursorPosition(2, 48);
            Console.Write("Please input your action >> ");
            string? s = Console.ReadLine();
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

        /// <summary>
        /// 입력 값으로 씬의 행동을 서술하는 메소드
        /// </summary>
        /// <param name="input">입력값</param>
        /// <returns>딜레이 트리거 boolean</returns>
        public bool Action(int input);
    }
}
