using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.System;

namespace TextRPG_Team20
{
    internal class HighWeaponBox : ConsumeItem
    {
        public HighWeaponBox(ItemData itemData) : base(itemData)
        {

        }

        public HighWeaponBox()
        {

        }
        public override void useitem()
        {
            // Get Weapon Items
            List<Item> items = ItemManager.Instance.FindItems(item => item.data.ItemEquipType == ItemType.Weapon);

            string quality = "";

            Random random = Game.random;
            int percentage = random.Next(0, 100);

            if (percentage < 15)
            {
                quality = "레전더리";
            }
            else if (percentage < 200)
            {
                quality = "유니크";
            }
            else if (percentage < 450)
            {
                quality = "에픽";
            }
            else if (percentage < 700)
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
