using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<Item.Item> sellItems = [];
        public int[] itemCnt = [];

        private ShopManager()
        {
            SetSellItems();
        }

        private void SetSellItems()
        {
            sellItems = ItemManager.Instance.FindItems(item => item.data.Type == Item.ItemType.Consumable);
        }

        public void BuyItem()
        {

        }

        public void SellItem()
        {

        }
    }
}
