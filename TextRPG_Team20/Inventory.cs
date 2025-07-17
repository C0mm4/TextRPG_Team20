using System;
using System.Collections.Generic;
using System.Linq;
using TextRPG_Team20.Item;
using TextRPG_Team20.Scene; 

namespace TextRPG_Team20
{
    internal class Inventory
    {
        public List<Item.Item> Items { get; private set; }
        public int MaxCapacity { get; private set; } 

        public Inventory(int capacity = 50) 
        {
            Items = new List<Item.Item>();
            MaxCapacity = capacity;
        }

        public string showItem(int itemNum)
        {
            if (itemNum < 0 || itemNum >= Items.Count) return "";

            var item = Items[itemNum];

            string equipMark = item.data.isEquipped ? $"{AnsiColor.Green}[E]" : "";
            string whatType = "";
            string whatStatString = "";
            int whatStatInt = 0;
            string stackInfo = ""; 


            if (item.data.ItemType == ItemType.Consumable && item.CurrentStack > 1)
            {
                stackInfo = $" (x{item.CurrentStack})";
            }

            switch (item.data.ItemType)
            {
                case ItemType.Weapon:
                    whatType = "무기";
                    whatStatString = "공격력";
                    whatStatInt = item.data.Atk;
                    break;
                case ItemType.Head:
                    whatType = "모자";
                    whatStatString = "방어력/체력";
                    whatStatInt = item.data.Def + item.data.HP;
                    break;
                case ItemType.Top:
                    whatType = "상의";
                    whatStatString = "방어력/체력";
                    whatStatInt = item.data.Def + item.data.HP;
                    break;
                case ItemType.Bottom:
                    whatType = "하의";
                    whatStatString = "방어력/체력";
                    whatStatInt = item.data.Def + item.data.HP;
                    break;
                case ItemType.Accessory:
                    whatType = "장신구";
                    whatStatString = $"공격력/방어력/체력";
                    whatStatInt = (item.data.Atk > 0 ? item.data.Atk : 0) +
                                  (item.data.Def > 0 ? item.data.Def : 0) +
                                  (item.data.HP > 0 ? item.data.HP : 0);
                    break;
                case ItemType.Consumable:
                    if (item.data.ClassName != null && item.data.ClassName.Contains("Box"))
                    {
                        whatType = "상자";
                        whatStatString = "내용물";
                        whatStatInt = 0; 
                    }
                    else
                    {
                        whatType = "소모품";
                        whatStatString = "회복량";
                        whatStatInt = item.data.HP;
                    }
                    break;
                default:
                    whatType = "알 수 없음";
                    whatStatString = "정보";
                    whatStatInt = 0;
                    break;
            }

            string safeName = (item.data.Name ?? "").Replace("\n", " ").Replace("\r", " ");
            string safeDesc = (item.data.Description ?? "").Replace("\n", " ").Replace("\r", " ");

            return $"{(itemNum + 1).ToString().PadRight(3)} | " +
            $"{ConsoleUI.PadRightDisplay(equipMark + safeName + stackInfo, 15)} | " + 
            $"{ConsoleUI.PadRightDisplay(whatType, 6)} | " +
            $"{whatStatString} + {whatStatInt,-3} | " +
            $"{safeDesc}{AnsiColor.Reset}";
        }

        public void EquipItem(int index)
        {
            index -= 1;

            if (Items == null || index < 0 || index >= Items.Count)
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}아이템이 없습니다.{AnsiColor.Reset}", ref ConsoleUI.info2View);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
                return;
            }

            var selectedItem = Items[index];

            if (selectedItem.data.ItemType == ItemType.Consumable)
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}{selectedItem.data.Name} 은(는) 장착할 수 없는 아이템입니다.{AnsiColor.Reset}", ref ConsoleUI.info2View);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
                return;
            }

            if (selectedItem.data.isEquipped)
            {
                selectedItem.data.isEquipped = false;
                RemoveEquipStats(selectedItem);

                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}{selectedItem.data.Name} 을(를) 해제했습니다!{AnsiColor.Reset}", ref ConsoleUI.info2View);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
                return;
            }

            Item.Item equippedItemOfSameType = Items.FirstOrDefault(
                i => i.data.isEquipped && i.data.ItemType == selectedItem.data.ItemType
            );

            if (equippedItemOfSameType != null)
            {
                ConsoleUI.Instance.DrawTextInBox(
                    $"{AnsiColor.Yellow}{equippedItemOfSameType.data.Name} 을(를) 해제하고 {selectedItem.data.Name}을(를) 장착합니다.{AnsiColor.Reset}",
                    ref ConsoleUI.info2View);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");

                equippedItemOfSameType.data.isEquipped = false;
                RemoveEquipStats(equippedItemOfSameType);
            }

            selectedItem.data.isEquipped = true;
            ApplyEquipStats(selectedItem);

            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}{selectedItem.data.Name} 을(를) 장착했습니다!{AnsiColor.Reset}", ref ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
        }


        private void ApplyEquipStats(Item.Item item)
        {
            switch (item.data.ItemType)
            {
                case ItemType.Weapon:
                    Game.playerInstance.status.Atk += item.data.Atk;
                    Game.playerInstance.status.ExtraAtk += item.data.Atk;
                    break;
                case ItemType.Head:
                case ItemType.Top:
                case ItemType.Bottom:

                    Game.playerInstance.status.Def += item.data.Def;
                    Game.playerInstance.status.ExtraDef += item.data.Def;

                    if (item.data.HP != 0)
                    {
                        Game.playerInstance.status.MaxHP += item.data.HP;
                        Game.playerInstance.status.HP += item.data.HP;
                    }
                    break;
                case ItemType.Accessory:
                    if (item.data.Atk != 0)
                    {
                        Game.playerInstance.status.Atk += item.data.Atk;
                        Game.playerInstance.status.ExtraAtk += item.data.Atk;
                    }
                    if (item.data.Def != 0)
                    {
                        Game.playerInstance.status.Def += item.data.Def;
                        Game.playerInstance.status.ExtraDef += item.data.Def;
                    }
                    if (item.data.HP != 0)
                    {
                        Game.playerInstance.status.MaxHP += item.data.HP;
                        Game.playerInstance.status.HP += item.data.HP;
                    }
                    break;
            }
        }

        private void RemoveEquipStats(Item.Item item)
        {
            switch (item.data.ItemType)
            {
                case ItemType.Weapon:
                    Game.playerInstance.status.Atk -= item.data.Atk;
                    Game.playerInstance.status.ExtraAtk -= item.data.Atk;
                    break;
                case ItemType.Head:
                case ItemType.Top:
                case ItemType.Bottom:

                    Game.playerInstance.status.Def -= item.data.Def;
                    Game.playerInstance.status.ExtraDef -= item.data.Def;

                    if (item.data.HP != 0)
                    {
                        Game.playerInstance.status.MaxHP -= item.data.HP;

                        if (Game.playerInstance.status.HP > Game.playerInstance.status.MaxHP)
                        {
                            Game.playerInstance.status.HP = Game.playerInstance.status.MaxHP;
                        }
                    }
                    break;
                case ItemType.Accessory:
                    if (item.data.Atk != 0)
                    {
                        Game.playerInstance.status.Atk -= item.data.Atk;
                        Game.playerInstance.status.ExtraAtk -= item.data.Atk;
                    }
                    if (item.data.Def != 0)
                    {
                        Game.playerInstance.status.Def -= item.data.Def;
                        Game.playerInstance.status.ExtraDef -= item.data.Def;
                    }
                    if (item.data.HP != 0)
                    {
                        Game.playerInstance.status.MaxHP -= item.data.HP;
                        if (Game.playerInstance.status.HP > Game.playerInstance.status.MaxHP)
                        {
                            Game.playerInstance.status.HP = Game.playerInstance.status.MaxHP;
                        }
                    }
                    break;
            }
        }
        public void useItem(int index)
        {
            index -= 1;

            if (Items == null || index < 0 || index >= Items.Count)
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}사용할 아이템이 없습니다.{AnsiColor.Reset}", ref ConsoleUI.info2View);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
                return;
            }

            var itemToUse = Items[index];

            if (itemToUse.data.ItemType == ItemType.Consumable)
            {
                if (itemToUse.data.ClassName != null && itemToUse.data.ClassName.Contains("Box"))
                {
                    ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}{itemToUse.data.Name} 을(를) 열었습니다!{AnsiColor.Reset}", ref ConsoleUI.info2View);
                    // 상자는 스택이 없다고 가정하고 바로 제거
                    RemoveItem(itemToUse);
                }
                else 
                {
                    ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}{itemToUse.data.Name} (x1) 을(를) 사용했습니다!{AnsiColor.Reset}", ref ConsoleUI.info2View);
                    Game.playerInstance.status.HP += itemToUse.data.HP;
                    if (Game.playerInstance.status.HP > Game.playerInstance.status.MaxHP)
                    {
                        Game.playerInstance.status.HP = Game.playerInstance.status.MaxHP;
                    }
                    RemoveStack(itemToUse);
                }
                ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
            }
            else
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}{itemToUse.data.Name} 은(는) 사용할 수 없는 아이템입니다. {AnsiColor.Reset}", ref ConsoleUI.info2View);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
            }
        }
        public void AddItem(Item.Item newItem)
        {
            // 소모품이고 스택 가능한 아이템인지 확인
            if (newItem.data.ItemType == ItemType.Consumable && newItem.data.MaxStackSize > 1)
            {
                Item.Item existingStack = Items.FirstOrDefault(i =>
                    i.data.ID == newItem.data.ID && // 같은 종류의 아이템인지 (ID로 비교)
                    i.CurrentStack < i.data.MaxStackSize); // 스택이 가득 차지 않았는지

                if (existingStack != null)
                { 
                    existingStack.CurrentStack++; 
                    ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Cyan}{newItem.data.Name} (x1) 을(를) 획득하여 기존 스택에 추가했습니다! (총 {existingStack.CurrentStack}개){AnsiColor.Reset}", ref ConsoleUI.info2View);
                    ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
                    return; 
                }
            }

            if (Items.Count < MaxCapacity)
            {
                Items.Add(newItem);
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Cyan}{newItem.data.Name} 을(를) 획득했습니다!{AnsiColor.Reset}", ref ConsoleUI.info2View);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
            }
            else
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}인벤토리가 가득 찼습니다!{AnsiColor.Reset}", ref ConsoleUI.info2View);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
            }
        }
        private void RemoveStack(Item.Item itemToReduce)
        {
            if (itemToReduce == null) return;

            if (itemToReduce.CurrentStack > 1)
            {
                itemToReduce.CurrentStack--;
            }
            else // 스택이 1개 남았거나 스택 불가능한 아이템이면 완전히 제거
            {
                Items.Remove(itemToReduce);
            }
        }
        public void RemoveItem(Item.Item item)
        {
            Items.Remove(item);
        }
    }
}