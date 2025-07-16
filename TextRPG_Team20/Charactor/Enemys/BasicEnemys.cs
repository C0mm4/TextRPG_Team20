using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Charactor.Enemys
{
    internal class BlueSnail : Enemy
    {    //id, level, hp, atk, def, extraAtk, extraDef
        public BlueSnail() : base("파란 달팽이", 10, new Status(1, 1, 10, 5, 0))
        {

        }
    }

    internal class StoneGolem : Enemy
    {
        public StoneGolem() : base("스톤 골램", 20, new Status(2, 1, 30, 5, 5))
        {

        }
    }
}
