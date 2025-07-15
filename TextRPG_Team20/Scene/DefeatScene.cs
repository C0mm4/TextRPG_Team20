using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class DefeatScene : IScene
    {


        public void PrintScene()
        {
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}패배했습니다.{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("로비로 돌아가시겠습니까?", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0. Go to lobby", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1. Go to title", ref ConsoleUI.mainView);


        }




   

        public bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.ReturnToLobby();
                    return true; 
                case 1:
                    Game.Instance.GameStart();
                    return false;
                default:
                    Console.WriteLine("Input Error!");
                    return true;
            }
        }

    }
}
