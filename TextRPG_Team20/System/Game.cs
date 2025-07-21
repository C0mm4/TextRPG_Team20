using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Charactor.Enemys;
using TextRPG_Team20.Scene;
using TextRPG_Team20.System;


namespace TextRPG_Team20
{
    public class Game
    {
        private static Game? _instance;
        public static Game Instance
        {
            get 
            { 
                if(_instance == null)
                {
                    _instance = new Game();
                } 
                return _instance;
            }
        }

        internal static Player? playerInstance;
        public static Random random = new Random();

        public enum SceneState
        {
            Title, Intro, Lobby, DungeonSelect, InField, Battle, BossBattle, Result, Shop, Inventory, EquipControl, Status, SkillList, UseItem, Win, Defeat, DungeonClear, Quest
        }

        private Stack<Scene.Scene> _sceneStack;
        private Scene.Scene? _currentScene;

        public Game()
        {
            if(_instance == null)
            {
                _instance = this;
            }

//            Console.SetBufferSize(160, 50);   // 버퍼도 동일하게 설정
//            Console.SetWindowSize(160, 50);   // 가로 80, 세로 30
            Console.Clear();
            var a = ItemManager.Instance;
            var d = Dungeon.DungeonManager.Instance;
            var m = MobSpawnner.Instance;
            var s = ShopManager.Instance;
            _sceneStack = new Stack<Scene.Scene>();

            SceneChange(SceneState.Title);
            playerInstance = null;

            while(_currentScene != null)
            {
                _currentScene.Print();
            }
        }

        public void SceneChange(SceneState state)
        {
            Scene.Scene? newScene = null;
            switch (state)
            {
                case SceneState.Title:
                    newScene = new TitleScene();
                    break;
                case SceneState.Intro:
                    newScene = new IntroScene();
                    break;
                case SceneState.Lobby:
                    newScene = new LobbyScene();
                    break;
                case SceneState.DungeonSelect:
                    newScene = new DungeonSelectScene();
                    break;
                case SceneState.InField:
                    newScene = new InFieldScene();
                    break;
                case SceneState.Battle:
                    newScene = new BattleScene(playerInstance);
                    break;
                case SceneState.BossBattle:
                    newScene = new BossBattleScene();
                    break;
                case SceneState.Result:
                    newScene = new ResultScene();
                    break;
                case SceneState.Shop:
                    newScene = new ShopScene(ShopManager.Instance);
                    break;
                case SceneState.Inventory:
                    newScene = new InventoryScene(playerInstance.Inventory);   
                    break;
                case SceneState.EquipControl:
                    break;
                case SceneState.Status:
                    newScene = new StatScene(playerInstance.status);
                    break;
                case SceneState.SkillList:
                    newScene = new SkillListScene();
                    break;
                case SceneState.UseItem:
                    break;
                case SceneState.Win:
                    newScene = new WinScene();
                    break;
                case SceneState.Defeat:
                    newScene = new DefeatScene();
                    break;
                case SceneState.DungeonClear:
                    newScene = new DungeonClearScene();
                    break;
                case SceneState.Quest:
                    newScene = new QuestListScene();
                    break;
            }
            if (newScene != null) 
            {
                if(_currentScene != null && _currentScene.GetType().Name != "IntroScene")
                {
                    _sceneStack.Push(_currentScene);
                }
                _currentScene = newScene;
            }
        }

        public void PopScene()
        {
            if(_sceneStack.Count > 0)
            {
                _currentScene = _sceneStack.Pop();
            }
            else
            {
                _currentScene = null;
            }
        }

        public void GameStart()
        {
            QuestManager.Instance.GenerateQuests();
            SceneChange(SceneState.Intro);
        }

        public void CreatePlayerInstance(string? name, JobType job = JobType.None, int gold = 50000)
        {
            if(job == JobType.None)
            {

                string playerName = name.Trim() ?? "플레이어";
                ConsoleUI.mainView.ClearBuffer();
                ConsoleUI.Instance.DrawTextInBox($"캐릭터 이름: {playerName}", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox("직업을 선택해주세요:", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox("1.전사", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox("2.궁수", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox("3.마법사", ref ConsoleUI.mainView);
                ConsoleUI.Instance.DrawTextInBox("원하는 직업의 번호를 입력하세요: ", ref ConsoleUI.mainView);
                ConsoleUI.Instance.PrintView(ref ConsoleUI.mainView, "center", "middle");

                job = JobType.None;


                while (true)
                {
                    string? input = Console.ReadLine();
                    if (int.TryParse(input, out int choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                job = JobType.Warrior;
                                break;
                            case 2:
                                job = JobType.Archer;
                                break;
                            case 3:
                                job = JobType.Mage;
                                break;
                            default:
                                ConsoleUI.Instance.DrawTextInBox("잘못된 입력입니다!", ref ConsoleUI.logView);
                                ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
                                continue;
                        }
                        if (job != JobType.None)
                        {
                            break;
                        }
                    }
                    else
                    {
                        ConsoleUI.Instance.DrawTextInBox("잘못된 입력입니다!", ref ConsoleUI.logView);
                        ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);
                    }

                }
            }
            // 캐릭터 초기 스텟
            Status status = new Status(0, 1, 100, 5, 10, name.Trim() ?? "", gold, 0);
            playerInstance = new Player(job, status);



            ConsoleUI.Instance.DrawTextInBox($"{name}, {job.ToKoreanString()}로 게임을 시작합니다!", ref ConsoleUI.logView);
            

        }

        public void SaveGame()
        {
            SaveData.Instance.Save();
        }

        public void LoadGame() 
        {
            SaveData.Instance.Load();
            SceneChange(SceneState.Lobby);
        }

        public void ReturnToLobby()
        {
            if(_currentScene != null)
            {
                while (_currentScene.GetType().Name != "LobbyScene")
                {
                    PopScene();
                }
            }
        }

        public void ReturnToTitle()
        {
            while (_sceneStack.Count > 0)
            {
                PopScene();
            }

            ConsoleUI.logView.ClearBuffer();
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);

            ConsoleUI.info1View.ClearBuffer();
            ConsoleUI.Instance.PrintView(ref ConsoleUI.info1View);

            SaveGame();
            playerInstance = null;
            SceneChange(SceneState.Title);
        }

        public void GameEnd()
        {
            while (_sceneStack.Count > 0)
            {
                _sceneStack.Pop();
            }

            _currentScene = null;
        }

        public int GetCurrentFieldID()      // 현재 플레이어가 있는 필드의 ID 가져오는 메서드 
        {
            return Dungeon.DungeonManager.Instance.currentField?.FieldID ?? 1;
        }
    }
}
