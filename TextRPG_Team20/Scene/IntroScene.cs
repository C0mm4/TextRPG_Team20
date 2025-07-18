using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.System;

namespace TextRPG_Team20.Scene
{
    internal class IntroScene : Scene
    {
        private  string? nameInput;
        public override bool Action(int input)
        {
            switch (input) 
            {
                case 1:
                    ConsoleUI.Instance.DrawTextInBox("어서오세요!", ref ConsoleUI.mainView);
                    // Add Player Instance Initialize
                    Game.Instance.CreatePlayerInstance(nameInput);
                    //ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
                    Game.Instance.SceneChange(Game.SceneState.Lobby);
                    return false;
                case 2:
                    ConsoleUI.Instance.DrawTextInBox("확인했습니다", ref ConsoleUI.mainView);
                    ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
                    return true;
                default:
                    ((Scene)this).InvalidInput();
                    return true;

            }

        }

        public override void PrintScene()
        {            
            ConsoleUI.Instance.DrawTextInBox("페이팔스토리에 오신걸 환영합니다", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("이름을 입력해주세요 >> ", ref ConsoleUI.inputView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView, "left", "top");

            nameInput = ConsoleUI.Read(ref ConsoleUI.inputView);

            ConsoleUI.mainView.ClearBuffer();
            ConsoleUI.inputView.ClearBuffer();

            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Cyan}{nameInput}{AnsiColor.Reset} 이(가) 당신의 이름이 맞습니까?", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1.예", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("2.아니요", ref ConsoleUI.mainView);
            
            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
        }
    }
}
