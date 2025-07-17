using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Item;

namespace TextRPG_Team20.Scene
{
    internal class InventoryScene : Scene
    {
        private Inventory _inventory;

        //Inventory equipInventory = new Inventory();          //장착 아이템
        //Inventory consumableInventory = new Inventory();     //소비 아이템


        public InventoryScene(Inventory inventory)
        {
            _inventory = inventory;


        }


        public override void PrintScene()
        {
            
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}인벤토리{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("보유 중인 아이템을 관리할 수 있습니다.", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("Please input your action", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("[아이템 목록]", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            for (int i = 0; i < _inventory.Items.Count; i++)
            {
                ConsoleUI.Instance.DrawTextInBox($"{_inventory.showItem(i)}", ref ConsoleUI.mainView);
            }
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("장착할 아이템 번호를 입력하세요.", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0. Go to Title", ref ConsoleUI.mainView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "left", "top");
        }

        public override bool Action(int input)
        {
            switch (input)
            {
                case 0:

                    Game.Instance.PopScene();
                    return true;

                default:
                    if (_inventory.Items == null)
                    {
                        ((Scene)this).InvalidInput();
                    }
                    else
                    {
                        _inventory.EquipItem(input);
                        
                    }
                    return true;
            }
        }

    }
}

