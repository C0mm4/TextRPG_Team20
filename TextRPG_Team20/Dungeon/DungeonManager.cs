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

        private DungeonData _dungeonData;

        public DungeonManager()
        {
            _dungeonData = new DungeonData { Dungeons = new List<Dungeon>() };
            LoadDungeonData("DungeonData.json");
        }

        public Dungeon currentDungeon;
        public Field currentField;

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
                Console.WriteLine("던전 데이터 로드 완료!");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"오류: 파일을 찾을 수 없습니다 - {filePath}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"오류: JSON 파싱 중 오류 발생 - {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"예기치 않은 오류 발생: {ex.Message}");
            }
        }

        /// <summary>
        /// 특정 던전을 가져옵니다.
        /// </summary>
        /// <param name="dungeonId">던전 ID</param>
        /// <returns>해당 던전 객체 또는 null</returns>
        public Dungeon? GetDungeon(int dungeonId)
        {
            return _dungeonData?.GetDungeonById(dungeonId);
        }

        // 예시: 현재 필드 정보를 출력하는 메서드 (게임 로직에서 활용)
        public void PrintField(Field field)
        {
            if (field == null)
            {
                Console.WriteLine("필드를 찾을 수 없습니다.");
                return;
            }

            Console.WriteLine($"\n--- 필드: {field.FieldName} (ID: {field.FieldID}) ---");
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 5; c++)
                {
                    CellType cell = (CellType)field[r, c];
                    switch (cell)
                    {
                        case CellType.Empty:
                            Console.Write(" . "); // 이동 가능
                            break;
                        case CellType.Wall:
                            Console.Write(" # "); // 벽
                            break;
                        case CellType.Enemy:
                            Console.Write(" E "); // 적
                            break;
                        case CellType.ItemBox:
                            Console.Write(" B "); // 아이템 상자
                            break;
                        case CellType.ConnectionPortal:
                            Console.Write(" P "); // 연결 통로
                            break;
                        case CellType.Trap:
                            Console.Write(" T "); // 함정
                            break;
                        default:
                            Console.Write(" ? "); // 알 수 없음
                            break;
                    }
                }
                Console.WriteLine();
            }
        }
        
        public void StartDungone(int DungeonID)
        {
            currentDungeon = GetDungeon(DungeonID);
            currentField = currentDungeon.GetStartField();
        }
    }
}
