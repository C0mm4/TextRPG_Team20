using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class IntroScene : IScene
    {
        string? nameInput;
        public bool Action(int input)
        {
            switch (input) 
            {
                case 1:
                    Console.WriteLine("Welcome");
                    // Add Player Instance Initialize
                    Game.Instance.CreatePlayerInstance(nameInput);
                    Game.Instance.SceneChange(Game.SceneState.Lobby);
                    return false;
                case 2:
                    Console.WriteLine("Okay...");
                    return true;
                default:
                    Console.WriteLine("Input Error!");
                    return true;

            }

        }

        public void PrintScene()
        {
            Console.WriteLine("Welcome to this game");
            Console.Write("Please input your charachor name >> ");
            nameInput = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine($"{AnsiColor.Cyan}{nameInput}{AnsiColor.Reset} is your name. right?");
            Console.WriteLine();
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
        }
    }
}
