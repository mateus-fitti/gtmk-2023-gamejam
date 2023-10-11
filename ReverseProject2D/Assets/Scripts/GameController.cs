using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController instance;
    [Tooltip("Ative para deixar todos as fases liberadas no seletor. Desative para liberar apenas fases que você já completou.")]
    public bool unlockAllLevels = true;
    private bool _gameStarted = false;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && this.GetGameStarted())
        {
            Button pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();

            // Verifica se o botão não é nulo e, em seguida, simula o clique nele
            if (pauseButton != null)
            {
                pauseButton.onClick.Invoke();
            }
        }
    }

    public void OnSceneChange(string sceneName)
    {
        SetGameStarted(false);
        SceneManager.LoadScene(sceneName);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1.0f;
    }


    public bool GetGameStarted()
    {
        return _gameStarted;
    }

    public void SetGameStarted(bool newValue)
    {
        _gameStarted = newValue;
    }

    public int CurrentLevel()
    {
        return PlayerPrefs.GetInt("CurrentLevel", 1);
    }

    public void UpdateLevel(int level)
    {
        int nextLevel = level + 1;
        if ((nextLevel) < SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt("Level" + level, 1);
            PlayerPrefs.SetInt("CurrentLevel", nextLevel);
            PlayerPrefs.Save();
        }
    }

    public bool CheckLevelCompleted(int num)
    {
        bool flag = false;
        int level = PlayerPrefs.GetInt("Level" + num, 0);
        if (level > 0) flag = true;
        else flag = false;

        return flag;
    }

    public int NumberOfLevels()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        List<string> scenes = new List<string>(sceneCount);

        for (int i = 0; i < sceneCount; i++)
        {
            string sceneName = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            if (sceneName.ToLower().Contains("level"))
            {
                scenes.Add(sceneName);
            }
        }
        if (unlockAllLevels) return scenes.Count;
        else return CurrentLevel();
    }
}