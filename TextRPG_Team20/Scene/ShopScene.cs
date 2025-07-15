using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class ShopScene : IScene
    {
        private bool BuyShop = false;
        public bool Action(int input)
        {
            if (BuyShop == false)
            {
                switch (input)
                {
                    case 0:
                        Game.Instance.SceneChange(Game.SceneState.Lobby);
                        return false;

                    case 1:
                        Console.WriteLine("구매로 변경");
                        BuyShop = true;
                        return true;

                    default:
                        ((IScene)this).InvalidInput();
                        return true;
                }
            }
            else
            {
                switch (input)
                {
                    case 0:
                        BuyShop = false;
                        Game.Instance.PopScene(); // 이전 씬으로 돌아간다
                        return false;

                    default:
                        ((IScene)this).InvalidInput();
                        return true;
                }
            }

        }

        //씬 출력 메서드
        public void PrintScene()
        {
            // mainView 출력
            if (BuyShop == false)
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}상점{AnsiColor.Reset}", ref ConsoleUI.mainView);
            }
            else 
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}상점 - 구매{AnsiColor.Reset}", ref ConsoleUI.mainView);
            }

            ConsoleUI.Instance.DrawTextInBox("좋은 아이템? 운이지 뭐~.", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("[보유 골드]", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow} G{AnsiColor.Reset}", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("[아이템 목록]", ref ConsoleUI.mainView);

            if (BuyShop == true)
            {
                ConsoleUI.Instance.DrawTextInBox($"- [🗡️]일반 무기 상자 \t| {AnsiColor.Yellow}50 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"- [🗡️]{AnsiColor.Blue}레어 무기 상자 \t{AnsiColor.Reset}| {AnsiColor.Yellow}500 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"- [🗡️]{AnsiColor.Red}에픽 무기 상자 \t{AnsiColor.Reset}| {AnsiColor.Yellow} 5000 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"- [🗡️]일반 방어구 상자 \t| {AnsiColor.Yellow}20 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"- [🗡️]{AnsiColor.Blue}레어 방어구 상자 \t{AnsiColor.Reset}| {AnsiColor.Yellow}200 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"- [🗡️]{AnsiColor.Red}에픽 방어구 상자 \t{AnsiColor.Reset}| {AnsiColor.Yellow}2000 G{AnsiColor.Reset}", ref ConsoleUI.mainView);

                // info1View 출력
                ConsoleUI.Instance.DrawTextInBox("[1] 아이템 구매", ref ConsoleUI.info1View);
                ConsoleUI.Instance.DrawTextInBox("[0] 상점 나가기", ref ConsoleUI.info1View);
            }
            else
            {
                ConsoleUI.Instance.DrawTextInBox($"[1] [🗡️]일반 무기 상자 \t| {AnsiColor.Yellow}50 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"[2] [🗡️]{AnsiColor.Blue}레어 무기 상자 \t{AnsiColor.Reset}| {AnsiColor.Yellow}500 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"[3] [🗡️]{AnsiColor.Red}에픽 무기 상자 \t{AnsiColor.Reset}| {AnsiColor.Yellow} 5000 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"[4] [🗡️]일반 방어구 상자 \t| {AnsiColor.Yellow}20 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"[5] [🗡️]{AnsiColor.Blue}레어 방어구 상자 \t{AnsiColor.Reset}| {AnsiColor.Yellow}200 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"[6] [🗡️]{AnsiColor.Red}에픽 방어구 상자 \t{AnsiColor.Reset}| {AnsiColor.Yellow}2000 G{AnsiColor.Reset}", ref ConsoleUI.mainView);

                // info1View 출력
                ConsoleUI.Instance.DrawTextInBox("[0] 구매 종료", ref ConsoleUI.info1View);                
            }
            
            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);
        }
    }
}

