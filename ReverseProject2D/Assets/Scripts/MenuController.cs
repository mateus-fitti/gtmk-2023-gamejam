using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public List<Sound> sounds;

    void Awake()
    {
        SetLevelSounds();
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
