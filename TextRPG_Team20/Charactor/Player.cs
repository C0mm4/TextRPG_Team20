using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Dungeon;
using TextRPG_Team20.Scene;
using TextRPG_Team20.Skill;
using TextRPG_Team20.System;

namespace TextRPG_Team20
{
    internal class Player : Character
    {
        public List<Skill.Skill> skills = new();

        public Player(JobType jobType, Status status)  : base(jobType, status)
        {
            
        }
        public void AddSkill()
        {

            Skill.Skill skill = SkillManager.Instance.GetRandomSkill();
            if (skills.Any(s => s.Data.ID == skill.Data.ID))
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}이미 배운 스킬입니다.{AnsiColor.Reset}", ref ConsoleUI.logView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);

                return;
            }
            if (Game.playerInstance.Job == (JobType)skill.Data.Class)
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}{skill.Data.Name}을 습득했습니다!!{AnsiColor.Reset}", ref ConsoleUI.logView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
                
                skills.Add(skill);
            }
            else
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}{skill.Data.Name}은(는) 다른직업의 스킬이라 획득에 실패했습니다..{AnsiColor.Reset}", ref ConsoleUI.logView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);              
            }

            skills.Sort();
        }
        public Skill.Skill? SelectSkill()
        {
            ConsoleUI.info2View.ClearBuffer();
            ConsoleUI.Instance.DrawTextInBox("=== 스킬 선택 ===", ref ConsoleUI.info2View);          //delete~
            for (int i = 0; i < skills.Count; i++)
            {
                ConsoleUI.Instance.DrawTextInBox($" {ConsoleUI.PadRightDisplay($"{i + 1}. {skills[i].Data.Name}", 30)} | 소모 골드 : {skills[i].Data.Cost}", ref ConsoleUI.inputView);
            }
            ConsoleUI.Instance.DrawTextInBox("선택 >>", ref ConsoleUI.inputView);
            ConsoleUI.Instance.DrawTextInBox("0.취소", ref ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);

            string? input = ConsoleUI.Read(ref ConsoleUI.inputView);
            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= skills.Count)
            {
                return skills[choice - 1];
            }
            else
            {
                return null;                                                                  //delete~
            }
        }

        public override void Action()
        {
            ConsoleUI.info2View.ClearBuffer();
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}{status.Name}의 차례입니다. 행동을 선택하세요.{AnsiColor.Reset}", ref ConsoleUI.info2View);
            ConsoleUI.Instance.DrawTextInBox("1.일반 공격", ref ConsoleUI.info2View);
            ConsoleUI.Instance.DrawTextInBox("2.스킬 사용", ref ConsoleUI.info2View);      
        }

        public override void CharacterInfo()
        {
            ConsoleUI.Instance.DrawTextInBox($"캐릭터 정보", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"{status.Name} {Job.ToKoreanString()}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"공격력 : {status.Atk} {(status.ExtraAtk == 0 ? "" : $" + ({status.ExtraAtk})")}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"방어력 : {status.Def} {(status.ExtraDef == 0 ? "" : $" + ({status.ExtraDef})")}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"체력 : {status.HP} / {status.MaxHP}", ref ConsoleUI.info1View);
            ConsoleUI.Instance.DrawTextInBox($"Gold : {status.Gold} G", ref ConsoleUI.info1View);
        }

        public override void Attack(Character target)
        {
            int damage = status.Atk;
            int actualDamage = Math.Max(1, damage - target.status.Def); //최소한 1의 피해는 무조건 입힌다
            target.DecreaseHp(actualDamage);

            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}{status.Name}{AnsiColor.Reset}이(가) {AnsiColor.Green}{target.status.Name}{AnsiColor.Reset}에게 일반 공격!", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Green}{actualDamage}{AnsiColor.Reset}의 피해를 입혔습니다.", ref ConsoleUI.logView);
        }

        public void UseSkill(List<Enemy> target, ISkill skill)
        {
            skill.Action(target);
            return;
        }

        public bool playercollision(int x, int y)
        {
            var CurrentDungeon = DungeonManager.Instance.currentDungeon;
            var CurrentField = DungeonManager.Instance.currentField;
            DungeonManager.Instance.currentField[this.y, this.x] = currentPosData;
            int[] newpos = { this.x + x, this.y + y };
            if (newpos[0] >= 0 && newpos[1] >= 0 && newpos[0] < CurrentField.GetLength(1) && newpos[1] < CurrentField.GetLength(0))
            {

                if (DungeonManager.Instance.currentField[newpos[1], newpos[0]] == 0)
                {
                    this.x = newpos[0];
                    this.y = newpos[1];
                }
                else if (DungeonManager.Instance.currentField[newpos[1], newpos[0]] == 1)
                { }

                else if (DungeonManager.Instance.currentField[newpos[1], newpos[0]] == 2)
                {

                    ConsoleUI.Instance.DrawTextInBox("상자를 발견했습니다. 축하합니다 5골드를 획득하셨습니다.", ref ConsoleUI.logView);
                    DungeonManager.Instance.currentField[newpos[1], newpos[0]] = 0;
                    this.x = newpos[0];
                    this.y = newpos[1];
                }
                else if (DungeonManager.Instance.currentField[newpos[1], newpos[0]] == 3)
                {
                    ConsoleUI.Instance.DrawTextInBox("닫힌 문입니다 지금은 지나가실 수 없습니다.", ref ConsoleUI.logView);

                }
                else if (DungeonManager.Instance.currentField[newpos[1], newpos[0]] == 4)
                {
                    ConsoleUI.Instance.DrawTextInBox("발판이 눌렸습니다 어딘가의 문이나 상자가 열렸을지도 모르겠네요.", ref ConsoleUI.logView);
                    DungeonManager.Instance.currentField[newpos[1], newpos[0]] = 0;

                    DungeonManager.Instance.OpenAllDoorsInDungeon(CurrentDungeon);


                    this.x = newpos[0];
                    this.y = newpos[1];
                }
                else if (DungeonManager.Instance.currentField[newpos[1], newpos[0]] == 5)
                {
                    ConsoleUI.Instance.DrawTextInBox("다음방으로 넘어갑니다.", ref ConsoleUI.logView);
                    var moveToField = CurrentField.Connections.FirstOrDefault(c => (c.FromCell[0] == newpos[0] && c.FromCell[1] == newpos[1]));
                    DungeonManager.Instance.MoveField(moveToField);
                    return true;
                }
                else if (DungeonManager.Instance.currentField[newpos[1], newpos[0]] == 6)
                {
                    DungeonManager.Instance.currentField[newpos[1], newpos[0]] = 0;
                    ConsoleUI.Instance.DrawTextInBox("적을 마주쳤다!" , ref ConsoleUI.logView);
                    Game.Instance.SceneChange(Game.SceneState.Battle);

                    this.x = newpos[0];
                    this.y = newpos[1];
                    currentPosData = DungeonManager.Instance.currentField[this.y, this.x];
                    DungeonManager.Instance.currentField[this.y, this.x] = -1;
                    return true;
                }
                else if (DungeonManager.Instance.currentField[newpos[1], newpos[0]] == 7)
                {
                    DungeonManager.Instance.currentField[newpos[1], newpos[0]] = 0;
                    ConsoleUI.Instance.DrawTextInBox("보스을 마주쳤다!", ref ConsoleUI.logView);
                    Game.Instance.SceneChange(Game.SceneState.BossBattle);

                    this.x = newpos[0];
                    this.y = newpos[1];
                    currentPosData = DungeonManager.Instance.currentField[this.y, this.x];
                    DungeonManager.Instance.currentField[this.y, this.x] = -1;
                    return true;
                }
            }
            currentPosData = DungeonManager.Instance.currentField[this.y, this.x];
            DungeonManager.Instance.currentField[this.y, this.x] = -1;
            return false;
        }

        public int LastBattleGold { get; private set; }
        public override void AddGold(int gold)
        {
            base.AddGold(gold);
            LastBattleGold += gold;
            if(DungeonManager.Instance.currentDungeon != null)
            {
                DungeonManager.Instance.currentDungeon.GetGold += gold;
            }
        }
  
        public void ResetLastBattleGold()
        {
            
            LastBattleGold = 0;
        }

        public override void PrintData()
        {

        }

        public override void PrintInField()
        {

        }
    }
}
