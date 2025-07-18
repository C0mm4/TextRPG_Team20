﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.System;

namespace TextRPG_Team20
{
    internal class MiddleWeaponBox : ConsumeItem
    {
        public MiddleWeaponBox(ItemData itemData) : base(itemData)
        {

        }

        public MiddleWeaponBox()
        {

        }
        public override void useitem()
        {
            // Get Weapon Items
            List<Item> items = ItemManager.Instance.FindItems(item => item.data.ItemEquipType == ItemType.Weapon);

            string quality = "";

            Random random = Game.random;
            int percentage = random.Next(0, 1000);

            if (percentage < 100)
            {
                quality = "유니크";
            }
            else if (percentage < 350)
            {
                quality = "에픽";
            }
            else if (percentage < 650)
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
