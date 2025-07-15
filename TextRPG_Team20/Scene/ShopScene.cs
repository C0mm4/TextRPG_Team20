using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class ShopScene : IScene
    {
        private int BuyScene = 0;

        public bool Action(int input)
        {
            if (BuyScene == 0)
            {
                switch (input)
                {
                    case 0:
                        Game.Instance.PopScene();
                        return false;
                    case 1:
                        BuyScene = 1;
                        return false;
                    default:
                        Console.WriteLine("Input Error!");
                        return true;
                }
            }
            else
            {
                switch (input)
                {
                    case 0:
                        Game.Instance.PopScene();
                        BuyScene = 0;
                        return false;
                    default:
                        Console.WriteLine("Input Error!");
                        return true;
                }
            }
        }


        //씬 출력 메서드
        public void PrintScene()
        {
            if(BuyScene == 0) 
            {
                Console.WriteLine($"{AnsiColor.Yellow}상점{AnsiColor.Reset}");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine("");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{AnsiColor.Yellow} G{AnsiColor.Reset}");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                // 아이템 목록 연결


                Console.WriteLine("");
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("0. 나가기");
            }
            else
            {
                Console.WriteLine($"{AnsiColor.Yellow}상점 - 구매{AnsiColor.Reset}");
                Console.WriteLine("구매할 아이템의 번호를 입력해 주세요.");
                Console.WriteLine("");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{AnsiColor.Yellow} G{AnsiColor.Reset}");
                Console.WriteLine("");
                Console.WriteLine("[아이템 목록]");
                // 아이템 목록 연결


                Console.WriteLine("");
                Console.WriteLine("0. 구매 종료");
            }

        }

  
    }
}
