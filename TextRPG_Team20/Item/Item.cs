using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20
{
    public enum JobType
    {
        None = 0,     // 기본값 또는 해당 없음 , 공용 아이템
        Warrior = 1,  
        Mage = 2,     
        Archer = 3   
    }
    public enum ItemType
    {
        Weapon = 0,     // 무기
        Head = 1,       // 모자
        Top = 2,        // 상의
        Bottom = 3,     // 하의
        Accessory = 4,  // 장신구
        Consumable = 5,  // 소모품 (상자 포함)
        None = 999
    }
    internal class ItemData
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Gold { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int HP { get; set; }

        public ItemType ItemEquipType { get; set; }
        public string? ItemType {  get; set; }
        public string? Grade { get; set; }
        public string? Class { get; set; }
        public int MaxStackSize { get; set; } = 1;
        public float GoldUP {  get; set; }

        public string? ClassName { get; set; }
        public List<JobType> EquipableJobs { get; set; } = new List<JobType>();

        public bool isEquipped = false;

        

        public ItemData() { }
        public ItemData(ItemData data)
        {
            ID = data.ID;
            Name = data.Name;
            Description = data.Description;
            Gold = data.Gold;
            Atk = data.Atk;
            Def = data.Def;
            HP = data.HP;
            ItemEquipType = data.ItemEquipType;
            ItemType = data.ItemType;
            Grade = data.Grade;
            Class = data.Class;
            MaxStackSize = data.MaxStackSize;
            GoldUP = data.GoldUP;
            ClassName = data.ClassName;

            EquipableJobs = new List<JobType>(data.EquipableJobs);

            isEquipped = data.isEquipped;
        }
    }

    internal class Item : IComponent, IComparable<Item>, ICloneable
    {
        public ItemData data = new();

        public Item()
        {
        }

        public void SetType()
        {
            switch (data.ItemType)
            {
                case "무기":
                    data.ItemEquipType = ItemType.Weapon;
                    break;

                case "모자":
                    data.ItemEquipType = ItemType.Head;
                    break;

                case "상의":
                    data.ItemEquipType = ItemType.Top;
                    break;

                case "하의":
                    data.ItemEquipType = ItemType.Bottom;
                    break;

                case "악세서리":
                    data.ItemEquipType = ItemType.Accessory;
                    break;

                case "소모품":
                    data.ItemEquipType = ItemType.Consumable;
                    break;
            }
        }

        public int CurrentStack { get; set; } = 1;

        public Item(ItemData itemData)
        {
            data = new ItemData(itemData);

            if (this.data.ItemEquipType == ItemType.Consumable)
            {
                data.MaxStackSize = 99;
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
            if (clone != null)
            {
                
                clone.data = new ItemData(this.data); 

                // 클론된 아이템은 장착되지 않은 상태로 시작해야 하므로 isEquipped를 false로 명시
                clone.data.isEquipped = false;

                
                clone.CurrentStack = 1;
            }

            

            return clone;
        }

        public int CompareTo(Item? other)
        {
            if (other != null)
            {
                // 장착 아이템 우선 정렬 (내림차순 앞번으로)
                if (data.isEquipped)
                {
                    if (other.data.isEquipped)
                    {
                        return data.ID.CompareTo(other.data.ID);
                    }
                    return -1;
                }
                if (other.data.isEquipped)
                {
                    return 1;
                }
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

        public int GetSellPrice()  
        {
            return (int)(data.Gold * 1);    // 판매 가격 설정 일단은 1로 해둠
        }

    }

    internal class ItemWrapper()
    {
        public required List<ItemData> Items { get; set; }
    }
}
