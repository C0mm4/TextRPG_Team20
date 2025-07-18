using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Dungeon;
using static TextRPG_Team20.Game;

namespace TextRPG_Team20.Scene
{
    internal class DungeonSelectScene : Scene
    {
     
        public override bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.PopScene(); // 이전 씬으로 돌아간다
                    return false;

                case 1:
                    Console.WriteLine("던전에 입장합니다...");
                    DungeonManager.Instance.StartDungone(1);
                    Game.Instance.SceneChange(Game.SceneState.InField);
                    return true;



                default:
                    ((Scene)this).InvalidInput();
                    return true;
            }
        }
        public override void PrintScene()
        {
            ConsoleUI.mainView.ClearBuffer();
            ConsoleUI.logView.ClearBuffer();
            ConsoleUI.info1View.ClearBuffer();
            ConsoleUI.info2View.ClearBuffer();
            ConsoleUI.inputView.ClearBuffer();

            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Cyan}Dungeon Select{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("입장할 던전을 선택하세요:", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1. 던전", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0. 로비로 돌아가기", ref ConsoleUI.mainView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);
        }
    }
    

    
}
