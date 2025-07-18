using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.System;

namespace TextRPG_Team20.Scene
{
    internal class InventoryScene : Scene
    {
        private Inventory _inventory;

        


        public InventoryScene(Inventory inventory)
        {
            _inventory = inventory;
        }   
            


        public override void PrintScene()
        {

            //_inventory.AddItem(ItemManager.Instance.Create(446));
            //_inventory.AddItem(ItemManager.Instance.Create(463));
            //_inventory.AddItem(ItemManager.Instance.Create(469));
            //_inventory.AddItem(ItemManager.Instance.Create(475));
            //_inventory.AddItem(ItemManager.Instance.Create(493));


            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}인벤토리{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("보유 중인 아이템을 관리할 수 있습니다.", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("당신의 행동을 입력해주세요", ref ConsoleUI.mainView);
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
            ConsoleUI.Instance.DrawTextInBox("0.타이틀로 돌아가기", ref ConsoleUI.mainView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "left", "top");
        }

        public override bool Action(int input)
        {
            switch (input)
            {
                case 0:

                    Game.Instance.PopScene();
                    return false;
                        
                default:
                    if (_inventory.Items == null)
                    {
                        ((Scene)this).InvalidInput();
                    }
                    else
                    {
                        _inventory.EquipItem(input);
                        
                    }
                    return false;
            }
        }

    }
}

