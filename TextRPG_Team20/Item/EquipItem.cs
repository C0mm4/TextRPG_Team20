using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Item
{
    internal class EquipItem : Item
    {

        public void equip()
        {
            //플레이어 정보와 아이템 데이터를 받아와서 때(공격,방어,최대체력)
            int PlayerAtk = 0;
            int PlayerDef = 0;
            int PlayerMaxHp = 0;

            int ItemAtk = 0;

            int ItemDef = 0;
            int ItemHp = 0;
            //장비의 데이터를 더해줌

            PlayerAtk = PlayerAtk + ItemAtk;
            PlayerDef = PlayerDef + ItemDef;
            PlayerMaxHp = PlayerMaxHp + ItemHp;
        }

        public void unequip()
        {
            //플레이어 정보와 아이템 데이터를 받아와서 때(공격,방어,최대체력)
            int PlayerAtk = 0;
            int PlayerDef = 0;
            int PlayerMaxHp = 0;

            int ItemAtk = 0;
            int ItemDef = 0;
            int ItemHp = 0;
            //장비의 데이터를 빼준다

            PlayerAtk = PlayerAtk - ItemAtk;
            PlayerDef = PlayerDef - ItemDef;
            PlayerMaxHp = PlayerMaxHp - ItemHp;
        }

    }
}
