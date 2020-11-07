using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    private float volume;
    private float pitch;

    [HideInInspector]
    public AudioSource source;
}
