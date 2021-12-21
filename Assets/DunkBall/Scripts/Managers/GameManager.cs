using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DunkBall.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public void CompleteStage(bool status)
        {
            if (status)
            {
                EventManager.OnGameWin.Invoke();
                LevelManager.Instance.ReloadLevel(4f);
            }
            else
            {
                EventManager.OnGameLose.Invoke();
                LevelManager.Instance.ReloadLevel(4f);
            }
        }
    }
}

