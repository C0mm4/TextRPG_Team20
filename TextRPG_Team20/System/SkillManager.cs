using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TextRPG_Team20.Skill;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TextRPG_Team20.System
{
    internal class SkillManager
    {
        private readonly Dictionary<int, Skill.Skill> _skills = new();
        private static SkillManager? _instance;
        public static SkillManager? Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new SkillManager();
                return _instance;
            }
        }

        private SkillManager()
        {
            var skillDataStr = JsonLoader.LoadJson("SkillData.json");
            try
            {
                SkillWrap wrapper = JsonSerializer.Deserialize<SkillWrap>(skillDataStr);
                if (wrapper != null)
                {
                    foreach (var skill in wrapper.Skills)
                    {
                        Skill.Skill skl = new Skill.Skill();
                        skl.Data = skill;
                        Register(skl);
                    }
                }
            }
            catch (Exception ex) 
            {
                ConsoleUI.Instance.DrawTextInBox("JSON 파일 파싱 중 오류 발생: " + ex.Message, ref ConsoleUI.logView);
            }
        }

        public void Register(Skill.Skill skill)
        {
            _skills[skill.Data.ID] = skill;
        }

        public Skill.Skill? GetSkill(int index)
        {
            if(_skills.ContainsKey(index))
                return _skills[index];
            return null;
        }
    }
}
