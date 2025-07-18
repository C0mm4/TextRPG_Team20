using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Scene;
using TextRPG_Team20.System;

namespace TextRPG_Team20
{
    internal class ShopManager
    {
        private static ShopManager? _instance;
        public static ShopManager Instance 
        {
            get
            {
                if(_instance == null)
                    _instance = new ShopManager();
                return _instance;
            } 
        }

        public List<Item> sellItems = [];
        public int[] itemCnt = [];

        private ShopManager()
        {
            SetSellItems();
        }

        private void SetSellItems()
        {
            /*List<int> sellItemIds = new List<int> { 300, 301, 302 };

            sellItems = ItemManager.Instance.FindItems(item => sellItemIds.Contains(item.data.ID));
            */
            sellItems = ItemManager.Instance.FindItems(item => item.data.ItemEquipType == ItemType.Consumable);
        }

        public (bool, string) BuyItem(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= sellItems.Count)
            {
                return (false, "잘못된 입력입니다.");
            }

            var itemToBuy = sellItems[itemIndex];
            var player = Game.playerInstance;

            if (player.status.Gold < itemToBuy.data.Gold)
            {
                return (false, $"{AnsiColor.Red}골드가 부족합니다.{AnsiColor.Reset}");
            }

            // 스택 가능한 아이템이 이미 인벤토리에 있는지 확인
            bool canStack = itemToBuy.data.ItemEquipType == ItemType.Consumable ? true : false;

            // 인벤토리 공간 확인
            if (player.Inventory.Items.Count >= player.Inventory.MaxCapacity)
            {
                canStack = player.Inventory.Items.Any(i =>
                i.data.ID == itemToBuy.data.ID &&
                i.data.MaxStackSize > 1 &&
                i.CurrentStack < i.data.MaxStackSize
            );
                if (!canStack)
                {
                    return (false, $"{AnsiColor.Red}인벤토리가 가득 찼습니다.{AnsiColor.Reset}");
                }
            }

            if (canStack)
            {
                ConsoleUI.Instance.DrawTextInBox("구매 할 수량을 입력해주세요 >> ", ref ConsoleUI.inputView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);

                var n = ConsoleUI.Read(ref ConsoleUI.inputView);

                bool canParse = int.TryParse(n, out int value);
                if (canParse)
                {
                    if(value == 0)
                    {
                        return (false, "");
                    }
                    if(player.status.Gold >= value * itemToBuy.data.Gold)
                    {
                        player.DecreaseGold(itemToBuy.data.Gold * value);
                        for (int i = 0; i < value; i++)
                        {
                            player.Inventory.AddItem((Item)itemToBuy.Clone());
                        }

                        string ret = $"{AnsiColor.Cyan}{itemToBuy.data.Name}{AnsiColor.Reset}을(를) {value}개 구매했습니다.";
                        return (true, ret);
                    }
                    else
                    {
                        return (false, $"{AnsiColor.Red}골드가 부족합니다.{AnsiColor.Reset}");
                    }
                }
                else
                {
                    return (false, $"{AnsiColor.Red}잘못된 입력입니다.{AnsiColor.Reset}");
                }
            }

            // 플레이어 골드 차감
            player.DecreaseGold(itemToBuy.data.Gold);

            // 인벤토리에 아이템 추가
            player.Inventory.AddItem((Item)itemToBuy.Clone());

            // 성공 메시지 출력
            string message = $"{AnsiColor.Cyan}{itemToBuy.data.Name}{AnsiColor.Reset}을(를) 구매했습니다.";
            return (true, message);

        }

        // 아이템 판매 기능
        public (bool, string) SellItem(int itemIndex)
        {
            var inventory = Game.playerInstance.Inventory;
            if (itemIndex < 0 || itemIndex >= inventory.Items.Count)
            {
                return (false, "잘못된 입력입니다.");
            }

            var itemToSell = inventory.Items[itemIndex];

            if(itemToSell.data.ItemEquipType == ItemType.Consumable)
            {
                ConsoleUI.Instance.DrawTextInBox("판매 할 수량을 입력해주세요 >> ", ref ConsoleUI.inputView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);

                var n = ConsoleUI.Read(ref ConsoleUI.inputView);

                bool canParse = int.TryParse(n, out int value);
                if (canParse)
                {
                    if(itemToSell.CurrentStack < value)
                    {
                        return (false, "잘못된 입력입니다.");
                    }
                    if(value == 0)
                    {
                        return (false, "");
                    }
                    string ret = $"{AnsiColor.Cyan}{itemToSell.data.Name}{AnsiColor.Reset}을(를) {value}개 판매했습니다.";
                    for(int i = 0; i < value; i++)
                    {
                        int sell = itemToSell.GetSellPrice();
                        Game.playerInstance.AddGold(sell);
                        inventory.RemoveStack(itemToSell);
                    }
                    return (true, ret);
                }
                else
                {
                    return (false, $"{AnsiColor.Red}골드가 부족합니다.{AnsiColor.Reset}");
                }
            }
            

            // 장착 중인 아이템 판매 불가
            if (itemToSell.data.isEquipped)
            {
                return (false, $"{AnsiColor.Red}장착 중인 아이템은 판매할 수 없습니다.{AnsiColor.Reset}");
            }
            int sellPrice = itemToSell.GetSellPrice();

            // 플레이어 골드 추가
            Game.playerInstance.AddGold(sellPrice);

            // 인벤토리 아이템 제거
            if (itemToSell.data.ItemEquipType == ItemType.Consumable)
            {
                inventory.RemoveStack(itemToSell);
            }
            else
            {
                inventory.RemoveItem(itemToSell);
            }

            // 성공 메시지 출력
            string message = $"{AnsiColor.Cyan}{itemToSell.data.Name}{AnsiColor.Reset}을(를) 판매하여 {AnsiColor.Yellow}{sellPrice} G{AnsiColor.Reset}를 얻었습니다.";
            return(true, message);
        }
    }
}