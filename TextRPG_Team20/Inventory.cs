﻿using System;
using System.Collections.Generic;
using System.Linq;
using TextRPG_Team20.Scene;
using TextRPG_Team20.System;

namespace TextRPG_Team20
{
    internal class Inventory
    {
        public int MaxCapacity { get; private set; } 
        public List<Item> Items { get; private set; } = new List<Item>();

        private Character _ownerCharacter;

        public Inventory(Character owner, int capacity = 25) 
        {
            _ownerCharacter = owner;
            MaxCapacity = capacity;
            Items = new List<Item>();
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
            string jobRestrictInfo = "";

            if (item.data.ItemEquipType != ItemType.Consumable)
            {
                if (item.data.EquipableJobs != null && item.data.EquipableJobs.Count > 0)
                {
                    // 직업 제한이 있는 경우
                    jobRestrictInfo = $" ({string.Join("/", item.data.EquipableJobs.Select(j => j.ToKoreanString()))})";
                }
                else
                {
                    // 직업 제한이 없는 경우 (모든 직업 장착 가능)
                    jobRestrictInfo = " (모든 직업)";
                }
            }


            if (item.data.ItemEquipType == ItemType.Consumable && item.CurrentStack > 1)
            {
                stackInfo = $" (x{item.CurrentStack})";
            }

            switch (item.data.ItemEquipType)
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

            return $"{(itemNum + 1).ToString().PadRight(5)} | " +
            $"{ConsoleUI.PadRightDisplay(equipMark + item.data.Class, 10)} |" +
            $"{ConsoleUI.PadRightDisplay(safeName + stackInfo, 25)} | " +
            $"{ConsoleUI.PadRightDisplay(whatType, 8)} | " +
            (item.data.ItemEquipType != ItemType.Consumable ? $"{ConsoleUI.PadRightDisplay($"{whatStatString} + {whatStatInt,-3}", 25)} | " : "") + 
            
            $"{safeDesc}{AnsiColor.Reset}";
        }

        public void EquipItem(int index)
        {
            index -= 1;

            if (Items == null || index < 0 || index >= Items.Count)
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}아이템이 없습니다.{AnsiColor.Reset}", ref ConsoleUI.logView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.logView, "left", "top");
                return;
            }

            var selectedItem = Items[index];

            if (selectedItem.data.ItemEquipType == ItemType.Consumable)
            {
                if (Items[index] is ConsumeItem)
                {
                    (Items[index] as ConsumeItem).Execute();
                }
                return;
            }

            bool canEquip = false;
            if (selectedItem.data.EquipableJobs == null || selectedItem.data.EquipableJobs.Count == 0 || selectedItem.data.EquipableJobs.Contains(JobType.None))
            {
                
                canEquip = true;
            }
            else if (_ownerCharacter != null && selectedItem.data.EquipableJobs.Contains(_ownerCharacter.Job))
            {
                
                canEquip = true;
            }

            if (!canEquip)
            {
                string allowedJobs = selectedItem.data.EquipableJobs.Count > 0 ? string.Join(", ", selectedItem.data.EquipableJobs) : "정보 없음";
                if (selectedItem.data.EquipableJobs.Contains(JobType.None))
                {
                    allowedJobs = "모든 직업";
                }
                else
                {
                    
                    allowedJobs = string.Join(", ", selectedItem.data.EquipableJobs.Select(j => j.ToKoreanString()));
                }

                ConsoleUI.Instance.DrawTextInBox(
                     $"{AnsiColor.Red}{_ownerCharacter?.Job.ToKoreanString()}은(는) {selectedItem.data.Name}을(를) 장착할 수 없습니다. (가능 직업: {allowedJobs}){AnsiColor.Reset}",
                    ref ConsoleUI.logView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.logView, "left", "top");
                return; 
            }

            if (selectedItem.data.isEquipped)
            {
                selectedItem.data.isEquipped = false;
                RemoveEquipStats(selectedItem);

                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}{selectedItem.data.Name} 을(를) 해제했습니다!{AnsiColor.Reset}", ref ConsoleUI.logView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.logView, "left", "top");
                Items.Sort();
                return;
            }

            Item equippedItemOfSameType = Items.FirstOrDefault(
                i => i.data.isEquipped && i.data.ItemEquipType == selectedItem.data.ItemEquipType
            );

            if (equippedItemOfSameType != null)
            {
                ConsoleUI.Instance.DrawTextInBox(
                    $"{AnsiColor.Yellow}{equippedItemOfSameType.data.Name} 을(를) 해제하고 {selectedItem.data.Name}을(를) 장착합니다.{AnsiColor.Reset}",
                    ref ConsoleUI.logView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.logView, "left", "top");

                equippedItemOfSameType.data.isEquipped = false;
                RemoveEquipStats(equippedItemOfSameType);
            }

            selectedItem.data.isEquipped = true;
            ApplyEquipStats(selectedItem);

            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}{selectedItem.data.Name} 을(를) 장착했습니다!{AnsiColor.Reset}", ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView, "left", "top");


            Items.Sort();
        }


        private void ApplyEquipStats(Item item)
        {
            switch (item.data.ItemEquipType)
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

        private void RemoveEquipStats(Item item)
        {
            switch (item.data.ItemEquipType)
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
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}사용할 아이템이 없습니다.{AnsiColor.Reset}", ref ConsoleUI.logView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
                return;
            }

            var itemToUse = Items[index];

            if (itemToUse.data.ItemEquipType == ItemType.Consumable)
            {
                if (itemToUse.data.ClassName != null && itemToUse.data.ClassName.Contains("Box"))
                {
                    ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}{itemToUse.data.Name} 을(를) 열었습니다!{AnsiColor.Reset}", ref ConsoleUI.logView);
                    // 상자는 스택이 없다고 가정하고 바로 제거
                    RemoveItem(itemToUse);
                }
                else
                {
                    ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}{itemToUse.data.Name} (x1) 을(를) 사용했습니다!{AnsiColor.Reset}", ref ConsoleUI.logView);
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
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}{itemToUse.data.Name} 은(는) 사용할 수 없는 아이템입니다. {AnsiColor.Reset}", ref ConsoleUI.logView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
            }
        }
        public void AddItem(Item newItem)
        {
            // 소모품이고 스택 가능한 아이템인지 확인
            if (newItem.data.ItemEquipType == ItemType.Consumable)
            {
                Item existingStack = Items.FirstOrDefault(i =>
                    i.data.ID == newItem.data.ID && // 같은 종류의 아이템인지 (ID로 비교)
                    i.CurrentStack < 99); // 스택이 가득 차지 않았는지

                if (existingStack != null)
                {
                    existingStack.CurrentStack++;
                    ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Cyan}{newItem.data.Name} 을(를) 획득했습니다!{AnsiColor.Reset}", ref ConsoleUI.logView);
                    ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
                    return;
                }
            }

            if (Items.Count < MaxCapacity)
            {
                Items.Add(newItem);
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Cyan}{newItem.data.Name} 을(를) 획득했습니다!{AnsiColor.Reset}", ref ConsoleUI.logView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
            }
            else
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}인벤토리가 가득 찼습니다!{AnsiColor.Reset}", ref ConsoleUI.logView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View, "left", "top");
            }

            Items.Sort();
        }
        public void RemoveStack(Item itemToReduce)
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
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
            Items.Sort();
        }
    
        }
    }
