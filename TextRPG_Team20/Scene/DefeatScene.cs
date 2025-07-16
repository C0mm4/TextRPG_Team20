using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Charactor.Enemys;

namespace TextRPG_Team20.Scene
{
    internal class DefeatScene : Scene
    {


        public override void PrintScene()
        {
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}패배했습니다.{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("로비로 돌아가시겠습니까?", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0. Go to lobby", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1. Go to title", ref ConsoleUI.mainView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);
            
        }






        public override bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.ReturnToLobby();
                    ConsoleUI.logView.ClearBuffer();
                    ConsoleUI.Instance.DrawTextInBox("어라.. 꿈이었나..", ref ConsoleUI.logView);
                    Game.playerInstance.AddGold(-(int)(Game.playerInstance.Gold * 0.5));
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
