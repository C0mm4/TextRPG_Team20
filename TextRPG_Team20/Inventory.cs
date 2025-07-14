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
     

        public Inventory()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            Items = new List<Item.Item>();
        }

        public void EquipItem(Item.Item item)
        {
            bool hasItem = false;
            bool isequipped = false;

            if (Items[0] == null)
            {
                Console.WriteLine("장착할 아이템이 없습니다.");
                return;
            }

                     
            item.data.isEquipped = true;

            
            foreach (var it in Items)
            {
                string equipMark = item.data.isEquipped ? "[e]" : "";
                if (item.data.Atk > 0)
                Console.WriteLine($" - {equipMark}{item.data.Name} | {item.data.Type} +{item.data.Atk} | {item.data.Description}");
                else if(item.data.Def >0)
                Console.WriteLine($" - {equipMark}{item.data.Name} | {item.data.Type} +{item.data.Def} | {item.data.Description}");
            }

            Console.WriteLine($"{item.data.Name} 을(를) 장착했습니다.");
        
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

