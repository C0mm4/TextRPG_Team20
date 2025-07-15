using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TextRPG_Team20.Item;

namespace TextRPG_Team20.System
{
    internal class ItemManager
    {
        private static ItemManager? _instance;
        public static ItemManager Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new ItemManager();
                return _instance;
            }
        }

        List<Item.Item> itemOrigins = new();

        private ItemManager() 
        {
            var itemDataStr = JsonLoader.LoadJson("ItemData.json");
            try
            {
                ItemWrapper? wrapper = JsonSerializer.Deserialize<ItemWrapper>(itemDataStr);
                ItemData[]? datas;
                if (wrapper != null)
                {
                    datas = wrapper.Items.ToArray();

                    if (datas != null)
                    {
                        foreach (var data in datas) 
                        {
                            Item.Item item = new Item.Item();
                            item.data = data;
                            Register(item);
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                ConsoleUI.Instance.DrawTextInBox("JSON 파일 파싱 중 오류 발생: " + ex.Message, ref ConsoleUI.logView);
            }


        }

        private readonly Dictionary<int, Item.Item> _prototypes = new();

        public void Register(Item.Item item)
        {
            _prototypes[item.data.Id] = item;
        }

        public T Create<T>(int id) where T : Item.Item
        {
            return (T)_prototypes[id].Clone();
        }
    }
}
