using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.System;

namespace TextRPG_Team20
{
    internal class SkillBook : ConsumeItem
    {
        public SkillBook(ItemData itemData) : base(itemData)
        {

        }

        public SkillBook()
        {

        }
        public override void useitem()
        {
            Game.playerInstance.AddSkill();
        }
    }
}
