using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team20.Scene
{
    internal class ShopScene : Scene
    {
        private ShopManager shopManager;    // 생성자에 ShopManager형태에 필드 선언
        public ShopScene(ShopManager shopManager)       // 생성자에 ShopManager를 매개변수로 받아서, shopManager 필드에 저장
        {
            this.shopManager = shopManager;
        }

        
        private bool BuyShop = false;   //상점 구매전환 변수
        private bool SellShop = false;   //상점 판매전환 변수

        public override bool Action(int input)
        {
            if (BuyShop == false && SellShop == false)
            {
                switch (input)
                {
                    case 0:
                        Game.Instance.PopScene();
                        return false;

                    case 1:     //구매로 변경
                        BuyShop = true;
                        return false;

                    case 2:     //판매로 변경
                        SellShop = true;
                        return false;

                    default:
                        ((Scene)this).InvalidInput();
                        return true;
                }
            }
            else if (SellShop == true)
            {
                switch (input)
                {
                    case 0:
                        SellShop = false;
                        return false;

                    default:
                        // 입력값이 1 이상이고 인벤토리 아이템 개수 이하일 때 판매
                        if (input > 0 && input <= Game.playerInstance.Inventory.Items.Count)
                        {
                            var (success, message) = shopManager.SellItem(input - 1);

                            ConsoleUI.Instance.DrawTextInBox(message, ref ConsoleUI.logView);
                            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);

                            return false;
                        }
                        else
                        {
                            ((Scene)this).InvalidInput();
                            return true;
                        }
                        
                }   

            }
            else // if (BuyShop == true) // 구매 모드 (오류 방지를 위해 else로 처리)
            {
                switch (input)
                {
                    case 0:
                        BuyShop = false; // 구매 모드 종료
                        return false;
                  
                    default:
                // TODO: 아이템 구매 로직 구현 (예: shopManager.BuyItem(input - 1);)
                        ((Scene)this).InvalidInput();
                        return true;
                }
            }
        }

        //씬 출력 메서드
        public override void PrintScene()
        {
            ConsoleUI.SplitRectHorizontal(ConsoleUI.mainView, out ConsoleUI.Rect left, out ConsoleUI.Rect right);

            // mainView 출력
            if (BuyShop == false && SellShop == false)      // 상점 제목
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}상점{AnsiColor.Reset}", ref left);
            }
            else if (BuyShop == true)
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}상점-{AnsiColor.Green}구매{AnsiColor.Reset}", ref left);
            }
            else
            {
                ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Yellow}상점-{AnsiColor.Red}판매{AnsiColor.Reset}", ref left);
            }


            ConsoleUI.Instance.DrawTextInBox("좋은 아이템? 운이지 뭐~.", ref left);
            ConsoleUI.Instance.DrawTextInBox("", ref left);
            ConsoleUI.Instance.DrawTextInBox("[아이템 목록]", ref left);


            if (BuyShop == false && SellShop == false)  
            {
                // 판매할 아이템 추가 하는 코드 ShopManager.SetSellItems() 에서 아이템 추가 가능
                for (int i = 0; i < shopManager.sellItems.Count; i++)      
                {
                    Item.Item item = shopManager.sellItems[i];
                    ConsoleUI.Instance.DrawTextInBox($"- {item.data.Name} \t| {AnsiColor.Yellow}{item.data.Gold} G{AnsiColor.Reset}", ref left);
                }

                // info2View 출력         
                ConsoleUI.Instance.DrawTextInBox("[1] 아이템 구매", ref ConsoleUI.info2View);
                ConsoleUI.Instance.DrawTextInBox("[2] 아이템 판매", ref ConsoleUI.info2View);
                ConsoleUI.Instance.DrawTextInBox("[0] 상점 나가기", ref ConsoleUI.info2View);
            }
            else if (BuyShop == true)
            {
                for (int i = 0; i < shopManager.sellItems.Count; i++)
                {
                    Item.Item item = shopManager.sellItems[i];
                    ConsoleUI.Instance.DrawTextInBox($"[{i + 1}] {item.data.Name} \t| {AnsiColor.Yellow}{item.data.Gold} G{AnsiColor.Reset}", ref left);
                }
                 

                // info2View 출력
                ConsoleUI.Instance.DrawTextInBox("[0] 구매 종료", ref ConsoleUI.info2View);                
            }
            else if (SellShop == true) 
            {
                var inventory = Game.playerInstance.Inventory;
                if (inventory.Items.Count == 0)
                {
                    ConsoleUI.Instance.DrawTextInBox($"[?] {AnsiColor.Red}판매할 아이템이 없습니다.{AnsiColor.Reset}", ref left);
                }
                else
                {
                    for (int i = 0; i < inventory.Items.Count; i++)
                    {
                        var item = inventory.Items[i];
                        string equipMark = item.data.isEquipped ? $"{AnsiColor.Green}[E]{AnsiColor.Reset} " : "";
                        ConsoleUI.Instance.DrawTextInBox($"[{i + 1}] {equipMark}{item.data.Name} | {AnsiColor.Yellow}{item.GetSellPrice()} G{AnsiColor.Reset}", ref left);
                    }
                }

                ConsoleUI.Instance.DrawTextInBox("[0] 판매 종료", ref ConsoleUI.info2View);
            }


            // 메인뷰 우측에 슈피겔만 출력
            ConsoleUI.Instance.DrawTextInBox("", ref right);
            ConsoleUI.Instance.DrawTextInBox("", ref right);

            List<string> list = new List<string>();
            list.Add("               @#*#%%%%@@             ");
            list.Add("             %*+*##%%%%%@@@           ");
            list.Add("            @##**##%%%%@@@@@          ");
            list.Add("             @%#*##%%%@@@@%@          ");
            list.Add("             @%#+*%%%@@@@%@           ");
            list.Add("             @%=-===++*@@@            ");
            list.Add("            @##%%%%%%##*#@            ");
            list.Add("          @%%@@@@@@@@@%%%@            ");
            list.Add("          @%@%=..:=-*%@@@%@           ");
            list.Add("          @*-....:=--=-%@%@           ");
            list.Add("         @=:.=+::=.- .-:-%  @@        ");
            list.Add("      *-==--+...:--..:=-:-#-+=+       ");
            list.Add("      =:+:==#:::-+*=:=+=.:===-*%#@    ");
            list.Add("  @@@ @#-:::-***+*=-.:.=.:-@#%@+.*@@@@");
            list.Add("+:::.-@@=.+.......  ..-::--@@%=::::::+");
            list.Add("@-.:=:*#%=::+:.... ++::--*#*%+-:::::=%");
            list.Add("@@%@@%@@%%%*=:...:-=+#%@%%%%@%+%@@    ");
            list.Add("@*%*#  @@##%-###%*:.-##%%%@@@         ");
            list.Add(" %*#@  @%@%#**#%%:***#%@#%%@@         ");
            list.Add(" #**@  @@#@#**%******#%%@@@@@         ");
            list.Add("  ##    @@%#%%%%%%%%%%@@@@@           ");
            list.Add("  #%       @#%@@@@@@@%#@@             ");
            list.Add("  %@       @#%@      @#@@             ");

            ConsoleUI.Instance.InsertTextInBox(list, ref right);
            ConsoleUI.Instance.DrawTextInBox("", ref right);
            ConsoleUI.Instance.DrawTextInBox("", ref right);
            ConsoleUI.Instance.DrawTextInBox("200만 지르면 게임이 편해진다네!", ref right);

            ConsoleUI.Instance.PrintView(ref left);
            ConsoleUI.Instance.PrintView(ref right, "center", "middle");
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info2View);

        }
    }
}

