using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public struct SceneState
        {

        }

        private SceneState _state;

        public Game()
        {

        }

        public void SceneChange(SceneState state)
        {

        }

        public void PopScene()
        {

        }

        public void SaveGame()
        {

        }

        public void LoadGame() 
        {

        }
    }
}
