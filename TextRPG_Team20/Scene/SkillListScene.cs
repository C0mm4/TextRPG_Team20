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


            ConsoleUI.Instance.DrawTextInBox("당신의 행동을 입력해주세요", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0.로비로 돌아가기", ref ConsoleUI.mainView);
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
