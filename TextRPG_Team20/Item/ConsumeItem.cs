using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20
{
    internal class ConsumeItem : Item, ICommand
    {
        public ConsumeItem()
        {
            
        }    
        public ConsumeItem(ItemData itemData) : base(itemData)
        {

        }
        public void Execute()
        {
            useitem();
            if(--CurrentStack == 0)
            {
                Game.playerInstance.Inventory.RemoveItem(this);
            }
        }

        public virtual void useitem()
        {
        }
    }
}

