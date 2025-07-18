using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.System;

namespace TextRPG_Team20
{
    internal class LowAmorBox : ConsumeItem
    {
        public LowAmorBox(ItemData itemData) : base(itemData)
        {

        }

        public LowAmorBox()
        {

        }
        public override void useitem()
        {
            // Get Weapon Items
            List<Item> items = ItemManager.Instance.FindItems(item => (item.data.ItemEquipType == ItemType.Head ||
            item.data.ItemEquipType == ItemType.Top || item.data.ItemEquipType == ItemType.Bottom));


            string quality = "";

            Random random = new Random();
            int percentage = random.Next(0, 100);

            // 2% Epic
            if (percentage < 2)
            {
                quality = "에픽";
            }
            // 38% Rare
            else if (percentage < 40) 
            {
                quality = "레어";
            }
            else
            {
                quality = "일반";
            }
            // Get Target Quality Items
            var targetItems = items.FindAll(item => item.data.Grade == quality);
            Item item = targetItems[random.Next(targetItems.Count)];

            Game.playerInstance.Inventory.AddItem(item);
        }
    }
}
