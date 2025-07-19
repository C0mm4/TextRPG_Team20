using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.System
{
    internal class Quest
    {
        public int ID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool isClear {  get; set; }

        public List<Condition> Conditions { get; set; }
        public int Rewards {  get; set; }
    }

    internal class Condition
    {
        public int ID {  get; set; }
        public int RequireCount {  get; set; }
        public int currentCount { get; set; }
    }

    internal class QuestWrapper
    {
        public List<Quest> Quests { get; set; }
    }
}
