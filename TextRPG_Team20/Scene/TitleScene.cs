using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class TitleScene : IScene
    {

        public void PrintScene()
        {
            Console.WriteLine($"{AnsiColor.Yellow}Game Title{AnsiColor.Reset}");
            Console.WriteLine();
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Resume Game");
            Console.WriteLine();
            Console.WriteLine("0. End Game");
            Console.WriteLine();

        }


        public bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.PopScene();
                    Console.WriteLine("Bye bye");
                    return true;
                case 1:
                    Game.Instance.GameStart();
                    return false;
                case 2:
                    Game.Instance.LoadGame();
                    return false;
                default:
                    Console.WriteLine("Input Error!");
                    return true;
            }
        }
    }
}
