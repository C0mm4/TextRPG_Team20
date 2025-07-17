using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Item;
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

        public List<Item.Item> sellItems = new();

        private ShopManager()
        {
            SetSellItems();
        }

        private void SetSellItems()
        {
            List<int> sellItemIds = new List<int> { 1, 2, 3 };

            sellItems = ItemManager.Instance.FindItems(item => sellItemIds.Contains(item.data.ID));
            //sellItems = ItemManager.Instance.FindItems(item => item.data.ItemEquipType == Item.ItemType.Consumable);
        }

        public void BuyItem()
        {
            // TODO: 아이템 구매 로직 구현
        }

        public (bool, string) SellItem(int itemIndex)
        {
            var inventory = Game.playerInstance.Inventory;
            if (itemIndex < 0 || itemIndex >= inventory.Items.Count)
            {
                return (false, "없는 아이템 입니다.");
            }

            var itemToSell = inventory.Items[itemIndex];

            // 장착 중인 아이템 팬매 불가
            if (itemToSell.data.isEquipped)
            {
                return (false, $"{AnsiColor.Red}장착 중인 아이템은 판매할 수 없습니다.{AnsiColor.Reset}");
            }
            int sellPrice = itemToSell.GetSellPrice();

            // 플레이어 골드 추가
            Game.playerInstance.AddGold(sellPrice);
            // 인벤토리 아이템 제거
            inventory.RemoveItem(itemToSell);
            // 성공 메시지 출력
            string message = $"{AnsiColor.Cyan}{itemToSell.data.Name}{AnsiColor.Reset}을(를) 판매하여 {AnsiColor.Yellow}{sellPrice} G{AnsiColor.Reset}를 얻었습니다.";
            return(true, message);
        }
    }
}