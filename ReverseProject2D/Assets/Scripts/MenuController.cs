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

    public void QuitGame()
    {
        // Verifique a plataforma em que o jogo está sendo executado
        // e tome a ação apropriada para fechar o jogo.
#if UNITY_EDITOR
        // Se estiver no Editor do Unity, pare a reprodução no editor.
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        // Se estiver em uma plataforma de desktop (Windows, Mac, Linux), feche o aplicativo.
        Application.Quit();
#elif UNITY_ANDROID
        // Se estiver no Android, use a função nativa para fechar o aplicativo.
        // Observe que a função pode não funcionar em todos os dispositivos Android.
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
            .GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call("finish");
#else
        // Em outras plataformas, você pode adicionar lógica específica para fechar o jogo.
        // Por padrão, encerre o aplicativo.
        Application.Quit();
#endif
    }
}
