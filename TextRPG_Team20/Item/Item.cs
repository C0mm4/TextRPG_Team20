using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Item
{
    internal class ItemData
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string FlavorText { get; set; }
        public int Gold { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int HP { get; set; }

        public int Type { get; set; }


        public bool isEquipped { get; set; }

    }

    internal abstract class Item : IComponent, IComparable<Item>
    {

        public ItemData data;
        
        public int CompareTo(Item? other)
        {
            if (other != null)
            {
                return data.Id.CompareTo(other.data.Id);
            }
            return 0;
        }

        public void PrintData()
        {

        }

        public void PrintInField()
        {

        }

    }

    internal class ItemWrapper()
    {
        public required List<ItemData> Items { get; set; }
    }
}
