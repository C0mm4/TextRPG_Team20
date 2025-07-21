using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TextRPG_Team20.Dungeon;
using TextRPG_Team20.System;

namespace TextRPG_Team20
{
    internal class SaveData
    {
        private static SaveData? _instance;
        public static SaveData Instance
        {
            get
            {
                if(_instance == null )
                    _instance = new SaveData();
                return _instance;
            }
        }
        public List<bool> AbleData {  get; set; }
        public List<bool> ClearData { get; set; }
        public List<int> EquipsID { get; set; }

        // 아이템 ID와 개수를 함께 저장
        public List<ItemSlotData> Inventory { get; set; }

        public int Gold { get; set; }
        public string Name { get; set; }
        public int Job { get; set; }
        public List<int> skills { get; set; }

        public List<Quest> Quests { get; set; }

        private static readonly string SavePath = "SaveData.json";
        public void Save()
        {
            AbleData = DungeonManager.Instance.isAbleDungeon.ToList();
            ClearData = DungeonManager.Instance.isDungeonClear.ToList();
            EquipsID = Game.playerInstance.Inventory.Items
                .Where(item => item.data.isEquipped == true)
                .Select(item =>item.data.ID).ToList();

            Inventory = Game.playerInstance.Inventory.Items
                .Where(item => item.data.isEquipped == false)
                .Select(item => new ItemSlotData
                    {
                        ID = item.data.ID,
                        Quantity = item.CurrentStack
                    }).ToList();

            Gold = Game.playerInstance.status.Gold;
            Name = Game.playerInstance.status.Name;
            Job = (int)Game.playerInstance.Job;
            skills = Game.playerInstance.skills
                .Select(skill => skill.Data.ID).ToList();
            Quests = QuestManager.Instance.GetAllQuests();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(this, options);
            File.WriteAllText(SavePath, json);
        }

        public void Load()
        {
            if (!File.Exists(SavePath))
            {
                Console.WriteLine("세이브 파일이 존재하지 않습니다.");
                return;
            }

            string json;
            if (!File.Exists(SavePath))
            {
                return;

            }
            json = File.ReadAllText(SavePath);
            SaveData? loadedData = JsonSerializer.Deserialize<SaveData>(json);

            if (loadedData == null)
            {
                Console.WriteLine("세이브 데이터를 불러오는데 실패했습니다.");
                return;
            }


            // 데이터를 게임에 반영 (예: Gold, Name 등)
            Game.Instance.CreatePlayerInstance(loadedData.Name, (JobType)loadedData.Job, loadedData.Gold);

            // 인벤토리 복구
            Game.playerInstance.Inventory.Items.Clear();

            foreach (var itemData in loadedData.Inventory)
            {
                Item newItem = ItemManager.Instance.Create(itemData.ID);
                newItem.CurrentStack = itemData.Quantity;
                Game.playerInstance.Inventory.AddItem(newItem);
            }

            // 장비 복구
            foreach (var equipID in loadedData.EquipsID)
            {
                Item equipItem = ItemManager.Instance.Create(equipID);
                equipItem.data.isEquipped = true;
                Game.playerInstance.Inventory.AddItem(equipItem);
                Game.playerInstance.Inventory.ApplyEquipStats(equipItem);
            }

            // 스킬 복구
            Game.playerInstance.skills.Clear();
            foreach (int skillID in loadedData.skills)
            {
                Skill.Skill newSkill = SkillManager.Instance.GetSkill(skillID);
                Game.playerInstance.skills.Add(newSkill);
            }

            // 클리어 데이터 복구
            DungeonManager.Instance.isDungeonClear = loadedData.ClearData.ToArray();
            DungeonManager.Instance.isAbleDungeon = loadedData.AbleData.ToArray();
            this.Quests = loadedData.Quests;
            QuestManager.Instance.LoadQuestData();
//            DungeonManager.Instance.isDungeonClear = new BitArray(loadedData.ClearData.ToArray());
        }
        
    }

    // 새로운 클래스 정의: 아이템 한 칸
    internal class ItemSlotData
    {
        public int ID { get; set; }
        public int Quantity { get; set; }  // 소모품일 경우만 1 이상
    }
}
