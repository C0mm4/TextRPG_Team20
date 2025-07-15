using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class EquipScene : IScene
    {
        public bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.SceneChange(Game.SceneState.Inventory);
                    return false;
                       
                default:
                    ((IScene)this).InvalidInput();
                    return true;
            }
        }

        public void PrintScene()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();

            //아이템 목록

            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine(">>");



        }
    }
}
