using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Item;
using TextRPG_Team20.Scene;

namespace TextRPG_Team20
{


    internal class Inventory
    {
                    
        public List<Item.Item> Items { get; private set; }

        public void showItem()
        {
            if (Items == null || Items.Count == 0)
            {
                Console.WriteLine("아이템이 없습니다.");
                return;
            }
            else
            {
                foreach (var item in Items)
                {
                    Console.WriteLine($" - {item.data.Name} | {item.data.Type} +{(item.data.Type == 0 ? item.data.Atk : item.data.Def)} | {item.data.Description}");
                }
            }

        }

        public void EquipItem(Item.Item item)
        {

            if (Items == null || Items.Count == 0)
            {
                Console.WriteLine("장착할 아이템이 없습니다.");
                return;
            }
            else
            {

                item.data.isEquipped = true;

                foreach (var inventoryItem in Items)
                {
                    string equipMark = item.data.isEquipped ? "[E]" : "";
                    if (item.data.Atk > 0)
                        Console.WriteLine($" - {equipMark}{item.data.Name} | {item.data.Type} +{item.data.Atk} | {item.data.Description}");
                    else if (item.data.Def > 0)
                        Console.WriteLine($" - {equipMark}{item.data.Name} | {item.data.Type} +{item.data.Def} | {item.data.Description}");
                }

                Console.WriteLine($"{item.data.Name} 을(를) 장착했습니다.");

            }

                       
        }
        public void UnequipItem(Item.Item item)
        {
            if (item == null)
            {
                Console.WriteLine("해제할 아이템이 없습니다.");
                return;
            }
            item.data.isEquipped = false;
            Console.WriteLine($"{item.data.Name} 을(를) 해제했습니다.");
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

