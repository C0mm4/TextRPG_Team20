using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.System;

namespace TextRPG_Team20
{
    internal class potion : ConsumeItem
    {
        public potion(ItemData itemData) : base(itemData)
        {

        }

        public potion()
        {

        }
        public override void useitem()
        {
            // Get Weapon Items
            List<Item> items = ItemManager.Instance.FindItems(item => item.data.ItemEquipType == ItemType.Consumable);

            

            string quality = "";
            Random random = new Random();

            // Get Target Quality Items
            var targetItems = items.FindAll(item => item.data.Grade == quality);
            Item item = targetItems[random.Next(targetItems.Count)];

            Game.playerInstance.Inventory.AddItem(item);
        }
    }
}
