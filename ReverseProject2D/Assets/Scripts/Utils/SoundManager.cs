using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixerGroup mixer;
    public List<Sound> sounds;
    
    public static SoundManager instance;

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

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = mixer;
        }

        Play("Test");
    }

    public void Play (string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        if (s == null) return;

        s.source.volume = s.volume;
        s.source.pitch = s.pitch;

        if (s.single) StopAllSounds();
        
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s =sounds.Find(sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        } 
        s.source.Stop();
    }


    void StopAllSounds()
    {
        foreach (Sound s in sounds)
        {
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.Stop();
        }
    }

}
