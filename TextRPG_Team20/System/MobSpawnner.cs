using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TextRPG_Team20.Item;

namespace TextRPG_Team20.System
{
    internal class MobSpawnner
    {
        private static MobSpawnner? _instance;

        private readonly Dictionary<int, Enemy> _prototypes = new();

        public static MobSpawnner Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new MobSpawnner();
                return _instance;
            }
        }

        private MobSpawnner()
        {
            var enemyData = JsonLoader.LoadJson("EnemyData.json");
            try
            {
                ItemWrapper? wrapper = JsonSerializer.Deserialize<ItemWrapper>(enemyData);
                ItemData[]? datas;
                if (wrapper != null)
                {
                    datas = wrapper.Items.ToArray();

                    if (datas != null)
                    {
                        foreach (var data in datas)
                        {
                            string? className = data.ClassName;
                            Type? itemType = AppDomain.CurrentDomain
                                                .GetAssemblies()
                                                .SelectMany(a => a.GetTypes())
                                                .FirstOrDefault(t => t.Name == className && typeof(Enemy).IsAssignableFrom(t));
                            if (itemType != null)
                            {
                                if (Activator.CreateInstance(itemType) is Enemy item)
                                {
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

            }
            catch (Exception ex)
            {
                ConsoleUI.Instance.DrawTextInBox("JSON 파일 파싱 중 오류 발생: " + ex.Message, ref ConsoleUI.logView);
            }
        }

        public void Register(Enemy item)
        {
//            _prototypes[item.data.Id] = item;
        }

/*        public Enemy? Create(int id)
        {
            return _prototypes[id].Clone() as Enemy;
        }
*/
        public List<Enemy> FindItems(Predicate<Enemy> match)
        {
            var items = _prototypes.Values.ToList();
            return items.FindAll(match);
        }
    }
}
