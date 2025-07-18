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
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                    if(DungeonManager.Instance.isAbleDungeon.Length >= input)
                    {
                        if (DungeonManager.Instance.isAbleDungeon[input-1])
                        {
                            ConsoleUI.Instance.DrawTextInBox("Enetering Dungeon...", ref ConsoleUI.inputView);
                            DungeonManager.Instance.StartDungone(input);
                            Game.Instance.SceneChange(Game.SceneState.InField);

                        }
                        else
                        {
                            InvalidInput();
                        }
                    }
                    else
                    {
                        InvalidInput();
                    }

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
            ConsoleUI.Instance.DrawTextInBox("Choose a dungeon to enter:", ref ConsoleUI.mainView);
            for (int i = 0; i < DungeonManager.Instance.isAbleDungeon.Length; i++)
            {
                if (DungeonManager.Instance.isAbleDungeon[i])
                {

                    ConsoleUI.Instance.DrawTextInBox($"{i+1}. {DungeonManager.Instance.GetDungeon(i+1).DungeonName}", ref ConsoleUI.mainView);
                }
                else
                {
                    break;
                }
            }
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0. Back to Lobby", ref ConsoleUI.mainView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);
        }
    }
    

    
}
