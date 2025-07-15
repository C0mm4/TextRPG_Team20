using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextRPG_Team20.Game;

namespace TextRPG_Team20.Scene
{
    internal class DungeonSelectScene : IScene
    {

        public bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.PopScene(); // 이전 씬으로 돌아간다
                    return false;

                case 1:
                    Console.WriteLine("Enetering Dungeon...");
                    Game.Instance.SceneChange(Game.SceneState.InField);
                    return true;



                default:
                    Console.WriteLine("Invalid input. Try again."); // 잘못된 입력
                    return true;
            }
        }
        public void PrintScene()
        {
            Console.WriteLine($"{AnsiColor.Cyan}Dungeon Select{AnsiColor.Reset}");
            Console.WriteLine("Choose a dungeon to enter:");
            Console.WriteLine("Dungeon");
            Console.WriteLine();
            Console.WriteLine("0. Back to Lobby");
        }
    }
    

    
}
