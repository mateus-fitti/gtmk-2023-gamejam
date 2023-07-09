using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        defeatScreen.SetActive(true);
        PlaySound("Defeat");
    }

    public void Victory()
    {
        victoryScreen.SetActive(true);
        PlaySound("Victory");
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

}
