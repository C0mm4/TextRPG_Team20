using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Scene;

namespace TextRPG_Team20.Dungeon
{

    public class Dungeon
    {
        public int DungeonID {  get; set; }
        public string DungeonName {  get; set; }
        public List<Field> Fields { get; set; }   //필드에서 몇다시몇좌표 current던전->필드데이터->
        public int StartX {  get; set; }
        public int StartY { get; set; }
        public int BossID {  get; set; }

        public int GetGold = 0;

        // 기본 생성자 (JSON 역직렬화용)
        public Dungeon() { }

        

        // 복사 생성자
        public Dungeon(Dungeon original)
        {
            DungeonID = original.DungeonID;
            DungeonName = original.DungeonName;
            StartX = original.StartX;
            StartY = original.StartY;
            BossID = original.BossID;
            // Fields는 각 Field 객체를 깊은 복사해야 합니다.
            Fields = original.Fields?.Select(f => new Field(f)).ToList();
        }

        public Field GetFieldById(int fieldId)
        {
            return Fields.FirstOrDefault(f => f.FieldID == fieldId);
        }

        public Field GetStartField()
        {
            return Fields[0];
        }
    }

    public class Field
    {
        public int FieldID { get; set; }
        public string FieldName { get; set; }
        public List<List<int>> GridData { get; set; } = new();
        public List<Connection> Connections { get; set; }

        public Field() { }
        public Field(Field original)
        {
            FieldID = original.FieldID;
            FieldName = original.FieldName;

            // GridData 깊은 복사
            if (original.GridData != null)
            {
                GridData = new List<List<int>>();
                foreach (var row in original.GridData)
                {
                    GridData.Add(new List<int>(row)); // 각 행을 복사
                }
            }
            else
            {
                GridData = new List<List<int>>();
            }

            // Connections 깊은 복사
            Connections = original.Connections?
                .Select(conn => new Connection(conn)).ToList()
                ?? new List<Connection>();
        }

        public List<string> ToPrintString()
        {
            List<string> ret = new List<string>();
            foreach(var row in GridData)
            {
                StringBuilder sb = new StringBuilder();
                foreach(var col in row)
                {
                    string ansi = "";
                    string tile = "  ";
                    switch (col)
                    {
                        case -1:
                            ansi = AnsiColor.Green;
                            tile = "@";
                            break;
                        case 1:
                            ansi = AnsiColor.White;
                            tile = "#";
                            break;
                        case 2:
                            ansi = AnsiColor.Yellow;
                            tile = "C";
                            break;
                        case 3:
                            ansi = AnsiColor.Cyan;
                            tile = "D";
                            break;
                        case 4:
                            ansi = AnsiColor.Magenta;
                            tile = "B";
                            break;
                        case 5:
                            ansi = AnsiColor.Blue;
                            tile = "P";
                            break;
                        case 6:
                            ansi = AnsiColor.Red;
                            tile = "@";
                            break;
                        case 7:
                            ansi = AnsiColor.Magenta;
                            tile = "@";
                            break;
                        case 0:
                            tile = " ";
                            break;
                        default:
                            tile = "A";
                            break;
                    }
                    sb.Append($"{ansi}{tile}{AnsiColor.Reset}");
                }
                ret.Add(sb.ToString());
            }

            return ret;
        }

        public int GetLength(int layer)
        {
            if (layer == 0)
            {
                return GridData.Count;
            }
            if (layer == 1)
            {
                return GridData[0].Count;
            }
            return 0;
        }

        public int this[int i, int j]
        {
            get => GridData[i][j];
            set => GridData[i][j] = value;
        }

    }

    public class Connection
    {
        public List<int> FromCell { get; set; }
        public List<int> ToCell { get; set; }
        public int ToFieldID {  get; set; }

        public Connection() { }

        public Connection(Connection original)
        {
            FromCell = original.FromCell?.ToList();
            ToFieldID = original.ToFieldID;
            ToCell = original.ToCell?.ToList();
        }
    }

    public class DungeonData
    {
        public List<Dungeon> Dungeons { get; set; }

        public Dungeon? GetDungeonById(int dungeonId)
        {
            return Dungeons.FirstOrDefault(d => d.DungeonID == dungeonId) ?? null;
        }
    }
}
