using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal abstract class Scene
    {
        public virtual void Print()
        {
            Console.Clear();
            if (!ClearBuffer())
            {
                Thread.Sleep(50);
                return;
            }
            SetPlayerInfo();
            PrintUIViews();
            PrintScene();
            var input = GetAction();
            var isDelay = Action(input);
            PrintUIViews();
            if(isDelay)
                Thread.Sleep(500);
        }

        public bool ClearBuffer()
        {
            // 혹시 모를 싱글톤 생성 전 Clear 방지 위한 싱글톤 호출
            var c = ConsoleUI.Instance;
            ConsoleUI.mainView.ClearBuffer();
            ConsoleUI.info1View.ClearBuffer();
            ConsoleUI.info2View.ClearBuffer();
            ConsoleUI.inputView.ClearBuffer();

            bool ret = true;

            ret &= ConsoleUI.mainView.DrawRect();
            ret &= ConsoleUI.info1View.DrawRect();
            ret &= ConsoleUI.info2View.DrawRect();
            ret &= ConsoleUI.inputView.DrawRect();
            ret &= ConsoleUI.logView.DrawRect();

            if (!ret)
            {
                Console.Clear();
                Console.WriteLine("콘솔 UI 크기를 초과했습니다. 해상도나 콘솔 설정을 확인하세요.");
                return false;
            }

            return true;
        }



        public abstract void PrintScene();

        public virtual int GetAction()
        {
            ConsoleUI.Instance.DrawTextInBox("원하시는 행동을 입력해주세요 >>", ref ConsoleUI.inputView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true); 
            }
            string? s = ConsoleUI.Read(ref ConsoleUI.inputView);
            var isAble = int.TryParse(s, out var action);
            if (isAble)
            {
                return action;
            }
            else
            {
                return -1;
            }
        }
        public void InvalidInput()
        {
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Red}잘못된 입력입니다!{AnsiColor.Reset}", ref ConsoleUI.inputView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView, "left", "top");
        }

        /// <summary>
        /// 입력 값으로 씬의 행동을 서술하는 메소드
        /// </summary>
        /// <param name="input">입력값</param>
        /// <returns>딜레이 트리거 boolean</returns>
        public abstract bool Action(int input);

        public void SetPlayerInfo()
        {
            ConsoleUI.info1View.ClearBuffer();
            ConsoleUI.info2View.ClearBuffer();

            if (Game.playerInstance != null)
                Game.playerInstance.CharacterInfo();
        }

        public void PrintUIViews()
        {
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);
        }
    }
}
