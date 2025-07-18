using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        private ItemType ConvertStringToItemType(string itemTypeString)
        {
            return itemTypeString switch
            {
                "무기" => ItemType.Weapon,
                "모자" => ItemType.Head,
                "상의" => ItemType.Top,
                "하의" => ItemType.Bottom,
                "악세서리" => ItemType.Accessory,
                "소모품" => ItemType.Consumable,
                _ => ItemType.None
            };
        }

        private List<JobType> ConvertClassStringToJobTypes(string? classString)
        {
            List<JobType> jobTypes = new List<JobType>();

            if (string.IsNullOrEmpty(classString))
            {
                return jobTypes;
            }

            switch (classString)
            {
                case "공용":                 
                    break;
                case "전사":
                    jobTypes.Add(JobType.Warrior);
                    break;
                case "궁수":
                    jobTypes.Add(JobType.Archer);
                    break;
                case "마법사":
                    jobTypes.Add(JobType.Mage);
                    break;               
                default:
                    ConsoleUI.Instance.DrawTextInBox($"경고: 알 수 없는 직업 제한 '{classString}'이(가) 발견되었습니다.", ref ConsoleUI.logView);
                    break;
            }
            return jobTypes;
        }

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
                        // test
                        string? className = data.ClassName;
                        // Get Application Domain Assemblies
                        Type? itemType = AppDomain.CurrentDomain
                                            .GetAssemblies()
                                            .SelectMany(a => a.GetTypes())
                                            .FirstOrDefault(t => t.Name == className && typeof(Item).IsAssignableFrom(t));
                        if (itemType != null)
                        {
                            // Create itemType class instance by Activator
                            // if created Instance is inherit Item, Register prototype
                            if (Activator.CreateInstance(itemType) is Item item)
                            {
                                item.data = data;
                                item.SetType();

                                item.data.EquipableJobs = ConvertClassStringToJobTypes(data.Class);

                                Register(item);
                            }
                            else
                            {
                                ConsoleUI.Instance.DrawTextInBox($"{itemType.Name} 은 Item.Item 을 상속하지 않습니다.", ref ConsoleUI.logView);
                            }
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
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
        }

        private readonly Dictionary<int, Item> _prototypes = new();

        public void Register(Item item)
        {
            _prototypes[item.data.ID] = item;
        }

        public Item? Create(int id)
        {
            if (_prototypes.TryGetValue(id, out var prototype))
            {
                return prototype.Clone() as Item;
            }
            return null;
        }

        public Item? FindItem(Predicate<Item> predicate)
        {
            return _prototypes.Values.ToList().Find(predicate)?.Clone() as Item;
        }

        public List<Item> FindItems(Predicate<Item> predicate)
        {
            var items = _prototypes.Values.ToList();
            return items.FindAll(predicate);
        }

    }
}
