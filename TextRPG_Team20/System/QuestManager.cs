using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TextRPG_Team20.System
{
    internal class QuestManager
    {
        private static QuestManager? _instance;
        public static QuestManager Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new QuestManager();
                return _instance;
            }
        }

        private Dictionary<int, Quest> _quests = new();

        public Quest? GetQuest(int id)
        {
            if (_quests.ContainsKey(id))
            {
                return _quests[id];
            }
            return null;
        }

        public List<Quest> GetAllQuests()
        {
            return _quests.Values.ToList();
        }

        public void GenerateQuests()
        {
            string json = JsonLoader.LoadJson("QuestData.json");

            try
            {
                QuestWrapper? wrapper = JsonSerializer.Deserialize<QuestWrapper>(json);
                if (wrapper != null)
                {
                    foreach (var item in wrapper.Quests) 
                    {
                        _quests[item.ID] = item;
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleUI.Instance.DrawTextInBox("JSON 파일 파싱 중 오류 발생: " + ex.Message, ref ConsoleUI.logView);
            }
        }

        public void LoadQuestData()
        {
            var quests = SaveData.Instance.Quests;
            foreach (var item in quests)
            {
                _quests[item.ID] = item;
            }
        }

        public void KillEnemy(int ID)
        {
            foreach(var item in _quests.Values)
            {
                if (!item.isClear)
                {
                    bool checkCondition = true;
                    foreach (var condition in item.Conditions)
                    {
                        if (condition.ID == ID)
                        {
                            if(condition.currentCount < condition.RequireCount)
                            {
                                condition.currentCount++;
                                ConsoleUI.Instance.DrawTextInBox($"{item.Name} 진행도 업데이트", ref ConsoleUI.logView);
                                ConsoleUI.Instance.DrawTextInBox($"{MobSpawnner.Instance.GetEnemyName(ID)} 처치 {condition.currentCount} / {condition.RequireCount}", ref ConsoleUI.logView);
                                if (condition.currentCount != condition.RequireCount)
                                {
                                    checkCondition = false;
                                }
                            }
                        }
                        else
                        {

                            if (condition.currentCount != condition.RequireCount)
                            {
                                checkCondition = false;
                            }
                        }
                    }
                    if (checkCondition)
                    {
                        item.isClear = true;
                        ConsoleUI.Instance.DrawTextInBox($"{item.Name} 퀘스트 성공! {item.Rewards}G를 획득합니다.", ref ConsoleUI.logView);
                        ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
                        Game.playerInstance.AddGold(item.Rewards);
                    }
                }
            }
        }
    }
}
