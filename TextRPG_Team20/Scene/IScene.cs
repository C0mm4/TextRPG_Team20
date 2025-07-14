using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal interface IScene
    {

        public virtual void Print()
        {
            Console.Clear();
            PrintScene();
            var input = GetAction();
            var isDelay = Action(input);
            if(isDelay)
                Thread.Sleep(500);
        }

        public void PrintScene();

        public int GetAction()
        {
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

        public bool Action(int input);
    }
}
