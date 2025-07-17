using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20;

namespace TextRPG_Team20
{
    public class Status
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public int Level { get; set; }
        public int HP {  get; set; }
        public int MaxHP { get; set; }
        public int Atk {  get; set; }
        public int Def {  get; set; }
        public int ExtraAtk {  get; set; }
        public int ExtraDef {  get; set; }
        public string? ClassName {  get; set; }

        public int Gold {  get; set; }
        
        public Status(int id, int level, int hp, int atk, int def, string name, int gold, int extraAtk = 0, int extraDef = 0)
        {
            Level = level;
            MaxHP = hp;
            HP = hp;
            Atk = atk;
            Def = def;
            ExtraAtk = extraAtk;
            ExtraDef = extraDef;
            ID = id;
            Name = name;
            Gold = gold;
        }

        public int TotalAtk => Atk + ExtraAtk;
        public int TotalDef => Def + ExtraDef;
    }

    public class AsciiData
    {
        public int ID { get; set; }
        public List<string> Data { get; set; }
    }

    public class StatusWrap() 
    {
        public List<Status> Enemy {  get; set; }
        public List<AsciiData> AsciiData {  get; set; }
    }
    
}