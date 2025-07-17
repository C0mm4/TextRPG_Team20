using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Skill
{
    internal interface ISkill : ICommand
    {
        public void Action(List<Enemy> targets);
    }

    internal class SkillData 
    {
        public int ID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Range {  get; set; }
        public int Cost {  get; set; }
        public float AtkPercent {  get; set; }
        
    }

    internal class SkillWrap
    {
        public List<SkillData> Skills { get; set; }
    }
}
