﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.System;

namespace TextRPG_Team20
{
    internal class UltraArmorBox : ConsumeItem
    {
        public UltraArmorBox(ItemData itemData) : base(itemData)
        {

        }

        public UltraArmorBox()
        {

        }
        public override void useitem()
        {
            // Get Weapon Items
            List<Item> items = ItemManager.Instance.FindItems(item => (item.data.ItemEquipType == ItemType.Head ||
            item.data.ItemEquipType == ItemType.Top || item.data.ItemEquipType == ItemType.Bottom));


            string quality = "레전더리";

            Random random = Game.random;

            // Get Target Quality Items
            var targetItems = items.FindAll(item => item.data.Grade == quality);
            Item item = targetItems[random.Next(targetItems.Count)];

            Game.playerInstance.Inventory.AddItem(item);
        }
    }
}
