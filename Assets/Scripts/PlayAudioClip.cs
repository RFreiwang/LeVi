using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioClip : MonoBehaviour
{
    public void PlayAudio(string sourceName)
    {
        FindObjectOfType<AudioManager>().PlayOrStop(sourceName);
    }
}
