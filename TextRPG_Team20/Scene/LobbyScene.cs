using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class LobbyScene : Scene
    {
        public override bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.ReturnToTitle();
                    return false;
                case 1:
                    Game.Instance.SceneChange(Game.SceneState.DungeonSelect);
                    return false;
                case 2:
                    Game.Instance.SceneChange(Game.SceneState.Shop);
                    return false;
                case 3:
                    Game.Instance.SceneChange(Game.SceneState.Status);
                    return false;
                case 4:
                    Game.Instance.SceneChange(Game.SceneState.Inventory);
                    return false;
                case 5:
                    Game.Instance.SceneChange(Game.SceneState.Quest);
                    return false;
                default:
                    ((Scene)this).InvalidInput();
                    return true;
            }
        }

        public override void PrintScene()
        {
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}로비{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("원하시는 행동을 입력해주세요", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1.던전 입장", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("2.상점 입장", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("3.상태창 보기", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("4.인벤토리", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("5.퀘스트", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0.타이틀로 돌아가기", ref ConsoleUI.mainView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
        }
    }
}
