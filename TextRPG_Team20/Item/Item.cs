using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Item
{
    internal class ItemData
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? FlavorText { get; set; }
        public int Gold { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int HP { get; set; }

        public string Type { get; set; }
        public float GoldUP {  get; set; }

        public string? ClassName { get; set; }   

        public bool isEquipped = false;
        
    }

    internal class Item : IComponent, IComparable<Item>, ICloneable
    {
        public ItemData data = new();

        public Item()
        {
        }

        public virtual object Clone()
        {
            var clone = Activator.CreateInstance(this.GetType()) as Item;
            clone.data = new ItemData
            {
                ID = data.ID,
                Name = data.Name,
                Description = data.Description,
                FlavorText = data.FlavorText,
                Gold = data.Gold,
                Atk = data.Atk,
                Def = data.Def,
                HP = data.HP,
                Type = data.Type,
                isEquipped = data.isEquipped
            };
            return clone;
        }

        public int CompareTo(Item? other)
        {
            if (other != null)
            {
                return data.ID.CompareTo(other.data.ID);
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
