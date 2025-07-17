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
            var d = new Dungeon.DungeonManager();
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
            Status status = new Status(0, 1, 100, 5, 10);
            playerInstance = new Player(name ?? "", "Job", 0, status);
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
