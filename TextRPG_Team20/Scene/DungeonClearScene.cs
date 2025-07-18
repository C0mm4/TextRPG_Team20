using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Dungeon;

namespace TextRPG_Team20.Scene
{
    internal class DungeonClearScene : Scene
    {
        public override bool Action(int input)
        {
            switch (input)
            {
                default:
                    ConsoleUI.Instance.DrawTextInBox("로비로 돌아갑니다.", ref ConsoleUI.logView);
                    DungeonManager.Instance.isDungeonClear[DungeonManager.Instance.currentDungeon.DungeonID - 1] = true;
                    ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
                    Game.Instance.ReturnToLobby();
                    return true;
            }
        }

        public override int GetAction()
        {
            Console.ReadKey();
            return 0;
        }

        public override void PrintScene()
        {
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}{DungeonManager.Instance.currentDungeon.DungeonName}을 클리어했습니다!{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}이번 던전에서 획득한 골드 : {DungeonManager.Instance.currentDungeon.GetGold} {AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("아무 키를 눌러 진행하세요", ref ConsoleUI.mainView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);
        }
    }
}
