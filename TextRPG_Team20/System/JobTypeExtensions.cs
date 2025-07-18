using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.System
{
    public static class JobTypeExtensions
    {
        
        public static string ToKoreanString(this JobType jobType)
        {
            switch (jobType)
            {
                case JobType.Warrior:
                    return "전사";
                case JobType.Archer:
                    return "궁수";
                case JobType.Mage:
                    return "마법사";
                case JobType.None: 
                    return "알 수 없음";
                default:
                    return jobType.ToString(); 
            }
        }
    }
}
