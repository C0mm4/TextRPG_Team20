using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.System;

namespace TextRPG_Team20.Scene
{
    internal class QuestListScene : Scene
    {
        private bool isShowQuest = false;
        Quest? targetQuest;

        public override bool Action(int input)
        {
            if (isShowQuest)
            {
                if (input == 0)
                {
                    isShowQuest = false;
                    return false;
                }
                InvalidInput();
                return true;
            }
            else
            {
                if (input == 0)
                {
                    Game.Instance.PopScene();
                    return false;
                }

                targetQuest = QuestManager.Instance.GetQuest(input);
                if (targetQuest != null)
                {
                    isShowQuest = true;
                    return false;
                }
                InvalidInput();
                return true;
            }
        }

        public override void PrintScene()
        {
            if(isShowQuest)
            {
                if (targetQuest != null)
                {
                    ConsoleUI.Instance.DrawTextInBox("====퀘스트 정보====", ref ConsoleUI.mainView);
                    ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
                    ConsoleUI.Instance.DrawTextInBox($"{targetQuest.Name}", ref ConsoleUI.mainView);
                    ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
                    ConsoleUI.Instance.DrawTextInBox($"{targetQuest.Description}", ref ConsoleUI.mainView);
                    ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
                    foreach (var condi in targetQuest.Conditions)
                    {
                        ConsoleUI.Instance.DrawTextInBox($"{MobSpawnner.Instance.GetEnemyName(condi.ID)} : {condi.currentCount} / {condi.RequireCount}", ref ConsoleUI.mainView);
                    }

                    ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
                    ConsoleUI.Instance.DrawTextInBox($"보상 : {targetQuest.Rewards}G", ref ConsoleUI.mainView);
                    ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
                    ConsoleUI.Instance.DrawTextInBox($"달성 여부 : {(targetQuest.isClear ? "완료" : "미완료")}", ref ConsoleUI.mainView);
                }
            }
            else
            {
                var quests = QuestManager.Instance.GetAllQuests();
                ConsoleUI.Instance.DrawTextInBox("====퀘스트 리스트====", ref ConsoleUI.mainView);
                for (int i = 0; i < quests.Count; i++)
                {
                    ConsoleUI.Instance.DrawTextInBox($"{(quests[i].isClear ? AnsiColor.Green : "")}[{i+1}] : {quests[i].Name}{AnsiColor.Reset}", ref ConsoleUI.mainView);
                }
            }
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"0 : 돌아간다", ref ConsoleUI.mainView);

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);
        }
    }
}
