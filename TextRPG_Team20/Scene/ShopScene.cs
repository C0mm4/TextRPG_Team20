using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class ShopScene : IScene
    {
        private bool BuyShop = false;   //상점 구매전환 변수
        private bool SellShop = false;   //상점 판매전환 변수

        public bool Action(int input)
        {
            if (BuyShop == false && SellShop == false)
            {
                switch (input)
                {
                    case 0:
                        Game.Instance.SceneChange(Game.SceneState.Lobby);
                        return false;

                    case 1:     //구매로 변경
                        BuyShop = true;
                        return false;

                    case 2:     //판매로 변경
                        SellShop = true;
                        return false;

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
                        SellShop = false;
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
            if (BuyShop == false && SellShop == false)      // 상점 제목
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}상점{AnsiColor.Reset}", ref ConsoleUI.mainView);
            }
            else if (BuyShop == true)
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}상점-{AnsiColor.Green}구매{AnsiColor.Reset}", ref ConsoleUI.mainView);
            }
            else
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}상점-{AnsiColor.Red}판매{AnsiColor.Reset}", ref ConsoleUI.mainView);
            }

            ConsoleUI.Instance.DrawTextInBox("좋은 아이템? 운이지 뭐~.", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("[아이템 목록]", ref ConsoleUI.mainView);

            if (BuyShop == false && SellShop == false)      //품목
            {
                ConsoleUI.Instance.DrawTextInBox($"- [?]일반 무기 상자 \t\t| {AnsiColor.Yellow}50 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"- [?]{AnsiColor.Blue}레어 무기 상자 \t\t{AnsiColor.Reset}| {AnsiColor.Yellow}500 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"- [?]{AnsiColor.Red}에픽 무기 상자 \t\t{AnsiColor.Reset}| {AnsiColor.Yellow}5000 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"- [?]일반 방어구 상자 \t| {AnsiColor.Yellow}20 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"- [?]{AnsiColor.Blue}레어 방어구 상자 \t{AnsiColor.Reset}| {AnsiColor.Yellow}200 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"- [?]{AnsiColor.Red}에픽 방어구 상자 \t{AnsiColor.Reset}| {AnsiColor.Yellow}2000 G{AnsiColor.Reset}", ref ConsoleUI.mainView);

                // info2View 출력         
                ConsoleUI.Instance.DrawTextInBox("[0] 상점 나가기", ref ConsoleUI.info2View);
                ConsoleUI.Instance.DrawTextInBox("[1] 아이템 구매", ref ConsoleUI.info2View);
                ConsoleUI.Instance.DrawTextInBox("[2] 아이템 판매", ref ConsoleUI.info2View);
            }
            else if (BuyShop == true)
            {
                ConsoleUI.Instance.DrawTextInBox($"[1] [?]일반 무기 상자 \t| {AnsiColor.Yellow}50 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"[2] [?]{AnsiColor.Blue}레어 무기 상자 \t{AnsiColor.Reset}| {AnsiColor.Yellow}500 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"[3] [?]{AnsiColor.Red}에픽 무기 상자 \t{AnsiColor.Reset}| {AnsiColor.Yellow}5000 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"[4] [?]일반 방어구 상자 \t| {AnsiColor.Yellow}20 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"[5] [?]{AnsiColor.Blue}레어 방어구 상자 \t{AnsiColor.Reset}| {AnsiColor.Yellow}200 G{AnsiColor.Reset}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox($"[6] [?]{AnsiColor.Red}에픽 방어구 상자 \t{AnsiColor.Reset}| {AnsiColor.Yellow}2000 G{AnsiColor.Reset}", ref ConsoleUI.mainView);

                // info2View 출력
                ConsoleUI.Instance.DrawTextInBox("[0] 구매 종료", ref ConsoleUI.info2View);                
            }
            else
            {
                ConsoleUI.Instance.DrawTextInBox($"[1] {AnsiColor.Red}판매가능 품목 없음{AnsiColor.Reset}", ref ConsoleUI.mainView);

                ConsoleUI.Instance.DrawTextInBox("[0] 구매 종료", ref ConsoleUI.info2View);
            }

            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView);
        }
    }
}

