using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Charactor.Enemys
{
    internal class BlueSnail : Enemy
    {
        public BlueSnail() : base("파란 달팽이", 10, new Status(1, 1, 10, 2, 0))
        {

        }
    }

    internal class StoneGolem : Enemy
    {
        public StoneGolem() : base("스톤 골램", 10, new Status(2, 1, 30, 5, 5))
        {

        }
    }
}
