using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class SkillListScene : Scene
    {

        public override void PrintScene()
        {
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}스킬 목록{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            //스킬 목록을 받아와서 출력한다.


            ConsoleUI.Instance.DrawTextInBox("Please input your action", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0. Go to lobby", ref ConsoleUI.mainView);
        }


        public override bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.PopScene();
                    return false;

                default:
                    ((Scene)this).InvalidInput();
                    return true;
            }
        }
    }
}
