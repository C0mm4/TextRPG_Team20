using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TextRPG_Team20.System
{
    public class EnemySpawnSetting
    {
        public List<int> EnemysID { get; set; } = new();
        public int MinCount { get; set; } = 1;
        public int MaxCount { get; set; } = 1;
        public bool IsRandom { get; set; } = false;
    }

    public class FieldSpawnData
    {
        public int FieldID { get; set; }
        public EnemySpawnSetting SpawnSetting { get; set; } = new();
    }

    public class EnemySpawnData
    {
        public List<FieldSpawnData> FieldSpawns { get; set; } = new();
    }

    internal class MobSpawnner
    {
        private readonly Dictionary<int, Enemy> _prototypes = new();
        private static MobSpawnner? _instance;
        private EnemySpawnData _spawnData;
        private Random _random;
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
            _random = new Random();
            _spawnData = new EnemySpawnData();
            
            var enemyData = JsonLoader.LoadJson("EnemyData.json");
            ConsoleUI.Instance.DrawTextInBox($"EnemyData.json 로드 완료", ref ConsoleUI.logView);
            try
            {
                StatusWrap? wrapper = JsonSerializer.Deserialize<StatusWrap>(enemyData);
                if (wrapper != null)
                {
                    ConsoleUI.Instance.DrawTextInBox($"적 데이터 파싱 완료, 적 수: {wrapper.Enemy.Count}", ref ConsoleUI.logView);
                    foreach (var data in wrapper.Enemy)
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
                                item.status = data;
                                Register(item);
                                ConsoleUI.Instance.DrawTextInBox($"적 등록 완료 - ID: {data.ID}, 이름: {data.Name}", ref ConsoleUI.logView);
                            }
                            else
                            {
                                ConsoleUI.Instance.DrawTextInBox($"{itemType.Name} 은 Item.Item 을 상속하지 않습니다.", ref ConsoleUI.logView);
                            }
                        }
                        else
                        {
                            ConsoleUI.Instance.DrawTextInBox($"클래스 {data.ClassName} 을 찾을 수 없습니다. (ID: {data.ID})", ref ConsoleUI.logView);
                        }
                    }

                    foreach(var ascii in wrapper.AsciiData)
                    {
                        if(_prototypes.ContainsKey(ascii.ID))
                            _prototypes[ascii.ID].asciiData = ascii.Data;
                    }
                }
                else
                {
                    ConsoleUI.Instance.DrawTextInBox("StatusWrap 파싱 실패", ref ConsoleUI.logView);
                }

            }
            catch (Exception ex)
            {
                ConsoleUI.Instance.DrawTextInBox("JSON 파일 파싱 중 오류 발생: " + ex.Message, ref ConsoleUI.logView);
            }
            
            LoadSpawnData("EnemySpawnData.json");
            
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
        }

        public void Register(Enemy item)
        {
            _prototypes[item.status.ID] = item;
        }

        public Enemy? Create(int id)
        {
            if (_prototypes.ContainsKey(id))
            {
                return _prototypes[id].Clone() as Enemy;
            }
            return null;
        }

        public List<Enemy> FindItems(Predicate<Enemy> match)
        {
            var items = _prototypes.Values.ToList();
            return items.FindAll(match);
        }

        private void LoadSpawnData(string filePath)
        {
            try
            {
                string jsonString = JsonLoader.LoadJson(filePath);
                _spawnData = JsonSerializer.Deserialize<EnemySpawnData>(jsonString);
                //ConsoleUI.Instance.DrawTextInBox($"스폰 데이터 로드 완료, 필드 수: {_spawnData.FieldSpawns.Count}", ref ConsoleUI.logView);
            }
            catch (FileNotFoundException)
            {
                ConsoleUI.Instance.DrawTextInBox($"경고: 적 스폰 데이터 파일을 찾을 수 없습니다 - {filePath}", ref ConsoleUI.logView);
                InitializeDefaultSpawnData();
            }
            catch (JsonException ex)
            {
                ConsoleUI.Instance.DrawTextInBox($"오류: 적 스폰 데이터 JSON 파싱 중 오류 발생 - {ex.Message}", ref ConsoleUI.logView);
                InitializeDefaultSpawnData();
            }
            catch (Exception ex)
            {
                ConsoleUI.Instance.DrawTextInBox($"예기치 않은 오류 발생: {ex.Message}", ref ConsoleUI.logView);
                InitializeDefaultSpawnData();
            }
        }

        private void InitializeDefaultSpawnData()
        {
            _spawnData = new EnemySpawnData
            {
                FieldSpawns = new List<FieldSpawnData>
                {
                    new FieldSpawnData 
                    { 
                        FieldID = 1, 
                        SpawnSetting = new EnemySpawnSetting 
                        { 
                            EnemysID = new List<int> { 1, 2 }, 
                            MinCount = 1, 
                            MaxCount = 2, 
                            IsRandom = true 
                        } 
                    }
                }
            };
        }

        public List<Enemy> GetEnemiesForField(int fieldID)
        {
            var fieldSpawn = _spawnData.FieldSpawns.FirstOrDefault(fs => fs.FieldID == fieldID);
            if (fieldSpawn == null)
            {
                ConsoleUI.Instance.DrawTextInBox($"필드 {fieldID}에 대한 적 스폰 설정이 없습니다.", ref ConsoleUI.logView);
                return new List<Enemy>();
            }

            var setting = fieldSpawn.SpawnSetting;
            var enemies = new List<Enemy>();

            if (setting.EnemysID.Count == 0)
            {
                ConsoleUI.Instance.DrawTextInBox($"필드 {fieldID}에 스폰할 적 종류가 설정되지 않았습니다.", ref ConsoleUI.logView);
                return enemies;
            }

            int spawnCount = _random.Next(setting.MinCount, setting.MaxCount + 1);

            for (int i = 0; i < spawnCount; i++)
            {
                int enemyTypeIndex;
                if (setting.IsRandom)
                {
                    enemyTypeIndex = _random.Next(setting.EnemysID.Count);
                }
                else
                {
                    enemyTypeIndex = i % setting.EnemysID.Count;
                }

                int enemyID = setting.EnemysID[enemyTypeIndex];
                Enemy enemy = Create(enemyID);
                
                if (enemy != null)
                {
                    enemies.Add(enemy);
                }
                else
                {
                    ConsoleUI.Instance.DrawTextInBox($"ID {enemyID}인 적을 생성할 수 없습니다.", ref ConsoleUI.logView);
                }
            }

            return enemies;
        }

        public EnemySpawnSetting GetSpawnSettingForField(int fieldID)
        {
            var fieldSpawn = _spawnData.FieldSpawns.FirstOrDefault(fs => fs.FieldID == fieldID);
            return fieldSpawn?.SpawnSetting ?? new EnemySpawnSetting();
        }

        public void SetSpawnSettingForField(int fieldID, EnemySpawnSetting setting)
        {
            var fieldSpawn = _spawnData.FieldSpawns.FirstOrDefault(fs => fs.FieldID == fieldID);
            if (fieldSpawn != null)
            {
                fieldSpawn.SpawnSetting = setting;
            }
            else
            {
                _spawnData.FieldSpawns.Add(new FieldSpawnData { FieldID = fieldID, SpawnSetting = setting });
            }
        }

        public void SaveSpawnData(string filePath = "EnemySpawnData.json")
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(_spawnData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonString);
                //ConsoleUI.Instance.DrawTextInBox($"적 스폰 데이터가 {filePath}에 저장되었습니다.", ref ConsoleUI.logView);
            }
            catch (Exception ex)
            {
                ConsoleUI.Instance.DrawTextInBox($"적 스폰 데이터 저장 중 오류 발생: {ex.Message}", ref ConsoleUI.logView);
            }
        }
    }
}
