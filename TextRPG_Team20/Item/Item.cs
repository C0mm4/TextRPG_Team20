using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Item
{
    public enum ItemType
    {
        Weapon = 0,     // 무기
        Head = 1,       // 모자
        Top = 2,        // 상의
        Bottom = 3,     // 하의
        Accessory = 4,  // 장신구
        Consumable = 5  // 소모품 (상자 포함)
    }
    internal class ItemData
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? FlavorText { get; set; }
        public int Gold { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int HP { get; set; }

        public ItemType Type { get; set; }

        public int MaxStackSize { get; set; } = 1;

        public string? ClassName { get; set; }   

        public bool isEquipped = false;
        
    }

    internal class Item : IComponent, IComparable<Item>, ICloneable
    {
        public ItemData data = new();

        public int CurrentStack { get; set; } = 1;

        public Item(ItemData itemData)
        {
            this.data = itemData;

            if (this.data.Type == ItemType.Consumable && this.data.MaxStackSize > 1)
            {
                CurrentStack = 1;
            }
            else // 그 외의 아이템 (장비 등) 또는 스택 불가능한 소모품
            {
                CurrentStack = 1; // 스택 불가능 아이템도 일단 1개로 시작
            }

            ConsoleUI.Instance.DrawTextInBox($"{GetType().Name}", ref ConsoleUI.logView);
        }

        public virtual object Clone()
        {
            var clone = Activator.CreateInstance(this.GetType()) as Item;
            clone.data = new ItemData
            {
                ID = data.ID,
                Name = data.Name,
                Description = data.Description,
                FlavorText = data.FlavorText,
                Gold = data.Gold,
                Atk = data.Atk,
                Def = data.Def,
                HP = data.HP,
                Type = data.Type,
                MaxStackSize = data.MaxStackSize,
                ClassName = data.ClassName,
                isEquipped = false
            };

            clone.CurrentStack = 1;

            return clone;
        }

        public int CompareTo(Item? other)
        {
            if (other != null)
            {
                return data.ID.CompareTo(other.data.ID);
            }
            return 0;
        }

        public void PrintData()
        {

        }

        public void PrintInField()
        {

        }

    }

    internal class ItemWrapper()
    {
        public required List<ItemData> Items { get; set; }
    }
}
