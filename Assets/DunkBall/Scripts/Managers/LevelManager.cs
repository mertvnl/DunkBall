using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DunkBall.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        private void Start()
        {
            EventManager.OnLevelStarted.Invoke();
        }

        public void ReloadLevel(float delay)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentSceneIndex = currentScene.buildIndex;

            StartCoroutine(LoadLevelCo(delay, currentSceneIndex));
        }

        private IEnumerator LoadLevelCo(float delay, int levelIndex)
        {
            EventManager.OnLevelFinished.Invoke();

            yield return new WaitForSeconds(delay);

            SceneManager.LoadScene(levelIndex);
        }
    }
}

