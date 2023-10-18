using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelController : MonoBehaviour
{
    public GameObject defeatScreen;
    public GameObject victoryScreen;
    public List<Sound> sounds;

    void Awake()
    {
        SetLevelSounds();
    }

    public void Defeat()
    {
        SoundManager.instance.StopAllSounds();
        FreezeGame(true);
        defeatScreen.SetActive(true);
        PlaySound("Defeat");
    }

    public void Victory()
    {
        FreezeGame(true);
        victoryScreen.SetActive(true);
        PlaySound("Victory");

        string currentScene = SceneManager.GetActiveScene().name;
        string searchString = "Level";
        int index = currentScene.IndexOf(searchString);
        int result = int.Parse(currentScene.Substring(index + searchString.Length));

        if (!GameController.instance.CheckLevelCompleted(result)) GameController.instance.UpdateLevel(result);


    }

    void SetLevelSounds()
    {
        SoundManager.instance.SetGameSounds(sounds);
    }

    public void PlaySound(string name)
    {
        SoundManager.instance.Play(name);
    }

    public void SetVolumeBar(Slider bar)
    {
        SoundManager.instance.SetVolume(bar);
    }

    public void OnSceneButton(string sceneName)
    {
        GameController.instance.OnSceneChange(sceneName);
    }

    public void FreezeGame(bool flag)
    {
        Cursor.visible = flag;


        if (flag) Time.timeScale = 0f;
        else Time.timeScale = 1.0f;
    }

}