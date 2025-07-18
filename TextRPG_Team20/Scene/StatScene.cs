using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class StatScene : Scene
    {

        private readonly Status Nowstatus;
        public StatScene(Status status)
        {
            Nowstatus = status;
        }


        public override void PrintScene()
        {
            Game.playerInstance.AddGold(20000000);
            Game.playerInstance.AddSkill();
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}STAT{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"{Nowstatus.Level} Lv", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"체력       : {Nowstatus.HP}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"공격력      : {Nowstatus.Atk}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"방어력      : {Nowstatus.Def}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"추가 공격력: {Nowstatus.ExtraAtk}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"추가 방어력: {Nowstatus.ExtraDef}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1.인벤토리로 가기", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("2.스킬 확인", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("0.로비로 돌아가기", ref ConsoleUI.mainView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);


        }


        public override bool Action(int input)
        {
            switch (input)
            {
                case 0:
                    Game.Instance.PopScene();
                    return false;
                case 1:
                    Game.Instance.SceneChange(Game.SceneState.Inventory);
                    return false;
                case 2:
                    Game.Instance.SceneChange(Game.SceneState.SkillList);
                    return false;

                default:
                    ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}잘못된 입력입니다!{AnsiColor.Reset}", ref ConsoleUI.logView);
                    ConsoleUI.Instance.PrintView(ref ConsoleUI.logView, "left", "top"); 
                    return false;
            }
        }
    }
}
