using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

[Serializable]
public class Sound
{
    [Tooltip("Nome do áudio")]
    public string name;
    [Tooltip("Arraste o áudio correspondente para aqui")]
    public AudioClip clip;
    [Tooltip("Volume do áudio")]
    [Range(0f, 1.2f)]
    public float volume = .75f;
    [Tooltip("Altura do áudio")]
    [Range(.1f, 3f)]
    public float pitch = 1f;
    [Tooltip("Marque se o áudio deve se repetir")]
    public bool loop;
    [Tooltip("Marque se apenas este áudio deve ser tocado quando acionado")]
    public bool single = false;

    [HideInInspector]
    public AudioSource source;
    [HideInInspector]
    public AudioMixerGroup mixer;
}
