using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Item;
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
            List<int> sellItemIds = new List<int> { 998, 999 };

            sellItems = ItemManager.Instance.FindItems(item => sellItemIds.Contains(item.data.ID));
        }

        public void BuyItem()
        {
            // TODO: 아이템 구매 로직 구현
        }

        public void SellItem()
        {
            // TODO: 아이템 판매 로직 구현
        }
    }
}