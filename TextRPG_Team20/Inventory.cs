using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Item;
using TextRPG_Team20.Scene;

namespace TextRPG_Team20
{


    internal class Inventory
    {

        public List<Item.Item> Items { get; private set; }

        public Inventory()
        {
            Items = new List<Item.Item>();


        }

        public string showItem(int itemNum)

        {
            if (itemNum < 0 || itemNum >= Items.Count) return "";

            var item = Items[itemNum];

            string equipMark = item.data.isEquipped ? $"{AnsiColor.Green}[E]" : "";
            string whatType = item.data.Type;
            string whatStatString = whatType == "무기" ? "공격력" : "방어력";
            int whatStatInt = whatType == "무기" ? item.data.Atk : item.data.Def;

            // 안전하게 개행 제거
            string safeName = (item.data.Name ?? "").Replace("\n", " ").Replace("\r", " ");
            string safeDesc = (item.data.Description ?? "").Replace("\n", " ").Replace("\r", " ");

            return $"{(itemNum + 1).ToString().PadRight(3)} | " +
           $"{ConsoleUI.PadRightDisplay(equipMark+safeName, 15)} | " +
           $"{ConsoleUI.PadRightDisplay(whatType, 6)} | " +
           $"{whatStatString} + {whatStatInt,-3} | " +
           $"{safeDesc}{AnsiColor.Reset}";
        }

       
        public void EquipItem(int index)
        {
            index -= 1;

            if (Items == null || index < 0 || index >= Items.Count)
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}아이템이 없습니다{AnsiColor.Reset}", ref ConsoleUI.info2View);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
                return;
            }
            var item = Items[index];
            item.data.isEquipped = !item.data.isEquipped;

            string doEquip = item.data.isEquipped ? "장착" : "해제";

            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}{item.data.Name} 을(를) {doEquip}했습니다.!{AnsiColor.Reset}", ref ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");

            if (doEquip == "장착")
            {
                if (item.data.Type == "무기")
                {
                    Game.playerInstance.status.Atk += item.data.Atk;
                    Game.playerInstance.status.ExtraAtk += item.data.Atk;
                }
                else
                {
                    Game.playerInstance.status.Def += item.data.Def;
                    Game.playerInstance.status.ExtraDef += item.data.Def;

                }
            }
            else if (doEquip == "해제")
            {
                if (item.data.Type == "무기")
                {
                    Game.playerInstance.status.Atk -= item.data.Atk;
                    Game.playerInstance.status.ExtraAtk -= item.data.Atk;
                    
                }
                else
                {
                    Game.playerInstance.status.Def -= item.data.Def;
                    Game.playerInstance.status.ExtraDef -= item.data.Def;
                }
            }

        }

        public void useItem(int index)
        {

        }

        public void AddItem(Item.Item item)
        {
            //상점에서 구입하면
            Items.Add(item);
        }
        public void RemoveItem(Item.Item item)
        {
            //상점에 판매하면
            Items.Remove(item);
        }
    }
}

