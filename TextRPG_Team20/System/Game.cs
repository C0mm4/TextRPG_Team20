using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Charactor.Enemys;
using TextRPG_Team20.Scene;
using TextRPG_Team20.System;
using TextRPG_Team20.Item;


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
        internal static List<Enemy> enemys = new List<Enemy>();
        

        public enum SceneState
        {
            Title, Intro, Lobby, DungeonSelect, InField, Battle, Result, Shop, Inventory, EquipControl, Status, SkillList, UseItem, Win, Defeat
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
                    newScene = new BattleScene(playerInstance, enemys);
                    break;
                case SceneState.Result:
                    newScene = new ResultScene();
                    break;
                case SceneState.Shop:
                    newScene = new ShopScene();
                    break;
                case SceneState.Inventory:
                    newScene = new InventoryScene(playerInstance.Inventory);   
                    break;
                case SceneState.EquipControl:
                    break;
                case SceneState.Status:
                    newScene = new BattleScene(playerInstance, enemys); //newScene = new InventoryScene(playerInstance.Inventory);  // 몬스터와 충돌 구현 후 정상화
                    break;
                case SceneState.SkillList:
                    break;
                case SceneState.UseItem:
                    break;
                case SceneState.Win:
                    newScene = new WinScene();
                    break;
                case SceneState.Defeat:
                    newScene = new DefeatScene();
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
            SceneChange(SceneState.Intro);
        }

        public void CreatePlayerInstance(string? name)
        {
            string playerName = name ?? "플레이어";
       
            ConsoleUI.Instance.DrawTextInBox($"캐릭터 이름: {playerName}", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("직업을 선택해주세요:", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("1. 전사", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("2. 궁수", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("3. 마법사", ref ConsoleUI.logView);
            ConsoleUI.Instance.DrawTextInBox("원하는 직업의 번호를 입력하세요: ", ref ConsoleUI.logView);
            ConsoleUI.Instance.PrintView(ref ConsoleUI.logView);

            JobType selectedJob = JobType.None; 

            
            while (true)
            {
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            selectedJob = JobType.Warrior;
                            break;
                        case 2:
                            selectedJob = JobType.Archer;
                            break;
                        case 3:
                            selectedJob = JobType.Mage;
                            break;
                        default:
                            ConsoleUI.Instance.DrawTextInBox("잘못된 입력입니다. 1, 2, 3 중 하나를 입력해주세요.", ref ConsoleUI.logView);
                            continue; 
                    }
                    if (selectedJob != JobType.None) 
                    {
                        break;
                    }
                }
                else
                {
                    ConsoleUI.Instance.DrawTextInBox("잘못된 입력입니다. 숫자를 입력해주세요.", ref ConsoleUI.logView);
                }
               
            }
            Status status = new Status(0, 1, 100, 5, 10, name, 0);
            playerInstance = new Player(name ?? "", selectedJob, 0, status);

            

            ConsoleUI.Instance.DrawTextInBox($"{playerName}, {selectedJob}으로 게임을 시작합니다!", ref ConsoleUI.logView);
            Console.ReadKey(); 
        }

        public void SaveGame()
        {

        }

        public void LoadGame() 
        {

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
        }

        public void GameEnd()
        {
            while (_sceneStack.Count > 0)
            {
                PopScene();
            }
        }
    }
}
