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
        

        public string showItem()
        {
            if (Items == null || Items.Count == 0)
            {
                return "아이템이 없습니다";
            }
            else
            {
                //아이템이 있으면
                //장착했으면 / 안했으면  - 행동시 해제 / 장착
                //공격력이면 / 방어력이면 - 공격력 + / 방어력 +


                var sb = new StringBuilder();

                for (int i = 0; i < Items.Count; i++)
                {
                    var item = Items[i];
                    string equipMark = item.data.isEquipped ? "[E]" : "";
                    string whatType = item.data.Type == 0 ? "무기" : "방어구";
                    string whatStatString = whatType == "무기" ? "공격력" : "방어력";
                    int whatStatInt = whatType == "무기" ? item.data.Atk : item.data.Def;

                    sb.AppendLine($"{i + 1}. {equipMark}{item.data.Name} | {whatType} | {whatStatString} {whatStatInt} | {item.data.Description}");
                   
                }

                return sb.ToString();
                          
            }

        }

        public void EquipItem(int index)
        {
            index -= 1;

            if (Items == null || index < 0 || index >= Items.Count)
            {
                ConsoleUI.Instance.DrawTextInBox("아이템이 없습니다", ref ConsoleUI.inputView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView, "left", "top");
                return;
            }
            var item = Items[index];
            item.data.isEquipped = !item.data.isEquipped;

            string doEquip = item.data.isEquipped ? "장착" : "해제";

            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}{item.data.Name} 을(를) {doEquip}했습니다.!{AnsiColor.Reset}", ref ConsoleUI.inputView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView, "left", "top");
            

            //foreach (var inventoryitem in Items)
            //{

            //    //string equipmark = item.data.isEquipped ? "[E]" : "";
            //    //if (item.data.Atk > 0)
            //    //    Console.WriteLine($" - {equipmark}{item.data.Name} | {item.data.Type} +{item.data.Atk} | {item.data.Description}");
            //    //else if (item.data.Def > 0)
            //    //    Console.WriteLine($" - {equipmark}{item.data.Name} | {item.data.Type} +{item.data.Def} | {item.data.Description}");
            //}

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

