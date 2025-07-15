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
        private Inventory _inventory;
        public InventoryScene(Inventory inventory)
        {
            _inventory = inventory;
        }
        //아이템 목록 가져오기
        //아이템 장착하기
        //


        public void PrintScene()
        {
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}인벤토리{AnsiColor.Reset}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox("보유 중인 아이템을 관리할 수 있습니다.", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox("Please input your action", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox("[아이템 목록]", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.info1View);

            //아이템 목록
            ConsoleUI.Instance.DrawTextInBox($"{_inventory.showItem}", ref ConsoleUI.info1View);

            ConsoleUI.Instance.DrawTextInBox("1. 장착 관리", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox("0. Go to Title", ref ConsoleUI.info1View);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View, "center", "middle");
        }

        public bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.PopScene();
                    return false;
                case 1:
                    Game.Instance.SceneChange(Game.SceneState.EquipControl);
                    return false;
                default:
                    ((IScene)this).InvalidInput();
                    return true;
            }
        }




    }
}

