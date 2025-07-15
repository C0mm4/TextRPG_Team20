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
                // Load Json Data
                ItemWrapper? wrapper = JsonSerializer.Deserialize<ItemWrapper>(itemDataStr);
                if (wrapper != null)
                {
                    foreach (var data in wrapper.Items) 
                    {
                        string? className = data.ClassName;
                        // Get Application Domain Assemblies
                        Type? itemType = AppDomain.CurrentDomain
                                            .GetAssemblies()
                                            .SelectMany(a => a.GetTypes())
                                            .FirstOrDefault(t => t.Name == className && typeof(Item.Item).IsAssignableFrom(t));
                        if (itemType != null)
                        {
<<<<<<< Updated upstream
                            Item.Item item = new Item.Item();
                            item.data = data;
                            Register(item);
=======
                            // Create itemType class instance by Activator
                            // if created Instance is inherit Item, Register prototype
                            if (Activator.CreateInstance(itemType) is Item.Item item)
                            {
                                item.data = data;
                                Register(item);
                            }
                            else
                            {
                                ConsoleUI.Instance.DrawTextInBox($"{itemType.Name} 은 Item.Item 을 상속하지 않습니다.", ref ConsoleUI.logView);
                            }
>>>>>>> Stashed changes
                        }
                        else
                        {
                            ConsoleUI.Instance.DrawTextInBox($"클래스 {data.ClassName} 을 찾을 수 없습니다.", ref ConsoleUI.logView);
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

        public Item.Item? Create(int id)
        {
            return _prototypes[id].Clone() as Item.Item;
        }

        public Item.Item? FindItem(Predicate<Item.Item> predicate)
        {
            return _prototypes.Values.ToList().Find(predicate)?.Clone() as Item.Item;
        }

        public List<Item.Item> FindItems(Predicate<Item.Item> predicate)
        {
            var items = _prototypes.Values.ToList();
            return items.FindAll(predicate);
        }
    }
}
