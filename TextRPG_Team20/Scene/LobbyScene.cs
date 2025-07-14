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
                    return false;
                default:
                    Console.WriteLine("Input Error!");
                    return true;
            }
        }

        public void PrintScene()
        {
            Console.WriteLine($"{AnsiColor.Yellow}Lobby{AnsiColor.Reset}");
            Console.WriteLine("Please input your action");
            Console.WriteLine("1. Enter Dungeon");
            Console.WriteLine("0. Go to Title");
        }
    }
}
