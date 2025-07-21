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

            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Cyan}던전 선택{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("입장할 던전을 선택하세요:", ref ConsoleUI.mainView);
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
            ConsoleUI.Instance.DrawTextInBox("0.로비로 돌아가기", ref ConsoleUI.mainView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);
        }
    }
    

    
}
