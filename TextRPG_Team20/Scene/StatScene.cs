using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class StatScene : IScene
    {

        private readonly Status Nowstatus;
        public StatScene(Status status)
        {
            Nowstatus = status;
        }


        public void PrintScene()
        {
            

            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}STAT{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"{Nowstatus.Level} Lv", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"HP       : {Nowstatus.Hp}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"ATK      : {Nowstatus.Atk}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"DEF      : {Nowstatus.Def}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"Extra ATK: {Nowstatus.ExtraAtk}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"Extra DEF: {Nowstatus.ExtraDef}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1. Go to Inventory", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0. Go to lobby", ref ConsoleUI.mainView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);


        }


        public bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.PopScene();
                    return false;
                case 1:
                    Game.Instance.SceneChange(Game.SceneState.Inventory);
                    return false;
                default:
                    Console.WriteLine("Input Error!");
                    return true;
            }
        }
    }
}
