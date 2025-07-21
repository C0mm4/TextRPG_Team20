using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.System;

namespace TextRPG_Team20.Scene
{
    internal class IntroScene : Scene
    {
        private  string? nameInput;
        public override bool Action(int input)
        {
            switch (input) 
            {
                case 1:
                    ConsoleUI.Instance.DrawTextInBox("어서오세요!", ref ConsoleUI.mainView);
                    // Add Player Instance Initialize
                    Game.Instance.CreatePlayerInstance(nameInput);
                    //ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
                    StartOpening();
                    Game.Instance.SceneChange(Game.SceneState.Lobby);
                    return false;
                case 2:
                    ConsoleUI.Instance.DrawTextInBox("확인했습니다", ref ConsoleUI.mainView);
                    ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
                    return true;
                default:
                    ((Scene)this).InvalidInput();
                    return true;

            }

        }

        public override void PrintScene()
        {            
            ConsoleUI.Instance.DrawTextInBox("페이팔스토리에 오신걸 환영합니다", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("이름을 입력해주세요 >> ", ref ConsoleUI.inputView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
            ConsoleUI.Instance.PrintView(ref ConsoleUI.inputView, "left", "top");

            nameInput = ConsoleUI.Read(ref ConsoleUI.inputView);

            ConsoleUI.mainView.ClearBuffer();
            ConsoleUI.inputView.ClearBuffer();

            ConsoleUI.Instance.DrawTextInBox($"{AnsiColor.Cyan}{nameInput}{AnsiColor.Reset} 이(가) 당신의 이름이 맞습니까?", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("1.예", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("2.아니요", ref ConsoleUI.mainView);
            
            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");
        }

        public void StartOpening()
        {
            ConsoleUI.mainView.ClearBuffer();
            // 메인뷰 우측에 슈피겔만 출력
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);

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

            ConsoleUI.Instance.InsertTextInBox(list, ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);
            ConsoleUI.Instance.DrawTextInBox("", ref ConsoleUI.mainView);

            List<string> text = new List<string>()
            {
                $"{Game.playerInstance.status.Name} : 사냥을 해도 원킬이 안 나니",
                $"{Game.playerInstance.status.Name} : 이 세상도 결국 차가운 세상의 거울이구나...",
                $"슈피겔만 : 이 세상에 공짜는 없다지만",
                $"슈피겔만 : 여기는 얘기가 다르지",
                $"슈피겔만 : 200만 골드만 있으면!",
                $"슈피겔만 : 한방에 잡혀, 성장이 안 막혀!",
                $"{Game.playerInstance.status.Name} : 2... 200만?? 그런 큰 돈을 어떻게 모아!!",
                $"슈피겔만 : 사냥을 해야지",
                $"슈피겔만 : 강해지고 싶지? 모험을 펼치고 싶지?",
                $"슈피겔만 : 200만 골드만 있으면!!!",
                $"슈피겔만 : 꿈이 이뤄져! 어서 사냥해!",
            };

            foreach (var line in text)
            {
                StringBuilder sb = new();
                for (int i = 0; i < line.Length; i++)
                {
                    int beforeLineCount = ConsoleUI.mainView.lines.Count;
                    sb.Append(line[i]);
                    // 텍스트 출력
                    string targetText = sb.ToString();
                    ConsoleUI.Instance.DrawTextInBox(targetText, ref ConsoleUI.mainView);
                    ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "top");

                    Thread.Sleep(50);

                    // 변화가 생겼을 때만 라인 제거
                    if (ConsoleUI.mainView.lines.Count > beforeLineCount)
                    {
                        ConsoleUI.RemoveLines(ref ConsoleUI.mainView, 1);
                    }
                }

                // 줄 전체 출력 후 1.5초 정지
                Thread.Sleep(1500);

                // 줄 전체 출력 후 View를 정리
                ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "top");
            }


            ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "top");
        }
    }
}
