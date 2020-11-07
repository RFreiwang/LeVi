using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private AudioSource src;

    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        if(src == null)
        {
            src = gameObject.GetComponent<AudioSource>();
        }

        foreach(Sound s in sounds)
        {
            //s.source = gameObject.AddComponent<AudioSource>();
            //s.source.clip = s.clip;
            //s.source.volume = s.volume;
            //s.source.pitch = s.pitch;
            //s.source.playOnAwake = false;
            //s.source.playOnAwake = false;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.clip.name == name);
        if(s == null)
        {
            return;
        }
        src.clip = s.clip;
        src.Play();
    }

    public void Stop()
    {
        src.Stop();
    }

    public IEnumerator WaitForSound(AudioSource sound)
    {
        yield return new WaitUntil(() => sound.isPlaying == false);
        Stop();
    }

    public void PlayOrStop(string name)
    {
        if(src.isPlaying)
        {
            Stop();
        }
        else
        {
            Play(name);
        }
    }

    public void Start()
    {

    }
}
