using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TextRPG_Team20.System;

namespace TextRPG_Team20.Dungeon
{
    public class DungeonManager
    {
        private static DungeonManager _instance;
        public static DungeonManager Instance
        {
            get { 
                if(_instance == null)
                    _instance = new DungeonManager();
                return _instance; 
            }
        }

        public void OpenAllDoorsInDungeon(Dungeon dungeon)
        {
            foreach (var field in dungeon.Fields)
            {
                for (int y = 0; y < field.GetLength(0); y++)
                {
                    for (int x = 0; x < field.GetLength(1); x++)
                    {
                        if (field[y, x] == 3)
                        {
                            field[y, x] = 0;
                            ConsoleUI.Instance.DrawTextInBox(
                                $"[문 열림] {field.FieldName} 필드의 위치 ({x}, {y})", ref ConsoleUI.logView);
                        }
                    }
                }
            }
        }

        private DungeonData _dungeonData;

        public DungeonManager()
        {
            _dungeonData = new DungeonData { Dungeons = new List<Dungeon>() };
            LoadDungeonData("DungeonData.json");
        }

        public Dungeon currentDungeon;
        public Field currentField;

        public bool[] isDungeonClear { get; set; }
        public bool[] isAbleDungeon { get; set; }

        /// <summary>
        /// JSON 파일에서 던전 데이터를 로드합니다.
        /// </summary>
        /// <param name="filePath">JSON 파일 경로</param>
        public void LoadDungeonData(string filePath)
        {
            try
            {
                string jsonString = JsonLoader.LoadJson(filePath);
                _dungeonData = JsonSerializer.Deserialize<DungeonData>(jsonString);

                isDungeonClear = new bool[_dungeonData.Dungeons.Count];
                isAbleDungeon = new bool[_dungeonData.Dungeons.Count];
                isAbleDungeon[0] = true;
                // for map test
                isAbleDungeon[1] = true;
                isAbleDungeon[2] = true;
                isAbleDungeon[3] = true;
            }
            catch (FileNotFoundException)
            {
                ConsoleUI.Instance.DrawTextInBox($"오류: 파일을 찾을 수 없습니다 - {filePath}", ref ConsoleUI.logView);
            }
            catch (JsonException ex)
            {
                ConsoleUI.Instance.DrawTextInBox($"오류: JSON 파싱 중 오류 발생 - {ex.Message}", ref ConsoleUI.logView);
            }
            catch (Exception ex)
            {
                ConsoleUI.Instance.DrawTextInBox($"예기치 않은 오류 발생: {ex.Message}", ref ConsoleUI.logView);
            }
        }

        /// <summary>
        /// 특정 던전을 가져옵니다.
        /// </summary>
        /// <param name="dungeonId">던전 ID</param>
        /// <returns>해당 던전 객체 또는 null</returns>
        public Dungeon? GetDungeon(int dungeonId)
        {
            return new Dungeon(_dungeonData?.GetDungeonById(dungeonId));
        }

       

        
        public void StartDungone(int DungeonID)
        {
            currentDungeon = GetDungeon(DungeonID);
            currentField = currentDungeon.GetStartField();
            Game.playerInstance.SetPos(currentDungeon.StartX, currentDungeon.StartY);
        }

        public void MoveField(Connection? connection)
        {
            if(connection != null)
            {
                currentField = currentDungeon.GetFieldById(connection.ToFieldID);
                Game.playerInstance.SetPos(connection.ToCell[0], connection.ToCell[1]);
            }
        }

        public void ClearDungeon(int ID)
        {
            if (isDungeonClear != null && ID - 1 >= 0 && ID - 1 < isDungeonClear.Length)
                isDungeonClear[ID - 1] = true;

            if (isAbleDungeon != null && ID >= 0 && ID < isAbleDungeon.Length)
                isAbleDungeon[ID] = true;
        }
    }
}
