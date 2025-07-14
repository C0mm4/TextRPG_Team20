using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TextRPG_Team20.Scene;

namespace TextRPG_Team20
{
    public class Game
    {
        private Game _instance;
        public Game Instance
        {
            get 
            { 
                if(_instance == null)
                {
                    _instance = new Game();
                } return _instance;
            }
        }

        public enum SceneState
        {
            Title, Lobby, DungeonSelect, InField, Battle, Result, Shop, Inventory, EquipControl, Status, SkillList, UseItem, 
        }

        private SceneState _state;
        private Stack<IScene> sceneStack;
        private IScene currentScene;

        public Game()
        {

        }

        public void SceneChange(SceneState state)
        {
            IScene? newScene = null;
            switch (state)
            {
                case SceneState.Title:
                    break;
                case SceneState.Lobby:
                    break;
                case SceneState.DungeonSelect:
                    break;
                case SceneState.InField:
                    break;
                case SceneState.Battle:
                    break;
                case SceneState.Result:
                    break;
                case SceneState.Shop:
                    break;
                case SceneState.Inventory:
                    break;
                case SceneState.EquipControl:
                    break;
                case SceneState.Status:
                    break;
                case SceneState.SkillList:
                    break;
                case SceneState.UseItem:
                    break;

            }
            if (newScene != null) 
            {
                if(currentScene.GetType().Name != "IntroScene")
                {
                    sceneStack.Push(currentScene);
                }
                currentScene = newScene;
            }
        }

        public void PopScene()
        {
            if(sceneStack.Count > 0)
            {
                currentScene = sceneStack.Pop();
            }
            else
            {
                currentScene = null;
            }
        }

        public void SaveGame()
        {

        }

        public void LoadGame() 
        {

        }
    }
}
