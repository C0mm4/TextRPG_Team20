using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Scene;

namespace TextRPG_Team20
{
    internal class Shop : IScene
    {
        public void PrintScene()
        {

            while (true)
            {
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
               // Console.WriteLine(playerStatus.gold + "G\n");

                Console.WriteLine("[아이템 목록]");
                // 아이템 목록 연결
               // system_Shop.ShopList();

                Console.WriteLine("");

                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("0. 나가기");


              }
         }
  
    }
}
