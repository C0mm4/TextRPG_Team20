using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Item;

namespace TextRPG_Team20.Scene
{
    internal class InventoryScene : IScene
    {
        public bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.SceneChange(Game.SceneState.Lobby);
                    return false;
                case 1:
                    Game.Instance.SceneChange(Game.SceneState.EquipControl);
                    return false;
                default:
                    ((IScene)this).InvalidInput();
                    return true;
            }
        }
   
  

        public void PrintScene()
        {            
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}인벤토리{AnsiColor.Reset}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox("Please input your action", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox("1. 장착 관리", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox("0. Go to Title", ref ConsoleUI.info1View);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View, "center", "middle");


            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();


            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine(">>");

        }
    }
}
