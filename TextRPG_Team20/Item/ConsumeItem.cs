using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Item
{
    internal class ConsumeItem : Item, ICommand
    {

        public void Execute()
        {
            useitem();
        }

        public virtual void useitem()
        {
            //아이템의 종류를 받아온다(체력 포션인지, 큰 포션인지 작은 포션인지)
            int ItemType = 0;/*아이템 종류*/
            int ItemBig = 0; //아이템의 크기
            //아이템 갯수 변수를 받아온다
            int ItemCount = 0/*받아온 아이템 갯수*/;
            ItemCount = ItemCount--; //아이템 갯수를 하나 뺀다
            int PlayerNowHp = 0;//현재 체력
            int PlayerMaxHp = 0;//최대 체력

            //아이템의 속성에 따라 회복한다
            if (ItemType == 0)//0이 체력 회복 아이템이라 가정
            {
                PlayerNowHp = PlayerNowHp + ItemBig;
                if (PlayerNowHp >= PlayerMaxHp)
                    PlayerNowHp = PlayerMaxHp; //최대 체력보다 많이 회복되면 현재체력을 최대체력으로 변경
            }
        }
    }
}

