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
            public int Level;
            public int Hp;
            public int Atk;
            public int Def;
            public int ExtraAtk;
            public int ExtraDef;

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


