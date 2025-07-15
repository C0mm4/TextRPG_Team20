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
            public int Level { get; set; }
            public int Hp {  get; set; }
            public int Atk {  get; set; }
            public int Def {  get; set; }
            public int ExtraAtk {  get; set; }
            public int ExtraDef {  get; set; }

            public Status(int level, int hp, int atk, int def, int extraAtk = 0, int extraDef = 0)
            {
                Level = level;
                Hp = hp;
                Atk = atk;
                Def = def;
                ExtraAtk = extraAtk;
                ExtraDef = extraDef;
            }

            public int TotalAtk => Atk + ExtraAtk;
            public int TotalDef => Def + ExtraDef;
        }
}